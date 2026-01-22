using UnityEngine;

public class TurretRange : MonoBehaviour
{
    public float range = 6f;
    public float coneAngle = 45f; // half-angle
    public int segments = 40;

    LineRenderer lr;

        void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.loop = false;
        lr.useWorldSpace = false;
    }
    void LateUpdate()
    {
        DrawCone();
    }

    void DrawCone()
    {
        lr.positionCount = segments + 2;

        lr.SetPosition(0, Vector3.zero);

        float startAngle = -coneAngle;
        float angleStep = (coneAngle * 2f) / segments;

        for (int i = 0; i <= segments; i++)
        {
            float angle = startAngle + angleStep * i;
            Vector3 dir = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            lr.SetPosition(i + 1, dir * range);
        }
    }

}