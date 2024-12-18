using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 5f; // Speed of the projectile

    void Update()
    {
        // Move the projectile downward
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the projectile hits the player
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player hit by projectile!");
            // Add logic here to damage the player if needed
        }

        // Destroy the projectile on any collision
        Destroy(gameObject);
    }

    private void Start()
    {
        // Destroy the projectile after 5 seconds, even if it doesn’t hit anything
        Destroy(gameObject, 5f);
    }
}
