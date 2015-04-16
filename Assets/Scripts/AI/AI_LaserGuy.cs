using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(ShipControl))]
public class AI_LaserGuy : AI_ShipControl {
    public Transform target;

    // If we are within this distance to the target, we should attack
    [SerializeField]
    private float attackingDistance = 4.0f;

    // Field of view (in degrees)
    [SerializeField]
    private float fov = 50.0f;

    // Rear field of view (For fleeing; To know when the target is behind us)
    [SerializeField]
    private float rearFov = 10.0f;

    // These values are updated once every frame
    private Vector2 directionToTarget;
    private float distanceToTarget;

    // An imaginary target to help us wander
    private Vector2 wanderTarget;

    // The radius that we will attempt to reach while wandering
    private float wanderTargetRadius = 2.0f;

    // The distance that random targets will be placed from us
    private float wanderTargetDistance = 8.0f;

    // The maximum angle that we will turn while wandering (eg. 180 means
    // that there is a chance that we will switch directions completely)
    private float wanderMaxRotationAngle = 90.0f;

    private delegate void AI_behaviour();
    private AI_behaviour delegatedBehaviour;

    // Use this for initialization
    protected void Start() {
        wanderTarget = transform.position;
        delegatedBehaviour = Seek;
        shipControl = gameObject.GetComponent<ShipControl>();
    }

    protected void Update() {
		targetShipList = getNearbyEnemies();
		ShipControl bestTarget = getBestTarget(targetShipList);
		if(bestTarget != null){
			target = bestTarget.transform;
		}
        if (target == null) {
            delegatedBehaviour = Wander;
        } else {
            // Calculate commonly required properties
            directionToTarget = target.position - transform.position;
            distanceToTarget = directionToTarget.magnitude;
            directionToTarget.Normalize();

            if (delegatedBehaviour == Wander) {
                delegatedBehaviour = Seek;
            }
        }

        delegatedBehaviour();
    }

    private void Wander() {
        // If we have reached the wander target, then calculate a new one
        if (Vector2.Distance(transform.position, wanderTarget) <= wanderTargetRadius) {
            wanderTarget = transform.position + wanderTargetDistance * (Quaternion.Euler(0.0f, 0.0f, Random.Range(-wanderMaxRotationAngle, wanderMaxRotationAngle)) * transform.up);
        }
        RotateTowards(wanderTarget);
        MoveForward();
    }

    private void Flee() {
        // Seek the target once we are fully recharged
        if (IsEnergyRecharged()) {
            delegatedBehaviour = Seek;
        } else {
            // Move forwards if the target is behind us
            if (IsTargetWithinRearFov()) {
                MoveForward();
            } else {
                StopMovingForward();

                // Rotate away from the target
                RotateTowards((Vector2)transform.position - directionToTarget);
            }
        }
    }

    private void Attack() {
        // Flee is our energy is too low
        if (IsEnergyLow()) {
            CeaseFirePrimaryWeapon();
            delegatedBehaviour = Flee;
        } else {
            // Check if we have drifted too far from the target
            if (IsTargetWithinAttackingDistance()) {
                if (IsTargetWithinFov()) {
                    FirePrimaryWeapon();
                } else {
                    CeaseFirePrimaryWeapon();
                    RotateTowards(target.position);
                }
            } else {
                CeaseFirePrimaryWeapon();
                delegatedBehaviour = Seek;
            }
        }
    }

    private void Seek() {
        // Attack if we are close to the target
        if (IsTargetWithinAttackingDistance()) {
            delegatedBehaviour = Attack;
        } else {
            // Move towards target if it is within our fov
            if (IsTargetWithinFov()) {
                MoveForward();
            } else {
                StopMovingForward();

                // Rotate towards the target
                RotateTowards(target.position);
            }
        }
    }

    private void RotateTowards(Vector2 position) {
        shipControl.updateRotation(position);
    }

    private void MoveForward() {
        shipControl.activateMainEngines();
        shipControl.cutSpaceBrake();
    }

    private void StopMovingForward() {
        shipControl.cutMainEngines();
        shipControl.activateSpaceBrake();
    }

    private void FirePrimaryWeapon() {
        shipControl.firePrimaryWeapons();
    }

    private void CeaseFirePrimaryWeapon() {
        shipControl.ceaseFirePrimaryWeapons();
    }

    private bool IsTargetWithinAttackingDistance() {
        return distanceToTarget <= attackingDistance;
    }

    private bool IsTargetWithinFov() {
        return Vector2.Angle(transform.up, directionToTarget) <= (fov / 2.0f);
    }

    private bool IsTargetWithinRearFov() {
        return Vector2.Angle(-transform.up, directionToTarget) <= (rearFov / 2.0f);
    }

    private bool IsEnergyLow() {
        return shipControl.energy < (shipControl.maxEnergy / 10.0f);
    }

    private bool IsEnergyRecharged() {
        return shipControl.energy >= shipControl.maxEnergy;
    }
}	