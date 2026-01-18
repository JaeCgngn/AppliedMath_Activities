using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header ("Reference")]
    public Transform player;
    public GameObject bullet;
    public GameObject firePoint;


    public float detectionRange;


    void TurretRotation(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void Update()
    {
        TurretRotation(player.position);
    }

    public void TurretDetection()
    {
        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        if(distance < detectionRange)
        {
            
        }
    }

}
