using System.Collections;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("[Spawner] Starting enemy spawn");
            StartCoroutine(SpawnEnemies(enemyPrefab, 1, 0.5f));
        }
    }

    private IEnumerator SpawnEnemies(GameObject enemyPrefab, int count, float delay)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            Debug.Log($"[Spawner] Spawned enemy {i + 1}/{count}");

            yield return new WaitForSeconds(delay);
        }

        Debug.Log("[Spawner] Finished spawning enemies");
    }
}
