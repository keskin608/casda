using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs; // Arabalarýn prefablerini buraya ekle
    public float spawnInterval = 2f; // Arabalarýn kaç saniyede bir spawn olacaðýný belirle
    public Transform[] spawnPoints; // Arabalarýn spawn olacaðý noktalarý belirle
    public float carSpeed = 10f; // Arabalarýn hareket hýzý

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

        // Yeni arabayý spawn et
        GameObject spawnedCar = Instantiate(carPrefabs[carIndex], spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);

        // Arabayý ileri hareket ettirmek için CarMovement scriptini ekle
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
