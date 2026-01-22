using UnityEngine;

public class FireTurret : MonoBehaviour
{
    public Transform firePoint;
    public Transform player;

    public float range = 6f;
    public float coneAngle = 45f;

    LineRenderer lr;


    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.loop = false;
        lr.useWorldSpace = false;
    }
    void Update()
    {
        Vector3 toPlayer = player.position - firePoint.position;
        float distance = toPlayer.magnitude;

        if (distance > range) return;

        Vector3 forward = firePoint.forward.normalized;
        Vector3 dir = toPlayer.normalized;

        float dot = Vector3.Dot(forward, dir);
        float minDot = Mathf.Cos(coneAngle * Mathf.Deg2Rad);

        if (dot >= minDot)
        {
            player.GetComponent<PlayerMovement>().PlayerDie();
        }
    }
}
