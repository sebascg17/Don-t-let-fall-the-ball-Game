using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    [SerializeField]private float velocidad;
    

    public float Velocidad // This variable is public so it can be set in the Unity Inspector
    {
        get { return velocidad; }
        set { velocidad = value; }
    }    
    public float GetVelocidad ()
    {
        return velocidad;
    }

    public void SetVelocidad (float velocidad)
    {
        this.velocidad = velocidad;
    }
}
