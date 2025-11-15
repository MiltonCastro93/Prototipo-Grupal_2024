using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABase : MonoBehaviour {

    [SerializeField]
    GameObject MeshPerson, LookCompare, Player;

    enum stateIA { Patrullar, Duda, Alerta, Persecucion};
    [SerializeField] stateIA CurrentState;

    Animator anim;
    [SerializeField]
    Vector3 PositionSound = Vector3.zero;

    [SerializeField] 
    float anglePos = 0f, radiusMax = 3f, distanceSoundMax = 0f, radiusAverage = 2f;//radio del trigger para el Oido, la distancia maxima, y el promedio.
    SphereCollider Oido;
    [SerializeField]
    bool Detectable = false;

    private void Awake() {
        CurrentState = stateIA.Patrullar;

        MeshPerson = transform.GetChild(1).gameObject;
        anim = MeshPerson.GetComponent<Animator>();
        Oido = GetComponent<SphereCollider>();
        Oido.radius = radiusMax;
        distanceSoundMax = radiusMax / radiusAverage;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        if (Player != null && Detectable) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, positionRelative(Player), out hit)) {
                if (hit.collider.CompareTag("Player")) {
                    CurrentState = stateIA.Alerta;
                    Debug.Log("Enemy Detected");
                } else {
                    CurrentState = stateIA.Patrullar;
                    
                }
                Debug.Log("Condicion de Deteccion");
            }

            Debug.DrawRay(transform.position, hit.point - transform.position, Color.green);

        }

    }

    private void OnTriggerStay(Collider other) {
        if (Player != null) {
            float footSound = Vector3.Distance(Player.transform.position, transform.position);

            LookCompare.transform.rotation = Quaternion.LookRotation(positionRelative(Player));
            anglePos = Quaternion.Angle(LookCompare.transform.rotation, Player.transform.rotation);
            if(anglePos <= 90f) {
                Detectable = true;
            } else {
                Detectable = false;
            }


            if (!Player.GetComponentInChildren<Animator>().GetBool("CrouchOn")) {
                if(Player.GetComponent<MoveCharacter>().CurrentVelocity() > 0.1f && footSound < distanceSoundMax) {
                    CurrentState = stateIA.Duda;
                    PositionSound = Player.transform.position;
                }

            }

        }

        if (other.CompareTag("EventSound")) {//EVENTO DE PISAR AGUA o BALDOSAS
            if (other.GetComponent<Liquidos>().RuidoActivo) {
                PositionSound = other.GetComponent<Liquidos>().EventSound();
            }
        }

    }


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {//meterle la condicion de persecucion ON o OFF
            if(CurrentState == stateIA.Patrullar) {
                Player = null;
                Detectable = false;
            }

        }
    }

    private Vector3 positionRelative(GameObject go) { 
        return go.transform.position - transform.position;
    }

}
