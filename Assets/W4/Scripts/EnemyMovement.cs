using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform targetPos;
    [SerializeField] private Transform controlPosition;
    [SerializeField] private float speed = 2f;

    private Vector3 startPos;
    private float targetReached;

    // Update is called once per frame
    void Update()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        if (targetReached >= 1f)
            return; // Reached the target

        // Increase t based on speed
        targetReached += Time.deltaTime * speed;


        // Quadratic Bezier using LERP
        Vector3 a = Vector3.Lerp(startPos, controlPosition.position, targetReached);
        Vector3 b = Vector3.Lerp(controlPosition.position, targetPos.position, targetReached);
        Vector3 position = Vector3.Lerp(a, b, targetReached);

        transform.position = position;

    }

}
