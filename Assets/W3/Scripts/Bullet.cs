using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 0.5f;

    void Update()
    {

        BulletMovement();

    }

    void BulletMovement()
    {
        Vector3 direction = transform.right;

        transform.position += direction * speed * Time.deltaTime;

        Destroy(gameObject, lifetime);
    }

}
