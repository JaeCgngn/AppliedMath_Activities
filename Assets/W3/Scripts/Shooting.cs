using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    [Header("References ")]

    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Shooter Settings")]
    public float fireRate = 1f;

    [Header("Spread Settings")]
    public int bulletsPerShot = 1;      // Number of bullets fired each time
    public float spreadAngle = 10f;



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

        for (int i = 0; i < bulletsPerShot; i++)
        {
            // Calculate random spread angle
            float randomAngle = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);

            // Apply spread to rotation
            Quaternion spreadRotation = firePoint.rotation * Quaternion.Euler(0, 0, randomAngle);

            Instantiate(
                bulletPrefab,
                firePoint.position,
                spreadRotation
            );
        }
    }

}
