/*
 * if ((puedes entender este código, ya sea por completo, o por partes) &&
 *     ((quieres aprender a crear código para videojuegos) || (ya sabes hacerlo)) &&
 *     (te interesa trabajar como gamedeveloper, ya sea ahora o en el futuro)) {
 *         Contáctame!!
 *         - ruth@ancestralgods.games
 *         - fb.com/vengadoravg
 *         - 77752127
 * } else if ((no puedes entender este código) && 
 *            (te interesa trabajar como gamedeveloper)) {
 *         Estudia hasta que lo entiendas, y luego contáctame :D
 * }
 */

using UnityEngine;
using System.Collections;

public class ControlJugador : MonoBehaviour {
    public Vector2 DistanciaMaximaEnSalto;
    public Transform PuntoDeVista;
    public Detector DetectorDePiso;

    private float _fuerzaDeSalto;
    private float _rapidez;
    private Rigidbody _body;
    private Animator _animator;

    void Start () {
        _body = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        // MAGIA!!
        // con un poquito de física, podemos adivinar cuál es la velocidad inicial
        // que debería tener el personaje al saltar, para que la altura máxima
        // del salto sea el número que está en la variable DistanciaMaximaEnSalto.y
        _fuerzaDeSalto = Mathf.Sqrt(2 * DistanciaMaximaEnSalto.y * Physics.gravity.y * -1);
        float floatingTime = -_fuerzaDeSalto / Physics.gravity.y;
        // también podemos adivinar la rapidez con la que el personaje debería correr
        // para que la distancia máxima horizontal del salto sea el número que está
        // en la variable DistanciaMaximaEnSalto.x
        _rapidez = DistanciaMaximaEnSalto.x / floatingTime;
    }
    
    void FixedUpdate () {
        // Vector que indica en qué dirección se moverá el jugador
        Vector3 direccion =
            (Vector3.Scale(new Vector3(1, 0, 1), PuntoDeVista.transform.forward)
             * Input.GetAxis("Vertical") +
             Vector3.Scale(new Vector3(1, 0, 1), PuntoDeVista.transform.right)
             * Input.GetAxis("Horizontal")).normalized;
        Vector3 velocity = _rapidez * direccion * Time.deltaTime;

        transform.position += velocity;
        _animator.SetFloat("rapidez", velocity.magnitude);
        
        // El personaje debería ver todo el tiempo hacia donde está avanzando.
        // Con esto me aseguro de no cambiar la dirección hacia la que está viendo
        // el personaje, a menos que la velocidad con la que se está moviendo
        // hacia dicha dirección sea mayor que 0.
        if (direccion.magnitude != 0) {
            transform.forward = direccion;
        }
    }

    void Update () {
        if (DetectorDePiso.Activado) {
            _animator.SetBool("esta en el piso", true);
            // asegurándonos que el personaje no conserve inercia física
            // cuando está en el piso.
            _body.velocity = Vector3.Scale(_body.velocity, new Vector3(0,1,0));

            if (Input.GetKeyDown(KeyCode.Space)) {
                _body.velocity += new Vector3(0, _fuerzaDeSalto, 0);
                _animator.SetTrigger("saltar");
            }

        } else {
            _animator.SetBool("esta en el piso", false);
        }
    }

    public void Morir () {
        _animator.SetTrigger("enemigo tocado");
        this.enabled = false;
        BoxCollider[] bs = GetComponents<BoxCollider>();
        foreach (BoxCollider b in bs) {
            b.enabled = false;
            _body.useGravity = false;
            _body.isKinematic = true;
        }
    }
}
