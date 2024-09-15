using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadSpawner : MonoBehaviour
{
    public GameObject roadPrefab; // Yolu temsil eden prefab
    public float roadLength = 10f; // Yol segmentinin uzunluðu
    public float spawnInterval = 2f; // Yol segmentinin doðma süresi (saniye cinsinden)
    public float roadLifetime = 5f; // Yol segmentinin yok olma süresi (saniye cinsinden)

    private float spawnZ = 0.0f; // Ýlk yol segmentinin spawnlanacaðý Z pozisyonu
    private Queue<GameObject> activeRoads = new Queue<GameObject>(); // Aktif yol segmentlerini tutacak

    void Start()
    {
        StartCoroutine(SpawnRoadsCoroutine());
    }

    private IEnumerator SpawnRoadsCoroutine()
    {
        while (true)
        {
            SpawnRoad(); // Yeni bir yol segmenti oluþtur
            yield return new WaitForSeconds(spawnInterval); // Bir sonraki yol segmentinin spawnlanmasýný bekle
        }
    }

    private void SpawnRoad()
    {
        // X ekseninde -90 derece ve Y ekseninde 90 derece döndürülmüþ yol segmentini oluþtur
        Quaternion rotation = Quaternion.Euler(-90f, 90f, 0f);
        GameObject road = Instantiate(roadPrefab, Vector3.forward * spawnZ, rotation);
        activeRoads.Enqueue(road);
        spawnZ += roadLength;

        // Yol segmentinin belirli bir süre sonra yok olmasýný saðla
        StartCoroutine(DestroyRoadAfterTime(road, roadLifetime));
    }

    private IEnumerator DestroyRoadAfterTime(GameObject road, float delay)
    {
        yield return new WaitForSeconds(delay); // Belirtilen süreyi bekle
        if (activeRoads.Contains(road))
        {
            activeRoads.Dequeue(); // Kuyruktan kaldýr
            Destroy(road); // Yol segmentini yok et
        }
    }
}
