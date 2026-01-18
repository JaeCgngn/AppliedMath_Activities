using UnityEngine;
using System.Collections;


public class W3Shooting : MonoBehaviour
{
   [Header("References ")]

    public GameObject bulletPrefab;
    public Transform firePoint;


    [Header("Shooter Settings")]

    public float fireRate = 1f;

    void Start()
    {
        StartCoroutine(FireRoutine());
    }

    IEnumerator FireRoutine()
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
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

}
