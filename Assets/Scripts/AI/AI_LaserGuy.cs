using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_LaserGuy : MonoBehaviour {
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

    public Vector2 DirectionToTarget { get; set; }
    public float DistanceToTarget { get; set; }

    private Stack<Transform> targets;

    private delegate void AI_behaviour();
    private AI_behaviour delegatedBehaviour;

    private ShipControl shipControl;

    // Use this for initialization
    protected void Start() {
        targets = new Stack<Transform>();

        //TODO: remove test code
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Team2");
        foreach (GameObject e in enemies) {
            targets.Push(e.transform);
        }

        target = targets.Pop();

        delegatedBehaviour = Seek;
        shipControl = gameObject.GetComponent<ShipControl>();
    }

    protected void Update() {
        if (target == null) {
            // If there are no targets to attack, then we wander
            if (targets.Count > 0 ) {
                target = targets.Pop();
            } else {
                delegatedBehaviour = Wander;
            }
        } else {
            // Calculate commonly required properties
            DirectionToTarget = target.position - transform.position;
            DistanceToTarget = DirectionToTarget.magnitude;
            DirectionToTarget.Normalize();
        }

        delegatedBehaviour();
    }

    private void Wander() {
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
                RotateTowards((Vector2)transform.position - DirectionToTarget);
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
    }

    private void StopMovingForward() {
        shipControl.cutMainEngines();
    }

    private void FirePrimaryWeapon() {
        shipControl.firePrimaryWeapons();
    }

    private void CeaseFirePrimaryWeapon() {
        shipControl.ceaseFirePrimaryWeapons();
    }

    private bool IsTargetWithinAttackingDistance() {
        return DistanceToTarget <= attackingDistance;
    }

    private bool IsTargetWithinFov() {
        return Vector2.Angle(transform.up, DirectionToTarget) <= (fov / 2.0f);
    }

    private bool IsTargetWithinRearFov() {
        return Vector2.Angle(-transform.up, DirectionToTarget) <= (rearFov / 2.0f);
    }

    private bool IsEnergyLow() {
        return shipControl.energy < (shipControl.maxEnergy / 10.0f);
    }

    private bool IsEnergyRecharged() {
        return shipControl.energy >= shipControl.maxEnergy;
    }
}
