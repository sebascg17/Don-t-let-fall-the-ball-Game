using System.Collections;
using UnityEngine;
using UnityEngine.LowLevel;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private GameObject gameOverPanel;
    public GameObject powerupIndicator; // Reference to the power-up indicator GameObject
    public GameObject rocketPrefab;
    private GameObject tmpRocket;
    private Coroutine powerupCountdown;
    public PowerUpType currentPowerUp = PowerUpType.None;

    private float speed = 5.0f; // Speed of the player movement
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
        if (currentPowerUp == PowerUpType.Rockets && Input.GetKeyDown(KeyCode.F)) 
        { 
            LaunchRockets(); 
        }
    }

    public float Speed // This variable is public so it can be set in the Unity Inspector
    {
        get { return speed; }
        set { speed = value; }
    }
    public float GetVelocidad ()
    {
        return speed;
    }

    public void SetVelocidad ( float speed )
    {
        this.speed = speed;
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
            currentPowerUp = other.gameObject.GetComponent<Powerup>().powerUpType;
            powerupIndicator.SetActive(true); // Activate the power-up indicator
            Destroy(other.gameObject); // Destroy the power-up object

            if(powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }
            powerupCountdown = StartCoroutine(PowerupCountdownRoutine()); // Start the countdown coroutine

        }
    }

    IEnumerator PowerupCountdownRoutine ()
    {
        yield return new WaitForSeconds(7); // Wait for 7 seconds
        hasPowerup = false; // Reset the power-up flag
        currentPowerUp = PowerUpType.None;
        powerupIndicator.SetActive(false); // Deactivate the power-up indicator
    }

    private void OnCollisionEnter ( Collision collision )
    {
        // Check if the player collides with an enemy and has a power-up
        if (collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.Pushback)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position; // Direction away from the player
            Debug.Log("Player colisiona con: " + collision.gameObject.name + " con el powerup " + currentPowerUp.ToString());
            // Apply force to the enemy away from the player
            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse); 
        }
    }

    void LaunchRockets () 
    {
        Enemy[] enemies = Object.FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        foreach (var enemy in enemies) 
        { 
            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity); 
            tmpRocket.GetComponent<RocketBehavior>().Fire(enemy.transform); 
        } 
    }
}
