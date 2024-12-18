using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject enemyProjectilePrefab; // The projectile the enemy will shoot
    public Transform firePoint;             // The position where the projectile spawns
    public float fireRate = 2f;             // How often the enemy shoots (in seconds)
    public float projectileSpeed = 5f;      // Speed of the projectile
    public AudioClip shootSFX;              // Sound effect for enemy shooting
    public AudioClip destructionSFX;        // Sound effect when the enemy is destroyed
    public int scoreValue = 10;             // Points awarded for destroying the enemy

    private float nextFireTime = 0f;        // Tracks when the enemy can shoot next
    private ScoreManager scoreManager;      // Reference to the score manager (if any)

    private void Start()
    {
        // Randomize the first firing time to avoid all enemies firing simultaneously
        nextFireTime = Time.time + Random.Range(0.5f, fireRate);

        // Try to find the ScoreManager in the scene (optional)
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogWarning("No ScoreManager found in the scene. Score won't update!");
        }
    }

    private void Update()
    {
        // Check if it's time for the enemy to shoot
        if (Time.time >= nextFireTime)
        {
            Shoot(); // Call the shoot function
            nextFireTime = Time.time + fireRate; // Set the next firing time
        }
    }

    private void Shoot()
    {
        if (enemyProjectilePrefab == null || firePoint == null)
        {
            Debug.LogWarning($"{gameObject.name} is missing a Projectile Prefab or Fire Point!");
            return;
        }

        // Instantiate the projectile at the fire point's position and rotation
        GameObject projectile = Instantiate(enemyProjectilePrefab, firePoint.position, firePoint.rotation);

        // Add velocity to the projectile
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = -firePoint.up * projectileSpeed; // Move the projectile downward
        }

        // Play shooting sound effect
        if (shootSFX != null)
        {
            AudioSource.PlayClipAtPoint(shootSFX, transform.position);
        }

        // Optional: Destroy the projectile after 5 seconds to avoid clutter
        Destroy(projectile, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the enemy is hit by a player's projectile (laser)
        if (collision.CompareTag("Laser"))
        {
            Debug.Log($"{gameObject.name} was hit by a laser!");

            // Destroy the enemy and the laser
            Destroy(gameObject);
            Destroy(collision.gameObject);

            // Play destruction sound effect
            if (destructionSFX != null)
            {
                AudioSource.PlayClipAtPoint(destructionSFX, transform.position);
            }

            // Update the score
            if (scoreManager != null)
            {
                scoreManager.AddScore(scoreValue);
            }
        }
    }
}
