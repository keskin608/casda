using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    public void GetInput()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
        m_brakeInput = Input.GetKey(KeyCode.Space) ? 1 : 0; // Space tuşuna basılıysa fren yapılacak
    }

    private void Steer()
    {
        // Direksiyon açısını maxSteerAngle ile sınırlıyoruz
        m_steeringAngle = maxSteerAngle * m_horizontalInput;
        m_steeringAngle = Mathf.Clamp(m_steeringAngle, -maxSteerAngle, maxSteerAngle);
        frontDriverW.steerAngle = m_steeringAngle;
        frontPassengerW.steerAngle = m_steeringAngle;
    }

    private void Accelerate()
    {
        // W tuşu ileri hareket, S tuşu geri hareket sağlayacak.
        frontDriverW.motorTorque = -m_verticalInput * motorForce;
        frontPassengerW.motorTorque = -m_verticalInput * motorForce;

        ApplyBraking(); // Fren kontrolü
    }

    private void ApplyBraking()
    {
        if (m_brakeInput > 0)
        {
            frontDriverW.brakeTorque = brakeForce;
            frontPassengerW.brakeTorque = brakeForce;
            rearDriverW.brakeTorque = brakeForce;
            rearPassengerW.brakeTorque = brakeForce;
        }
        else
        {
            frontDriverW.brakeTorque = 0;
            frontPassengerW.brakeTorque = 0;
            rearDriverW.brakeTorque = 0;
            rearPassengerW.brakeTorque = 0;
        }
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontDriverW, frontDriverT);
        UpdateWheelPose(frontPassengerW, frontPassengerT);
        UpdateWheelPose(rearDriverW, rearDriverT);
        UpdateWheelPose(rearPassengerW, rearPassengerT);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }

    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_brakeInput;
    private float m_steeringAngle;

    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;
    public float maxSteerAngle = 30; // Direksiyon açısını 30 dereceye sınırlıyoruz
    public float motorForce = 50;
    public float brakeForce = 1000; // Fren torku
}
