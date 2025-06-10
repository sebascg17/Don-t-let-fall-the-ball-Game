using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f; // Speed of the enemy movement
    public float force = 1f; // Speed of the enemy movement
    private Rigidbody enemyRb;
    private GameObject player; // Reference to the player GameObject
    private Vector3 lookDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player"); // Assuming the player GameObject is named "Player"
    }

    // Update is called once per frame
    void Update()
    {
        lookDirection = (player.transform.position - transform.position).normalized; // Calculate the direction towards the player
        enemyRb.AddForce(lookDirection * speed);

        if (transform.position.y < -10) // Check if the enemy falls below a certain height
        {
            Destroy(gameObject); // Destroy the enemy GameObject
        }
    }

    private void OnCollisionEnter ( Collision collision )
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody player = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = transform.position - player.gameObject.transform.position; // Direction away from the player
            Debug.Log("Enemy colisiona con: " + player.gameObject.name);
            // Apply force to the enemy away from the player
            enemyRb.AddForce(awayFromPlayer * force, ForceMode.Impulse);
        }
    }
}
