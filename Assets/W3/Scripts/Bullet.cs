using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public float lifeTime = 5f;

    [Header("Detection")]
    public Transform player;
    public float hitDistance = 0.5f;

    void Update()
    {
        transform.position += direction.normalized * speed * Time.deltaTime;

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0f)
        {
            Destroy(gameObject);
        }

        if (!player)
        {
            return;
        }

    }
}
