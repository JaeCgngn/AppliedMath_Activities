using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header ("Prefabs")]
    public Transform player;
    public GameObject bullet;
    public GameObject firePoint;

    [Header("Area Sensors")]
    public float detectionRange = 10f;
    public float fireAngle = 10f;

    [Header("Rotation")]
    public float turnSpeed = 180f;

    [Header("Firing")]
    public float fireCooldown = 1.5f;

    private float fireTimer;


    void Update()
    {
        if (!player) return;

        fireTimer -= Time.deltaTime;

        Vector3 towardsPlayer = player.position - transform.position;
        towardsPlayer.y = 0f;

        float distance = towardsPlayer.magnitude;
        if (distance > detectionRange)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(towardsPlayer);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        float angle = Vector3.Angle(transform.forward, towardsPlayer);


    }

    void Fire()
    {
        GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation);
        projectile.GetComponent<bullet>().direction = firePoint.forward;
    }

}
