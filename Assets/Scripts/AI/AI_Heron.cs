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

    // Contains the latest info on an object
    // that we need to avoid
    private AvoidInfo avoidInfo;

    // When starting to avoid an object, this gameobject's
    // position will be stored here when the avoiding behaviour occurs.
    // This is so that we can figure out the distance we've traveled to 
    // avoid a single object.
    private Vector2 avoidStartPosition;

    // This is how far we will travel to avoid an object before
    // we start seeking the target again
    private float avoidTravelDistance = 2.0f;

    private delegate void AI_behaviour();
    private AI_behaviour delegatedBehaviour;

    private Rigidbody2D rigidbody;
    private ShipControl shipControl;
    private AI_CollisionAvoidance collisionAvoidance;

    // Use this for initialization
    protected void Start() {
        delegatedBehaviour = Seek;

        rigidbody = GetComponent<Rigidbody2D>();
        shipControl = GetComponent<ShipControl>();
        collisionAvoidance = GetComponent<AI_CollisionAvoidance>();
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
            StopMovingForward();
            delegatedBehaviour = Align;
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
            StopMovingForward();

            // If we are here, then we are in our slot position
            // and we need to align to the slot's orientation
            RotateTowards(transform.position + target.up);
        }
    }

    private void Avoiding() {
        if (IsWithinTargetRadius()) {
            StopMovingForward();
            delegatedBehaviour = Align;
            return;
        }

        // We check infront of us to make sure that nothing
        // is there. This should probably be refactored as
        // noted in the AI_CollisionAvoidance.cs script.
        AvoidInfo ai = collisionAvoidance.AvoidCast();

        if (ai.hit.collider == null) {
            if (Vector2.Distance(transform.position, avoidStartPosition) >= avoidTravelDistance) {
                delegatedBehaviour = Seek;
            } else {
                MoveForward();
            }
        }
    }

    private void RotateTowards(Vector2 position) {
        shipControl.updateRotation(position);
    }

    private void MoveForward() {
        shipControl.activateMainEngines();
        shipControl.cutRetroThrusters();
        shipControl.cutSpaceBrake();
    }

    private void MoveBackward() {
        shipControl.activateRetroThrusters();
    }

    private void StopMovingForward() {
        shipControl.cutMainEngines();
        shipControl.activateSpaceBrake();
    }

    private bool IsTargetWithinFov() {
        return Vector2.Angle(transform.up, directionToTarget) <= (fov / 2.0f);
    }

    private bool IsWithinTargetRadius() {
        return distanceToTarget <= targetRadius;
    }

    public void OnAvoidCollision(AvoidInfo avoidInfo) {
        this.avoidInfo = avoidInfo;
        avoidStartPosition = transform.position;

        Vector2 rotateTowards = transform.position + Quaternion.AngleAxis(-avoidInfo.direction * 50.0f, Vector3.forward) * rigidbody.velocity.normalized;
        RotateTowards(rotateTowards);
        StopMovingForward();

        delegatedBehaviour = Avoiding;
    }

    protected void OnCollisionEnter2D(Collision2D collision) {
        // If ever there is a collision (which ideally shouldn't happen), we
        // we attempt unstick ourself by moving backwards
        StopMovingForward();
        MoveBackward();
    }
}