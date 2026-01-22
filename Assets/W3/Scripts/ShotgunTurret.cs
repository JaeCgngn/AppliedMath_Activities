using UnityEngine;

public class ShotgunTurret : MonoBehaviour
{
    public Transform player;

    public float detectionRange = 6f;
    public float coneAngle = 60f; // total cone angle
    public GameObject bulletPrefab;
    public Transform firePoint; 

    [Header("Shooting")]
    public int bulletCount = 5;
    public float spreadAngle = 30f;
    public float fireRate = 0.5f;

    float nextFireTime;

    void Update()
    {
        if (PlayerInCone())
        {
            AimAtPlayer();

            if (Time.time >= nextFireTime)
            {
                ShootCone();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    bool PlayerInCone()
    {
        Vector2 toPlayer = (Vector2)player.position - (Vector2)transform.position;

        // 1. Range check
        if (toPlayer.sqrMagnitude > detectionRange * detectionRange)
            return false;

        // 2. Dot product check
        Vector2 forward = transform.right.normalized;
        Vector2 dir = toPlayer.normalized;

        float dot = Vector2.Dot(forward, dir);
        float cosThreshold = Mathf.Cos(coneAngle * 0.5f * Mathf.Deg2Rad);

        return dot >= cosThreshold;
    }

    void AimAtPlayer()
    {
        Vector2 dir = (Vector2)player.position - (Vector2)transform.position;
        transform.right = dir;
    }

    void ShootCone()
    {
        int bulletCount = 5;
        float spread = 30f;

        float start = -spread / 2f;
        float step = spread / (bulletCount - 1);

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = start + step * i;
            Vector2 dir = Quaternion.Euler(0, 0, angle) * transform.right;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }
    }

    void ShootSpread()
    {
        float startAngle = -spreadAngle * 0.5f;
        float step = spreadAngle / (bulletCount - 1);

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = startAngle + step * i;

            Vector2 dir = Quaternion.Euler(0, 0, angle) * transform.right;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<W3Bullet>().direction = dir.normalized;
        }
    }

    void OnDrawGizmosSelected()
{
    Gizmos.color = Color.red;

    Vector3 left = Quaternion.Euler(0, 0, -coneAngle / 2f) * transform.right;
    Vector3 right = Quaternion.Euler(0, 0, coneAngle / 2f) * transform.right;

    Gizmos.DrawRay(transform.position, left * detectionRange);
    Gizmos.DrawRay(transform.position, right * detectionRange);
    Gizmos.DrawRay(transform.position, transform.right * detectionRange);
}
}
