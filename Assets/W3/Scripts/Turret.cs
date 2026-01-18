using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    public Shooting shootingScript;

    [Header("Rotation")]
    public float turnSpeed = 180f;

    [Header("Detection")]

    public float detectionRange = 2f;

    public bool playerInRange = false;
    private Coroutine fireCoroutine;


    void Update()
    {

        //TurretRotation(player.position);
        TurretDetection();

    }

    public void TurretRotation(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);

    }

    public void TurretDetection()
    {

        // Check if player is within detection range

        Vector3 direction = player.position - transform.position; // Vector from turret to player
        float distance = direction.magnitude; // Calculate distance

        if (distance < detectionRange) // Adjust detection range as needed
        {


            if (!playerInRange)

                playerInRange = true;
            TurretRotation(player.position);

            if (fireCoroutine == null)
            {
                fireCoroutine = StartCoroutine(shootingScript.FireRoutine());

                Debug.Log("Starts Firing");
            }

            Debug.Log("Turret Detection Active");

        }

        else
        {
            if (playerInRange)
            {
                playerInRange = false;

                if (fireCoroutine != null)
                {
                    StopCoroutine(fireCoroutine);
                    fireCoroutine = null;

                    Debug.Log("Stops Firing");
                }

                Debug.Log("Turret Detection Inactive");

            }
        }


    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }



}
