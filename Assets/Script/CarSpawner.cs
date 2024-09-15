using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs; // Arabalar�n prefablerini buraya ekle
    public float spawnInterval = 2f; // Arabalar�n ka� saniyede bir spawn olaca��n� belirle
    public Transform[] spawnPoints; // Arabalar�n spawn olaca�� noktalar� belirle
    public float carSpeed = 10f; // Arabalar�n hareket h�z�

    private float timeSinceLastSpawn;

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnCar();
            timeSinceLastSpawn = 0;
        }
    }

    void SpawnCar()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        int carIndex = Random.Range(0, carPrefabs.Length);

        // Yeni arabay� spawn et
        GameObject spawnedCar = Instantiate(carPrefabs[carIndex], spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);

        // Arabay� ileri hareket ettirmek i�in CarMovement scriptini ekle
        CarMovement carMovement = spawnedCar.AddComponent<CarMovement>();
        carMovement.speed = carSpeed;
    }
}

public class CarMovement : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
