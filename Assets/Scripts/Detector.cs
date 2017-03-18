using UnityEngine;
using System.Collections;

public class Detector : MonoBehaviour {
    public bool Activado;
    public bool ActuaAlReves = false;

    void OnTriggerStay () {
        Activado = true;
    }

    void OnTriggerExit () {
        Activado = false;
    }

    public bool EstaActivado () {
        return Activado == !ActuaAlReves;
    }
}
