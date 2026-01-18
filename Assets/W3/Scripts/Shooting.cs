using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    [Header("References ")]

    public GameObject bulletPrefab;
    public Transform firePoint;



    [Header("Shooter Settings")]

    public float fireRate = 1f;

    void Start()
    {

        //  StartCoroutine(FireRoutine());

    }


    public IEnumerator FireRoutine()
    {

        Debug.Log("Fire Routine Started");
        while (true)
        {
            FireBullet();
            yield return new WaitForSeconds(fireRate); // 1 second
        }
    }

    void FireBullet()
    {

        Debug.Log("Bullet Fired");

        // Use firePoint's world position and rotation
        GameObject bullet = Instantiate(
        bulletPrefab,
        firePoint.position,       // world position
        firePoint.rotation        // world rotation
    );

        // Optional: parent the bullet to nothing, just to be safe
        bullet.transform.parent = null;
    }

}
