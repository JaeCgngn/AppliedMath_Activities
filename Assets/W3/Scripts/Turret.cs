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

    public float viewAngle = 45f;



    void Update()
    {

        //TurretRotation(player.position);
        TurretDetection();


    }

    public void TurretRotation(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float currentAngle = transform.eulerAngles.z;

        float newAngle = Mathf.MoveTowardsAngle(
        currentAngle,
        targetAngle,
        turnSpeed * Time.deltaTime
        );

        transform.rotation = Quaternion.Euler(0f, 0f, newAngle);

    }




    public void TurretDetection()
    {

        // Check if player is within detection range

        Vector3 direction = player.position - transform.position; // Vector from turret to player
        float distance = direction.magnitude; // Calculate distance

        if (distance > detectionRange) // Adjust detection range as needed
        {
            StopFiring();
            return; // Player is out of range
        }

        direction.Normalize(); // Normalize direction vector

        float dot = Vector3.Dot(transform.right, direction); // Assuming turret's forward is along the x-axis
        float threshhold = Mathf.Cos(viewAngle * 0.5f * Mathf.Deg2Rad); // Calculate threshold based on view angle



        if (dot > threshhold)
        {

            Debug.Log("dot: " + dot + " threshhold: " + threshhold);

            if (!playerInRange)
            {
                playerInRange = true;
            }

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
            StopFiring();
        }


    }

    void StopFiring()
    {
        if (!playerInRange) return;
        playerInRange = false;
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;

            Debug.Log("Stops Firing");
        }

        Debug.Log("Turret Detection Inactive");

    }




    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.yellow;
        Vector3 rightBoundary = Quaternion.Euler(0, 0, viewAngle / 2) * transform.right;
        Vector3 leftBoundary = Quaternion.Euler(0, 0, -viewAngle / 2) * transform.right;

        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * detectionRange);
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * detectionRange);
    }





}
