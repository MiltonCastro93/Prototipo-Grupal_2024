using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierEnemy : MonoBehaviour {
    private enum _statesEnemy { Patrullar, Duda, PosibleDeteccion, Alerta, TargetLoss}//TARGETLOOS usare una QUEUE
    [SerializeField] _statesEnemy _currentState;
    private CharacterController _cC;
    private Vector3 _oldPosition = Vector3.zero;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _compareLook;
    private float angleLook = 0f;
    [SerializeField] private bool _detectado = false;

    [SerializeField]
    float _distanceTrigger = 3f, _currentSound = 0f, _soundDetectMax = 2f;
    SphereCollider Oido;

    [SerializeField] GameObject _active; //El SIMBOLO DE "!" en el METAL GEAR
    [SerializeField] GameObject _eyes;

    private void Start() {
        _cC = GetComponent<CharacterController>();
        Oido = GetComponent<SphereCollider>();
        Oido.radius = _distanceTrigger;
        _currentSound = _distanceTrigger / _soundDetectMax;
    }

    private void Update() {
        if (_player != null) {
            MoveCharacter player = _player.GetComponent<MoveCharacter>();
            Vector3 relative = _player.transform.position - transform.position;
            _compareLook.transform.rotation = Quaternion.LookRotation(relative);
            angleLook = Quaternion.Angle(_compareLook.transform.rotation, _player.transform.rotation);

            if(angleLook <= 45f) {
                _currentState = _statesEnemy.PosibleDeteccion;
            }else if(angleLook >= 45.1f && angleLook <= 90f) {
                _currentState = _statesEnemy.Duda;
            }
                _oldPosition = _player.transform.position;//Siempre que este dentro se actualizara, y obtendre la ultima posicion reconocida

            float footSound = Vector3.Distance(_oldPosition, transform.position);
            if (player.walking) {
                if (player.CurrentVelocity() > 0.1f && footSound >= _currentSound) {
                    if (!_detectado && _oldPosition != Vector3.zero) {
                        _currentState = _statesEnemy.Duda;
                        Debug.Log("ESCUCHE ALGO");
                    }
                } else {
                    Debug.Log("Si Corres, te ESCUCHARE");
                }
            } else {
                Debug.Log("NO Te puedo escuchar");
            }
        }

        if (_detectado) {
            _currentState = _statesEnemy.Alerta;
            _active.SetActive(true);
        }
        //Debug.Log("ESTADO DEL IA: " + _currentState);
    }

    private void FixedUpdate() {
        Vector3 posRelative = _oldPosition - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, posRelative - new Vector3(0, 0.3f, 0), out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {
                _detectado = true;
            }
            Debug.DrawRay(transform.position + Vector3.up, (hit.point - transform.position)  - new Vector3(0,0.3f,0), Color.green);
        }
        if (_currentState == _statesEnemy.PosibleDeteccion) {


        }

    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("EventSound")) {
            if (other.GetComponent<Liquidos>().RuidoActivo) {
                _oldPosition = other.GetComponent<Liquidos>().EventSound();
            }

        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            _player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            _player = null;
        }
    }

}
