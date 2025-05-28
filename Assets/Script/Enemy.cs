using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f; // Speed of the enemy movement
    private Rigidbody enemyRb;
    public GameObject player; // Reference to the player GameObject


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player"); // Assuming the player GameObject is named "Player"
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized; // Calculate the direction towards the player
        enemyRb.AddForce(lookDirection * speed);

        if (transform.position.y < -10) // Check if the enemy falls below a certain height
        {
            Destroy(gameObject); // Destroy the enemy GameObject
        }
    }
}
