using UnityEngine;

public class RotateOnXAxis : MonoBehaviour
{
    public float rotationSpeed = 100f;  // Döndürme hýzý
    public bool rotatePositiveX = true;  // Dönüþ yönü: true -> +X, false -> -X

    void Update()
    {
        // Eðer rotatePositiveX true ise +X yönünde, deðilse -X yönünde döndür
        float direction = rotatePositiveX ? 1f : -1f;

        // X ekseninde, local space (yerel) olarak sabit bir hýzla döndürme
        transform.Rotate(Vector3.right * rotationSpeed * direction * Time.deltaTime, Space.Self);
    }
}
