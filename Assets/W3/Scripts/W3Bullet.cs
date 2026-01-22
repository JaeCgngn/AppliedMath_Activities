using UnityEngine;

public class W3Bullet : MonoBehaviour
{
    public Vector2 direction;
   public float speed = 10f;
    public float lifetime = 5f;

    void Update()
    {

        BulletMovement();

    }

    void BulletMovement()
    {
        // Vector3 direction = transform.right;

        // transform.position += direction * speed * Time.deltaTime;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        Destroy(gameObject, lifetime);
    }

}
