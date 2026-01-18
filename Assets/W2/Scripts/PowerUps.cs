using UnityEngine;

public class PowerUps : MonoBehaviour
{

    [Header("References")]
    public ShooterManager shooterManager;

    public void Collect()
    {
        Debug.Log("Power-up collected!");
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered by " + collision.name);
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Player"))
    //     {
    //         Debug.Log("Power-up collected!");

    //         // Call the function to spawn a shooter
    //         if (shooterManager != null)
    //         {
    //             shooterManager.SpawnShooter();
    //         }
    //         else
    //         {
    //             Debug.LogWarning("ShooterManager not assigned in PowerUp!");
    //         }

    //         // Optional: destroy the power-up after pickup
    //         if (destroyOnPickup)
    //             Destroy(gameObject);
    //     }

    // }


}
