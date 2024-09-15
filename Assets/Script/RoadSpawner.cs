using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadSpawner : MonoBehaviour
{
    public GameObject roadPrefab; // Yolu temsil eden prefab
    public float roadLength = 10f; // Yol segmentinin uzunlu�u
    public float spawnInterval = 2f; // Yol segmentinin do�ma s�resi (saniye cinsinden)
    public float roadLifetime = 5f; // Yol segmentinin yok olma s�resi (saniye cinsinden)

    private float spawnZ = 0.0f; // �lk yol segmentinin spawnlanaca�� Z pozisyonu
    private Queue<GameObject> activeRoads = new Queue<GameObject>(); // Aktif yol segmentlerini tutacak

    void Start()
    {
        StartCoroutine(SpawnRoadsCoroutine());
    }

    private IEnumerator SpawnRoadsCoroutine()
    {
        while (true)
        {
            SpawnRoad(); // Yeni bir yol segmenti olu�tur
            yield return new WaitForSeconds(spawnInterval); // Bir sonraki yol segmentinin spawnlanmas�n� bekle
        }
    }

    private void SpawnRoad()
    {
        // X ekseninde -90 derece ve Y ekseninde 90 derece d�nd�r�lm�� yol segmentini olu�tur
        Quaternion rotation = Quaternion.Euler(-90f, 90f, 0f);
        GameObject road = Instantiate(roadPrefab, Vector3.forward * spawnZ, rotation);
        activeRoads.Enqueue(road);
        spawnZ += roadLength;

        // Yol segmentinin belirli bir s�re sonra yok olmas�n� sa�la
        StartCoroutine(DestroyRoadAfterTime(road, roadLifetime));
    }

    private IEnumerator DestroyRoadAfterTime(GameObject road, float delay)
    {
        yield return new WaitForSeconds(delay); // Belirtilen s�reyi bekle
        if (activeRoads.Contains(road))
        {
            activeRoads.Dequeue(); // Kuyruktan kald�r
            Destroy(road); // Yol segmentini yok et
        }
    }
}
