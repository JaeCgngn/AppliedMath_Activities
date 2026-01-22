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

    [Header("Renderer")]

    public float viewDistance = 5f;
    public LineRenderer rightLine;
    public LineRenderer leftLine;

    public LineRenderer coneLine;

    public bool showLines = true;

    public int segments = 20;

    void Start()
    {

        CreateLineRenderers();
    }

    void CreateLineRenderers()
    {
        if (rightLine == null)
        {
            GameObject rightObj = new GameObject("RightLine");
            rightObj.transform.parent = transform;
            rightLine = rightObj.AddComponent<LineRenderer>();
            rightLine.positionCount = 2;
            rightLine.useWorldSpace = true;
            rightLine.material = new Material(Shader.Find("Sprites/Default"));
            rightLine.startColor = Color.yellow;
            rightLine.endColor = Color.yellow;

            rightLine.startWidth = 0.05f;
            rightLine.endWidth = 0.05f;
        }

        if (leftLine == null)
        {
            GameObject leftObj = new GameObject("LeftLine");
            leftObj.transform.parent = transform;
            leftLine = leftObj.AddComponent<LineRenderer>();
            leftLine.positionCount = 2;
            leftLine.useWorldSpace = true;
            leftLine.material = new Material(Shader.Find("Sprites/Default"));
            leftLine.startColor = Color.yellow;
            leftLine.endColor = Color.yellow;

            leftLine.startWidth = 0.05f;
            leftLine.endWidth = 0.05f;
        }

        if (coneLine == null)
        {
            GameObject coneObj = new GameObject("ConeLine");
            coneObj.transform.parent = transform;
            coneLine = coneObj.AddComponent<LineRenderer>();
            coneLine.positionCount = segments + 1;
            coneLine.useWorldSpace = true;
            coneLine.material = new Material(Shader.Find("Sprites/Default"));
            coneLine.startColor = Color.yellow;
            coneLine.endColor = Color.yellow;

            coneLine.startWidth = 0.05f;
            coneLine.endWidth = 0.05f;
        }
    }


    void Update()
    {

        //TurretRotation(player.position);
        TurretDetection();
        DrawLines();
        DrawCone();
        ToggleLines();


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


    void ToggleLines()
    {
        if (rightLine != null) rightLine.enabled = showLines;
        if (leftLine != null) leftLine.enabled = showLines;
        if (coneLine != null) coneLine.enabled = showLines;
    }

    void DrawLines()
    {

        if (!showLines) return;

        Vector3 rightBoundary = Quaternion.Euler(0, 0, viewAngle / 2f) * transform.right;
        Vector3 leftBoundary = Quaternion.Euler(0, 0, -viewAngle / 2f) * transform.right;

        rightLine.SetPosition(0, transform.position);
        rightLine.SetPosition(1, transform.position + rightBoundary * detectionRange);

        leftLine.SetPosition(0, transform.position);
        leftLine.SetPosition(1, transform.position + leftBoundary * detectionRange);
    }

    void DrawCone()
    {
        if (!showLines || coneLine == null) return;

        coneLine.positionCount = segments + 1;
        float angleStep = viewAngle / segments;

        for (int i = 0; i <= segments; i++)
        {
            float angle = -viewAngle / 2 + angleStep * i;
            Vector3 dir = Quaternion.Euler(0, 0, angle) * transform.right;

            coneLine.SetPosition(i, transform.position + dir * detectionRange);
        }
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
