using Unity.VisualScripting;
using UnityEngine;

public class RocketBehavior : MonoBehaviour
{
    private Transform target;
    private float speed = 15.0f; // Speed of the rocket
    private bool blanco = false; // Flag to check if the rocket is white
    private float force = 15.0f; // Force applied to the rocket
    private float aliveTimer = 5.0f; // Time before the rocket is destroyed

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update () 
    { 
        if (blanco && target != null) 
        { 
            Vector3 moveDirection = (target.transform.position - transform.position).normalized; 
            transform.position += moveDirection * speed * Time.deltaTime; 
            transform.LookAt(target); 
        } 
    }

    public void Fire ( Transform newTarget ) 
    {
        target = newTarget;
        blanco = true; 
        Destroy(gameObject, aliveTimer); 
    }

    void OnCollisionEnter ( Collision colision ) 
    { 
        if (target != null) 
        { 
            if (colision.gameObject.CompareTag(target.tag)) 
            { 
                Rigidbody targetRigidbody = colision.gameObject.GetComponent<Rigidbody>(); 
                Vector3 away = -colision.contacts[0].normal; 
                targetRigidbody.AddForce(away * force, ForceMode.Impulse); 
                Destroy(gameObject); 
            } 
        } 
    }
}
