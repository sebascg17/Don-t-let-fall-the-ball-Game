using UnityEngine;

public class SpinObject : MonoBehaviour
{
    private float rotationSpeed = 100f;
    private float bounceAmplitude = 0.25f; // qu� tanto sube y baja
    private float bounceFrequency = 2f;    // velocidad del salto

    private Vector3 startPosition;

    private void Start ()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotaci�n del objeto
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Movimiento vertical de tipo "bouncing"
        float newY = startPosition.y + Mathf.Sin(Time.time * bounceFrequency) * bounceAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
