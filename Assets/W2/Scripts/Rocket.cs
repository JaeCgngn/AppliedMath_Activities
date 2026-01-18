using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 0.5f;

    // private Vector3 direction;

    // public void SetDirection(Vector3 dir)
    // {
    //     direction = dir.normalized;
    //     Destroy(gameObject, lifetime);
    // }


    void Update()
    {

        BulletMovement();

    }

    void BulletMovement()
    {
        Vector3 direction = transform.up;

        transform.position += direction * speed * Time.deltaTime;

        Destroy(gameObject, lifetime);
    }

}
