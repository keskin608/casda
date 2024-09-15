using UnityEngine;

public class CarControl : MonoBehaviour
{
    // PUBLIC

    public bool driveable = false;

    // Wheel Wrapping Objects
    public Transform frontLeftWheelWrapper;
    public Transform frontRightWheelWrapper;
    public Transform rearLeftWheelWrapper;
    public Transform rearRightWheelWrapper;

    // Wheel Meshes
    // Front
    public Transform frontLeftWheelMesh;
    public Transform frontRightWheelMesh;
    // Rear
    public Transform rearLeftWheelMesh;
    public Transform rearRightWheelMesh;

    // Wheel Colliders
    // Front
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    // Rear
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;

    public float maxTorque = 20f;
    public float brakeTorque = 100f;

    // max wheel turn angle;
    public float maxWheelTurnAngle = 30f; // degrees

    // car's center of mass
    public Vector3 centerOfMass = new Vector3(0f, 0f, 0f); // unchanged

    // GUI
    public float RO_speed; // READ ONLY (Debug)
    public float RO_EngineTorque; // READ ONLY (Debug)
    public float RO_SteeringAngleFL; // READ ONLY (Debug)
    public float RO_SteeringAngleFR; // READ ONLY (Debug)
    public float RO_BrakeTorque; // READ ONLY (Debug)

    // PRIVATE

    // acceleration increment counter
    private float torquePower = 0f;

    // turn increment counter
    private float steerAngle = 0f;

    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;

        // Configure wheel colliders
        ConfigureWheelCollider(wheelFL);
        ConfigureWheelCollider(wheelFR);
        ConfigureWheelCollider(wheelRL);
        ConfigureWheelCollider(wheelRR);
    }

    // Configure wheel colliders for suspension and damping
    void ConfigureWheelCollider(WheelCollider wheel)
    {
        JointSpring suspensionSpring = wheel.suspensionSpring;
        suspensionSpring.spring = 35000; // Suspension stiffness
        suspensionSpring.damper = 4500; // Suspension damping
        suspensionSpring.targetPosition = 0.5f; // Default position of the wheel

        wheel.suspensionSpring = suspensionSpring;
        wheel.suspensionDistance = 0.3f; // Maximum suspension distance
        wheel.wheelDampingRate = 1f; // Wheel damping rate
    }

    // Update wheel positions and rotations
    void UpdateWheelPoses(WheelCollider collider, Transform wheelMesh)
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        wheelMesh.position = position;
        wheelMesh.rotation = rotation;
    }

    // Visual updates
    void Update()
    {
        if (!driveable)
        {
            return;
        }

        // Update wheel positions and rotations
        UpdateWheelPoses(wheelFL, frontLeftWheelMesh);
        UpdateWheelPoses(wheelFR, frontRightWheelMesh);
        UpdateWheelPoses(wheelRL, rearLeftWheelMesh);
        UpdateWheelPoses(wheelRR, rearRightWheelMesh);
    }

    // Physics updates
    void FixedUpdate()
    {
        if (!driveable)
        {
            return;
        }

        // CONTROLS - FORWARD & REARWARD
        if (Input.GetKey(KeyCode.Space))
        {
            // BRAKE
            torquePower = 0f;
            wheelRL.brakeTorque = brakeTorque;
            wheelRR.brakeTorque = brakeTorque;
        }
        else
        {
            // SPEED
            torquePower = maxTorque * Mathf.Clamp(-Input.GetAxis("Vertical"), -1, 1); // Correct direction
            wheelRL.brakeTorque = 0f;
            wheelRR.brakeTorque = 0f;
        }

        // Apply torque
        wheelRR.motorTorque = torquePower;
        wheelRL.motorTorque = torquePower;

        Debug.Log("torquePower: " + torquePower);
        Debug.Log("brakeTorque RL: " + wheelRL.brakeTorque);
        Debug.Log("brakeTorque RR: " + wheelRR.brakeTorque);
        Debug.Log("steerAngle: " + steerAngle);

        // CONTROLS - LEFT & RIGHT
        // Apply steering to front wheels
        steerAngle = maxWheelTurnAngle * Input.GetAxis("Horizontal");
        wheelFL.steerAngle = steerAngle;
        wheelFR.steerAngle = steerAngle;

        // Debug info
        RO_BrakeTorque = wheelRL.brakeTorque;
        RO_SteeringAngleFL = wheelFL.steerAngle;
        RO_SteeringAngleFR = wheelFR.steerAngle;
        RO_EngineTorque = torquePower;

        // SPEED
        // Debug info
        RO_speed = GetComponent<Rigidbody>().velocity.magnitude;
    }
}
