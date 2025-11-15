using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class MoveCharacter : MonoBehaviour {
    CharacterController _character;
    GameObject MeshPerson;
    Animator anim;
    float SpeedCurrent = 0f;
    [SerializeField] private float _speedCrouch = 300f;
    [SerializeField] private float _speedWalk = 500f;

    float EjeH = 0, EjeV = 0, lateH = 0, LateV = 0;
    Quaternion rotPerson = Quaternion.identity;
    public bool walking = true;

    // Start is called before the first frame update
    void Start() {
        SpeedCurrent = _speedWalk;
        _character = GetComponent<CharacterController>();
        MeshPerson = transform.GetChild(0).gameObject;
        anim = MeshPerson.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        EjeH = Input.GetAxis("Horizontal");
        EjeV = Input.GetAxis("Vertical");

        Vector3 MovePlayer = new Vector3(EjeH, 0f,EjeV).normalized;
        MeshPerson.transform.localPosition = Vector3.zero;

        _character.SimpleMove(MovePlayer * SpeedCurrent * Time.deltaTime);

        if (EjeH == 0 && EjeV == 0) {
            rotPerson = Quaternion.LookRotation(new Vector3(lateH, 0, LateV), Vector3.up);
        } else {
            rotPerson = Quaternion.LookRotation(new Vector3(EjeH, 0, EjeV), Vector3.up);
            lateH = EjeH;
            LateV = EjeV;
        }
        MeshPerson.transform.rotation = rotPerson;


        anim.SetFloat("Move", CurrentVelocity());

        if (Input.GetKeyDown(KeyCode.C)) {
            walking = anim.GetBool("CrouchOn");
            anim.SetBool("CrouchOn", !anim.GetBool("CrouchOn"));

            if (anim.GetBool("CrouchOn")) {
                SpeedCurrent = _speedCrouch;
                _character.height = 1.7f / 4;
                _character.center = new Vector3(0f, 0.3f, 0f);
            } else {
                SpeedCurrent = _speedWalk;
                _character.height = 1.7f;
                _character.center = new Vector3(0f, 0.85f, 0f);
            }

        }

    }

    public float CurrentVelocity() {
        return (Mathf.Abs(EjeH)) + Mathf.Abs(EjeV);
    }

}
