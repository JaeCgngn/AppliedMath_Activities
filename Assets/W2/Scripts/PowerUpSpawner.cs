using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]

    public GameObject spawnerObject;
    public GameObject powerUpPrefab;
    public int powerUpCount = 10;
    public Vector2 mapSize = new Vector2(20, 20); // width and height of your map


    void Start()
    {
        SpawnPowerUp();
    }

    void SpawnPowerUp()
    {
        for (int i = 0; i < powerUpCount; i++)
        {
            Vector3 spawnPos = GetRandomSpawnPosition();
            Instantiate(powerUpPrefab, spawnPos, Quaternion.identity);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector2 offset = new Vector2(
            Random.Range(-mapSize.x / 2, mapSize.x / 2),
            Random.Range(-mapSize.y / 2, mapSize.y / 2)
        );

        return new Vector3(
            spawnerObject.transform.position.x + offset.x,
            spawnerObject.transform.position.y + offset.y,
            0
        );
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(mapSize.x, mapSize.y, 0));
    }


}