using UnityEngine;
using UnityEngine.InputSystem;

public class W2PlayerMovement : MonoBehaviour
{
    [Header("References")]
    public ShooterManager shooterManager;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Detection")]
    public float detectRadius = 0.5f;
    public LayerMask powerUpLayer;


    // public GameObject rocketPrefab;
    // public float fireInterval = 2f;
    // public int rocketCount = 4;
    // public int maxCount = 8;
    // public float rocketSpeed = 10f;
    //private float fireTimer;


    void Start()
    {
        PlayerControl();
    }

    void Update()
    {
        PlayerControl();
        CollisionDetection();
    }

    void PlayerControl()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W)) moveY = 1f;
        if (Input.GetKey(KeyCode.S)) moveY = -1f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        Vector3 dir = new Vector3(moveX, moveY, 0f).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    void CollisionDetection()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            transform.position,
            detectRadius,
            powerUpLayer
        );

        if (hit != null)
        {
            Debug.Log("Power-up detected: " + hit.name);
            shooterManager.SpawnShooter();
            hit.GetComponent<PowerUps>()?.Collect();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }


    // void FireRockets()
    // {
    //     float angleStep = 360f / rocketCount;
    //     float startAngle = 45f;

    //     for (int i = 0; 1 < rocketCount; i++)
    //     {
    //         float angle = startAngle + angleStep * i;
    //         Vector3 dir = AngleToDirection(angle);

    //         GameObject rocket = Instantiate(rocketPrefab, transform.position, Quaternion.identity);

    //         rocket.GetComponent<Rocket>().SetDirection(dir);
    //     }

    //     Debug.Log("Rockets Spawned");
    // }

    // Vector3 AngleToDirection(float angle)
    // {
    //     float rad = angle * Mathf.Deg2Rad;
    //     return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f);

    // }

    // public void IncreaseRocketCount()
    // {
    //     rocketCount = Mathf.Min(rocketCount + 1, maxCount);
    // }


}
