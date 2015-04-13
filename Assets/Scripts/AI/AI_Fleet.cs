using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manages a fleet. The transform of this
/// class is used to represent the 'anchor' point of
/// the fleet. The target represents where the anchor point
/// and its associated slots are moving to. The anchor point
/// will attempt to reach the target and the slots will stay
/// relative to the anchor.
/// 
/// Important: At the moment, this works only with AI
/// having the AI_Heron script attached.
/// </summary>
public class AI_Fleet : MonoBehaviour {
    public Transform target;

    [SerializeField]
    private float targetRadius = 1.0f;

    // This speed should depend on the speed of the member
    // ships. If the target moves to slowly, the behaviour
    // of the ships be look odd because they will be transitioning
    // between arrive and seek behaviours.
    [SerializeField]
    private float targetSpeed = 1.0f;

    [System.Serializable]
    struct FleetMember {
        public Transform member;
        public Transform slot;
    }

    [SerializeField]
    private List<FleetMember> members;

    // These values are updated once every frame
    private Vector2 directionToTarget;
    private float distanceToTarget;

    private delegate void AI_behaviour();
    private AI_behaviour delegatedBehaviour;

    protected void Start() {
        delegatedBehaviour = Seek;

        // Assign members to their respective slots
        foreach (FleetMember m in members) {
            m.member.GetComponent<AI_Heron>().target = m.slot;
        }
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
        if (distanceToTarget < targetRadius) return;

        transform.position = (Vector2)transform.position + targetSpeed * directionToTarget * Time.deltaTime;
        transform.up = directionToTarget;
    }

    protected void OnDrawGizmos() {
        foreach (FleetMember m in members) {
            Gizmos.DrawWireSphere(m.slot.position, 0.5f);
        }
    }
}
