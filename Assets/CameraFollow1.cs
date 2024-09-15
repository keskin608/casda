using UnityEngine;

public class CameraFollow1 : MonoBehaviour
{
    public Transform target;           // Takip edilecek ara� (target) transformu
    public Vector3 offset;             // Kameran�n ara�tan olan mesafesi
    public float smoothSpeed = 0.125f; // Kameran�n yumu�ak hareket etme h�z�
    public LayerMask collisionMask;    // Kameran�n �arp��may� kontrol edece�i katman

    private Vector3 currentOffset;

    void Start()
    {
        currentOffset = offset;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + currentOffset; // �stenen pozisyon
        Vector3 direction = desiredPosition - target.position;

        RaycastHit hit;

        // Kamera ile hedef aras�ndaki �arp��may� kontrol et
        if (Physics.Raycast(target.position, direction, out hit, offset.magnitude, collisionMask))
        {
            // �arp��ma varsa, kameray� engelden uzak tut
            currentOffset = direction.normalized * (hit.distance - 0.5f); // K���k bir tampon mesafe ekle
        }
        else
        {
            // �arp��ma yoksa, kameray� normal pozisyonuna geri getir
            currentOffset = Vector3.Lerp(currentOffset, offset, smoothSpeed);
        }

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, target.position + currentOffset, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target); // Kameran�n araca bakmas�n� sa�lar
    }
}
