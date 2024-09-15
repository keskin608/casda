using UnityEngine;

public class WheelDrive : MonoBehaviour
{
    public WheelCollider frontRightWheelCollider;
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;

    public Transform frontRightWheel;
    public Transform frontLeftWheel;
    public Transform rearRightWheel;
    public Transform rearLeftWheel;

    public float maxMotorTorque = 1500f; // Maksimum motor torku
    public float maxSteerAngle = 30f; // Maksimum direksiyon açýsý

    private void FixedUpdate()
    {
        // Ýleri-geri hareket
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        frontRightWheelCollider.motorTorque = motor;
        frontLeftWheelCollider.motorTorque = motor;

        // Direksiyon kontrolü
        float steering = maxSteerAngle * Input.GetAxis("Horizontal");
        frontRightWheelCollider.steerAngle = steering;
        frontLeftWheelCollider.steerAngle = steering;

        // Görsel tekerlek dönüþü
        UpdateWheelPoses();
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontRightWheelCollider, frontRightWheel);
        UpdateWheelPose(frontLeftWheelCollider, frontLeftWheel);
        UpdateWheelPose(rearRightWheelCollider, rearRightWheel);
        UpdateWheelPose(rearLeftWheelCollider, rearLeftWheel);
    }

    private void UpdateWheelPose(WheelCollider collider, Transform transform)
    {
        Vector3 pos;
        Quaternion quat;
        collider.GetWorldPose(out pos, out quat);
        transform.position = pos;
        transform.rotation = quat;
    }
}
