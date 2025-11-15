using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class RolEnemigo : MonoBehaviour {

    GameObject MeshPerson;
    [SerializeField]
    GameObject LookCompare;
    GameObject Player;
    Animator anim;
    [SerializeField]
    float anglePos = 0f;
    [SerializeField]
    Vector3 PositionSound = Vector3.zero;


    [SerializeField]
    float DistanceTrigger = 3f, CurrentSound = 0f , SoundDetectMax = 2f;
    SphereCollider Oido;
    [SerializeField]
    bool Detectable = false;

    private void Awake() {
        MeshPerson = transform.GetChild(1).gameObject;
        anim = MeshPerson.GetComponent<Animator>();
        Oido = GetComponent<SphereCollider>();
        Oido.radius = DistanceTrigger;
        CurrentSound = DistanceTrigger / SoundDetectMax;
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        Oido.radius = DistanceTrigger;
        CurrentSound = DistanceTrigger / SoundDetectMax;
    }

    private void FixedUpdate() {
        if (Player != null && Detectable) {
            Vector3 posRelative = Player.transform.position - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, posRelative + Vector3.up, out hit)) {
                if (hit.collider.CompareTag("Player")) {
                    Debug.Log("Enemy Detected");
                }
                Debug.Log("Condicion de Deteccion");
            }

            Debug.DrawRay(transform.position, hit.point - transform.position, Color.green);

        }

    }


    private void OnTriggerStay(Collider other) {
        if (Player != null) {
            Vector3 posRelative = Player.transform.position - transform.position;
            LookCompare.transform.rotation = Quaternion.LookRotation(posRelative);
            anglePos = Quaternion.Angle(LookCompare.transform.rotation, Player.transform.rotation);

            if(anglePos <= 90f) {
                Detectable = true;
                //print("Cono de Vision");
            }
            else
            {
                Detectable = false;
            }

                float footSound = Vector3.Distance(Player.transform.position, transform.position);

            if (!Player.GetComponentInChildren<Animator>().GetBool("CrouchOn")) { 
                if(Player.GetComponent<MoveCharacter>().CurrentVelocity() > 0.1f && footSound < CurrentSound) {
                    //print("estas corriendo cerca");
                    PositionSound = Player.transform.position;
                    //Usar condiones de angulos mayor de los 90 grados para aplicar la rotacion rapida
                    
                }
            
            }

        }

        if (other.CompareTag("EventSound")) {
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
        if (other.CompareTag("Player")) {
            Player = null;
            Detectable = false;
        }
    }

}
