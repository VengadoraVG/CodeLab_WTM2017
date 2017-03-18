using UnityEngine;
using System.Collections;

public class Danhino : MonoBehaviour {
    void OnCollisionEnter (Collision elOtro) {
        if (elOtro.gameObject.CompareTag("personaje principal")) {
            elOtro.gameObject.GetComponent<ControlJugador>().Morir();
        }
    }
}
