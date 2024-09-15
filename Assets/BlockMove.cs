using UnityEngine;

public class RotateOnXAxis : MonoBehaviour
{
    public float rotationSpeed = 100f;  // D�nd�rme h�z�
    public bool rotatePositiveX = true;  // D�n�� y�n�: true -> +X, false -> -X

    void Update()
    {
        // E�er rotatePositiveX true ise +X y�n�nde, de�ilse -X y�n�nde d�nd�r
        float direction = rotatePositiveX ? 1f : -1f;

        // X ekseninde, local space (yerel) olarak sabit bir h�zla d�nd�rme
        transform.Rotate(Vector3.right * rotationSpeed * direction * Time.deltaTime, Space.Self);
    }
}
