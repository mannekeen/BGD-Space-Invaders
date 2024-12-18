using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public AudioClip destructionSFX; // Sound effect for destruction
    public int scoreValue = 10;      // Points awarded when this enemy is destroyed
    private ScoreManager scoreManager;

    private void Start()
    {
        // Find the ScoreManager in the scene
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found in the scene. Please add a ScoreManager script!");
        }
    }

    // Triggered when the enemy collides with something (e.g., player projectile)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is a player projectile
        if (collision.CompareTag("Laser"))
        {
            Debug.Log($"Enemy {gameObject.name} was hit by a laser!");

            // Play destruction sound
            if (destructionSFX != null)
            {
                AudioSource.PlayClipAtPoint(destructionSFX, transform.position);
            }
            else
            {
                Debug.LogWarning("No destructionSFX assigned to " + gameObject.name);
            }

            // Update the score if the ScoreManager is available
            if (scoreManager != null)
            {
                scoreManager.AddScore(scoreValue);
            }

            // Optional: Add an explosion effect or visual feedback here
            // Example: Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

            // Destroy the enemy game object
            Destroy(gameObject);

            // Destroy the projectile game object
            Destroy(collision.gameObject);
        }
    }
}