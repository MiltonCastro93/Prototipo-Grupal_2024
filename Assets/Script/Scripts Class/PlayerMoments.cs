using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMoments : MonoBehaviour {
    private CharacterController _cC;
    private Vector3 _playerVelocity = Vector3.zero;
    [SerializeField] LayerMask _maskFloor;
    [SerializeField] private bool _inGround;
    [SerializeField] float _speedPlayer = 1f, _jumpHeight = 2f;
    private int indexJumps = 0;
    private float _forceGravity = Physics.gravity.y;

    private void Start() {
        _cC = GetComponent<CharacterController>();
    }

    private void Update() {
        _inGround = _cC.isGrounded;//InGround();
        if (_inGround && _playerVelocity.y < 0f) {
            _playerVelocity.y = 0f;
            indexJumps = 0;
        }

        //Horizontal Inputs
        Vector3 axisMoment = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        axisMoment = Vector3.ClampMagnitude(axisMoment, 1f);

        //Jump
        if (Input.GetButtonDown("Jump") && indexJumps < 2) {
            _playerVelocity.y = Mathf.Sqrt(_jumpHeight * -2f * _forceGravity);
            indexJumps++;
        }

        //Apply Gravity
        _playerVelocity.y += _forceGravity * Time.deltaTime;

        //Combinar todos los movimientos
        Vector3 _finalMove = (axisMoment * _speedPlayer) + (_playerVelocity.y * Vector3.up);//no quiero que el eje Y sea afectado por la velocidad por eso esta separado en terminos
        _cC.Move(_finalMove * Time.deltaTime);
    }

    bool InGround() {
        return Physics.CheckSphere(transform.position, 0.25f,_maskFloor);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.25f);
    }

}
