using UnityEngine;
using System.Collections;

public struct AvoidInfo {
    public float direction;
    public RaycastHit2D hit;
}

[RequireComponent(typeof(Rigidbody2D))]
public class AI_CollisionAvoidance : MonoBehaviour {
    [SerializeField]
    private float maxLookAheadDistance = 6.0f;

    [SerializeField]
    private float minLookAheadDistance = 3.0f;

    [SerializeField]
    private float lookAheadThresholdSpeed = 1.0f;

    [SerializeField]
    private float circleCastRadius = 2.0f;

    private Rigidbody2D rigidbody;
    private Collider2D collider;

	protected void Awake () {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
	}
	
	protected void Update () {
        float speed = rigidbody.velocity.magnitude;

        // If we are barely moving, there will be nothing to avoid
        if (speed <= 0.0001f) return;

        // If we are moving slower than the threshold, we'll check for collisions at a shorter distance
        float lookAheadDistance = (speed < lookAheadThresholdSpeed) ? minLookAheadDistance : maxLookAheadDistance;

        Vector2 from = (Vector2)transform.position + rigidbody.velocity.normalized * lookAheadDistance;
        RaycastHit2D hit = Physics2D.CircleCast(from, circleCastRadius, -rigidbody.velocity, lookAheadDistance);

        // Checks if the CircleCast hit anything other than ourself.
        // If this is true, then we will send a message to all scripts on
        // this gameobject that implement OnAvoidCollision.
        if (hit.collider != collider) {
            AvoidInfo avoidInfo;

            avoidInfo.direction = AngleDirection(rigidbody.velocity, hit.centroid, Vector3.forward);
            avoidInfo.hit = hit;
            
            SendMessage("OnAvoidCollision", avoidInfo, SendMessageOptions.DontRequireReceiver);
        }
	}

    private float AngleDirection(Vector3 forward, Vector3 target, Vector3 up) {
        Vector3 p = Vector3.Cross(forward, target);
        float d = Vector3.Dot(p, up);

        if (d > 0.0f) return 1.0f;
        if (d < 0.0f) return -1.0f;
  
        return 0.0f;
    }

    void OnDrawGizmos() {
        Gizmos.DrawRay(transform.position, rigidbody.velocity);
    }
}
