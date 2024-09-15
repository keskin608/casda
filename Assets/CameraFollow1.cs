using UnityEngine;

public class CameraFollow1 : MonoBehaviour
{
    public Transform target;           // Takip edilecek araç (target) transformu
    public Vector3 offset;             // Kameranýn araçtan olan mesafesi
    public float smoothSpeed = 0.125f; // Kameranýn yumuþak hareket etme hýzý
    public LayerMask collisionMask;    // Kameranýn çarpýþmayý kontrol edeceði katman

    private Vector3 currentOffset;

    void Start()
    {
        currentOffset = offset;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + currentOffset; // Ýstenen pozisyon
        Vector3 direction = desiredPosition - target.position;

        RaycastHit hit;

        // Kamera ile hedef arasýndaki çarpýþmayý kontrol et
        if (Physics.Raycast(target.position, direction, out hit, offset.magnitude, collisionMask))
        {
            // Çarpýþma varsa, kamerayý engelden uzak tut
            currentOffset = direction.normalized * (hit.distance - 0.5f); // Küçük bir tampon mesafe ekle
        }
        else
        {
            // Çarpýþma yoksa, kamerayý normal pozisyonuna geri getir
            currentOffset = Vector3.Lerp(currentOffset, offset, smoothSpeed);
        }

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, target.position + currentOffset, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target); // Kameranýn araca bakmasýný saðlar
    }
}
