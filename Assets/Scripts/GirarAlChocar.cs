using UnityEngine;
using System.Collections;

public class GirarAlChocar : MonoBehaviour {
    public Detector[] SensoresDeChoque;
    public float Angulo;
    public Vector3 Direccion;
    public float Rapidez;

    void Start () {
        
    }
    
    void FixedUpdate () {
        if (EstaBloqueado()) {
            Direccion = Quaternion.Euler(new Vector3(0, Angulo, 0)) * Direccion;
        }

        transform.position += Rapidez * Time.deltaTime * Direccion;
        transform.forward = Direccion;
    }

    public bool EstaBloqueado () {
        foreach (Detector sensor in SensoresDeChoque) {
            if (sensor.EstaActivado()) {
                return true;
            }
        }

        return false;
    }
}
