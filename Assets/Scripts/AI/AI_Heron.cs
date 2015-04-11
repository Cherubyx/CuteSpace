using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ShipControl))]
public class AI_Heron : MonoBehaviour {
    public Transform target;

    // We will be satisfied when this radius is reached
    [SerializeField]
    private float targetRadius = 1.0f;

    // Field of view (in degrees)
    [SerializeField]
    private float fov = 50.0f;

    // These values are updated once every frame
    private Vector2 directionToTarget;
    private float distanceToTarget;

    private delegate void AI_behaviour();
    private AI_behaviour delegatedBehaviour;

    private ShipControl shipControl;

    // Use this for initialization
    protected void Start() {
        delegatedBehaviour = Seek;
        shipControl = gameObject.GetComponent<ShipControl>();
    }

    protected void Update() {
        if (target != null) {
            // Calculate commonly required properties
            directionToTarget = target.position - transform.position;
            distanceToTarget = directionToTarget.magnitude;
            directionToTarget.Normalize();

            delegatedBehaviour();
        }
    }

    private void Seek() {
        if (IsWithinTargetRadius()) {
            delegatedBehaviour = Align;
            StopMovingForward();
            return;
        }

        // Move towards target if it is within our fov
        if (IsTargetWithinFov()) {
            MoveForward();
        } else {
            StopMovingForward();

            // Rotate towards the target
            RotateTowards(target.position);
        }
    }

    private void Align() {
        if (!IsWithinTargetRadius()) {
            delegatedBehaviour = Seek;
        } else {
            // If we are here, then we are in our slot position
            // and we need to align to the slot's orientation
            RotateTowards(transform.position + target.up);
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

    private bool IsTargetWithinFov() {
        return Vector2.Angle(transform.up, directionToTarget) <= (fov / 2.0f);
    }

    private bool IsWithinTargetRadius() {
        return distanceToTarget < targetRadius;
    }
}