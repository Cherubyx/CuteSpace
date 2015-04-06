using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ShipControl))]
public class AI_PilotSeek : MonoBehaviour {

    private ShipControl shipControl;

    // The target to seek
    public Transform Target { get; set; }

    // The angle between the facing direction
    // and target, that we will attempt to achieve
    public float TargetAngle { get; set; }

    // The distance to the target that
    // we will attempt to achieve
    public float TargetRadius { get; set; }

    public Vector2 DirectionToTarget { get; set; }

    protected void Awake() {
        // Set up script references
        shipControl = GetComponent<ShipControl>();
    }

    protected void Update() {
        // Only seek if there is a target to seek
        if (Target != null) {
            DirectionToTarget = Target.position - transform.position;

            UpdateRotation();
            UpdatePosition();
        }
    }

    private void UpdateRotation() {
        // Don't rotate if we are facing the correct direction
        if (IsWithinTargetAngle()) return;

        float angleDirection = Mathf.Sign(Vector3.Dot(Vector3.back, Vector3.Cross(transform.up, DirectionToTarget)));

        if (angleDirection > 0)
            shipControl.applyClockwiseRotation();
        else
            shipControl.applyCounterClockwiseRotation();
    }

    private void UpdatePosition() {
        // Don't update if the target has been reached
        if (IsWithinTargetRadius()) return;

        if (!IsWithinTargetAngle()) return;

        shipControl.activateMainEngines();
    }

    private bool IsWithinTargetRadius() {
        return Vector2.Distance(transform.position, Target.position) <= TargetRadius;
    }

    private bool IsWithinTargetAngle() {
        return Vector2.Angle(transform.up, DirectionToTarget) <= TargetAngle;
    }
}
