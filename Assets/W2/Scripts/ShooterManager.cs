using UnityEngine;

public class ShooterManager : MonoBehaviour
{

    [Header("Shooter Settings")]
    public GameObject shooterPrefab;

    [Header("Formation Settings")]
    public float angleOffset = 0f; //modify to rotate 
    public float radius = 2.5f;
    public int shooterCount = 8;

    // Track the current shooter index
    int currentIndex = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key pressed");
            SpawnShooter();
        }
    }

    public void SpawnShooter()
    {
        if (currentIndex >= shooterCount)
            return;

        float angleStep = 360f / shooterCount; //degrees apart each shooter
        float angle = currentIndex * angleStep + angleOffset;  //angle for this shooter on its current index

        float rad = angle * Mathf.Deg2Rad;  //caluclate radians

        //spawn position 
        Vector3 spawnPos = transform.position + new Vector3(
            Mathf.Cos(rad) * radius,
            Mathf.Sin(rad) * radius,
            0f
        );

        GameObject shooter = Instantiate(shooterPrefab, spawnPos, Quaternion.identity, transform);

        Debug.Log($"Shooter {currentIndex} spawned");
        currentIndex++;

        angle += angleStep; //increment angle for next shooter

    }
}
