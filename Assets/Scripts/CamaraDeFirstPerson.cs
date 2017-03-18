using UnityEngine;
using System.Collections;

public class CamaraDeFirstPerson : MonoBehaviour {
    public GameObject Target;
    public float Torque;
    public GameObject LaCamara;

    void Start () {

    }

    void FixedUpdate () {
        float lados = Input.GetAxis("Mouse X") * Torque * Time.deltaTime,
            arribaAbajo = -Input.GetAxis("Mouse Y") * Torque * Time.deltaTime;

        transform.Rotate(0, lados, 0);
        LaCamara.transform.Rotate(arribaAbajo, 0, 0);

        transform.position = Target.transform.position;
    }
}
