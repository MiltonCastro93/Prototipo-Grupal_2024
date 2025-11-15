using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTutorial : MonoBehaviour {
    private CharacterController _cC;
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    [SerializeField] private float _playerSpeed = 2.0f;
    [SerializeField] private float _jumpHeight = 1.0f;
    private float _gravityValue = -9.81f;

    void Start() {
        _cC = GetComponent<CharacterController>();
    }

    void Update() {
        _groundedPlayer = _cC.isGrounded;

        if(_groundedPlayer && _playerVelocity.y < 0f) {
            _playerVelocity.y = 0f;
        }

        //Horizontal Input
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        move = Vector3.ClampMagnitude(move, 1f); //Elimina las velocidades diagonal

        //Jump
        if (Input.GetButtonDown("Jump") && _groundedPlayer) {
            _playerVelocity.y = Mathf.Sqrt(_jumpHeight * -2.0f * _gravityValue);
        }

        //Apply Gravity
        _playerVelocity.y += _gravityValue * Time.deltaTime;

        //Combinar todos los movimientos
        Vector3 _finalMove = (move * _playerSpeed) + (_playerVelocity.y * Vector3.up);
        _cC.Move(_finalMove * Time.deltaTime);
    }

}
