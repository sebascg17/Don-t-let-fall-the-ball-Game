using UnityEngine;
using UnityEngine.LowLevel;

public class Velocidad : MonoBehaviour
{

    [SerializeField] PlayerController player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("La velocidad es: " + player.GetVelocidad());
        player.SetVelocidad(5.0f); // Set the initial speed of the player
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
