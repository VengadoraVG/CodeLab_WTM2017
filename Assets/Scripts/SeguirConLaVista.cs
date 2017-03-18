using UnityEngine;
using System.Collections;

public class SeguirConLaVista : MonoBehaviour {
    public GameObject Target;

    void Start () {
        
    }
    
    void Update () {
        transform.forward =
            (Target.transform.position - transform.position);
    }
}
