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
        AvoidInfo avoidInfo = AvoidCast();

        if (avoidInfo.hit.collider == null) return;

        // Any scripts on this game object that have 'OnAvoidCollision' will be called
        SendMessage("OnAvoidCollision", avoidInfo, SendMessageOptions.DontRequireReceiver);
	}

    /// <summary>
    /// This method is called per update, but also can be used manually
    /// from an external script.
    /// TODO: Consider refactoring collision avoidance into a helper class
    /// that can be used when needed instead of sending messages, which is
    /// an intense operation.
    /// </summary>
    /// <returns></returns>
    public AvoidInfo AvoidCast() {
        float speed = rigidbody.velocity.magnitude;

        // If we are moving slower than the threshold, we'll check for collisions at a shorter distance
        float lookAheadDistance = (speed < lookAheadThresholdSpeed) ? minLookAheadDistance : maxLookAheadDistance;

        Vector2 castDirection = (rigidbody.velocity.magnitude <= 0.001f) ? (Vector2)transform.up : rigidbody.velocity.normalized;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position + transform.up, circleCastRadius, castDirection, lookAheadDistance);

        // hits[1] is the closest hit object if it exists
        AvoidInfo avoidInfo;

        // If the number of hits is 1, then we hit ourself.
        // (We are assuming that objects using this script have a collider)
        if (hits.Length <= 1) {
            avoidInfo.direction = 0.0f;
            avoidInfo.hit = new RaycastHit2D();
        } else {
            // Get the sign of the direction of rotation to avoid collision
            avoidInfo.direction = AngleDirection(hits[1].centroid, rigidbody.velocity, Vector3.forward);
            avoidInfo.hit = hits[1];
        }

        return avoidInfo;
    }

    private float AngleDirection(Vector3 forward, Vector3 target, Vector3 up) {
        Vector3 p = Vector3.Cross(forward, target);
        float d = Vector3.Dot(p, up);

        if (d > 0.0f) return 1.0f;
        if (d < 0.0f) return -1.0f;
  
        return 0.0f;
    }

    void OnDrawGizmos() {
        if (rigidbody == null) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, rigidbody.velocity);

        float speed = rigidbody.velocity.magnitude;

        // If we are moving slower than the threshold, we'll check for collisions at a shorter distance
        float lookAheadDistance = (speed < lookAheadThresholdSpeed) ? minLookAheadDistance : maxLookAheadDistance;

        Vector2 from = (Vector2)transform.position + rigidbody.velocity.normalized * lookAheadDistance;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(from, circleCastRadius);
    }
}
