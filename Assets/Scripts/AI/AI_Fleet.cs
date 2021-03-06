﻿using UnityEngine;
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
	private List<Transform> targetList;
	private int targetIndex = 0;

    [SerializeField]
    private float targetRadius = 1.0f;

    // This speed should depend on the speed of the member
    // ships. If the target moves to slowly, the behaviour
    // of the ships be look odd because they will be transitioning
    // between arrive and seek behaviours.
    [SerializeField]
    private float targetSpeed = 1.0f;

    [SerializeField]
    private float maxSlotDistanceFromMember = 5.0f;

    [System.Serializable]
    struct FleetMember {
        public Transform member;
        public Transform slot;

        public float Distance {
            get {
                return Vector2.Distance(member.position, slot.position);
            }
        }
    }

    [SerializeField]
    private List<FleetMember> members;

    // These values are updated once every frame
    private Vector2 directionToTarget;
    private float distanceToTarget;

    private delegate void AI_behaviour();
    private AI_behaviour delegatedBehaviour;

    protected void Start() {

		initializeTargetList();

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
        foreach (FleetMember m in members) {
            if (!IsMemberWithinSlotDistance(m)) return;
        }

        if (distanceToTarget < targetRadius) {
			if(targetList.Count > 1){
				targetIndex = (targetIndex+1) % targetList.Count;
				target= targetList[targetIndex];
			}
			else{
				return;
			}
		}

        transform.position = (Vector2)transform.position + targetSpeed * directionToTarget * Time.deltaTime;
        transform.up = directionToTarget;
    }

    private bool IsMemberWithinSlotDistance(FleetMember member) {
        return member.Distance <= maxSlotDistanceFromMember;
    }

    protected void OnDrawGizmos() {
        foreach (FleetMember m in members) {
            Gizmos.DrawWireSphere(m.slot.position, 0.5f);
        }
    }

	private void initializeTargetList(){
		//Currently just want the fleet to patrol the jump points
		JumpGate[] jumpgates = GameObject.FindObjectsOfType<JumpGate> ();
		targetList = new List<Transform>();

		foreach (JumpGate gate in jumpgates) {
			targetList.Add(gate.gameObject.transform);
		}

		if (targetList.Count > 0) {
			target = targetList[targetIndex];
		}

		/*
		if (jumpgates.Count > 0) {
			JumpGate farthestGate = jumpgates[0];
			foreach (JumpGate gate in jumpgates) {
				if(Vector2.Distance(this.transform.position,gate.transform.position) > Vector2.Distance(this.transform.position,farthestGate.transform.position)){
					farthestGate = gate;
				}
			}
			target = farthestGate;
		}
		*/
	}
}
