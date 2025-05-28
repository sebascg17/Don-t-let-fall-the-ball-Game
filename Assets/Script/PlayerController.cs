using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject powerupIndicator; // Reference to the power-up indicator GameObject
    public GameObject gameOverPanel; 

    public float speed = 5.0f; // Speed of the player movement
    private float powerupStrength = 15.0f; // Strength of the power-up effect

    public bool hasPowerup = false; // Flag to check if the player has a power-up
    public bool gameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize the player Rigidbody and focal point
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        gameOverPanel = GameObject.Find("GameOverPanel");
    }

    // Update is called once per frame
    void Update()
    {
        //Player movement based on input
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed); 

        Vector3 offset = new Vector3(0, -0.5f, 0); // Offset for the power-up indicator position
        powerupIndicator.transform.position = transform.position + offset; // Set the position of the power-up indicator relative to the player

    }
    private void FixedUpdate ()
    {
        // Check if the player has fallen below a certain height
        ShowGameOverPanel();
    }

    private void ShowGameOverPanel ()
    {
        if(transform.position.y < -5)
        {
            gameOver = true;
            Debug.Log("Game Over! Player fell below the ground.");
            gameOverPanel.GetComponent<UIManager>().ShowGameOver();            
        }
    }


    private void OnTriggerEnter ( Collider other )
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true; // Set the flag to true when the player collects a power-up
            powerupIndicator.SetActive(true); // Activate the power-up indicator
            Destroy(other.gameObject); // Destroy the power-up object
            StartCoroutine(PowerupCountdownRoutine()); // Start the countdown coroutine
        }
    }

    IEnumerator PowerupCountdownRoutine ()
    {
        yield return new WaitForSeconds(7); // Wait for 7 seconds
        hasPowerup = false; // Reset the power-up flag
        powerupIndicator.SetActive(false); // Deactivate the power-up indicator
    }

    private void OnCollisionEnter ( Collision collision )
    {
        // Check if the player collides with an enemy and has a power-up
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position; // Direction away from the player
            Debug.Log("Player colisiona con: " + collision.gameObject.name + " con el powerup en " + hasPowerup);
            // Apply force to the enemy away from the player
            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse); 
        }
    }
}
