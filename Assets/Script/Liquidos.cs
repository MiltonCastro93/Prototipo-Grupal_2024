using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquidos : MonoBehaviour {
    public bool RuidoActivo = false;


    private void OnTriggerStay(Collider other) {
        GameObject player;
        if (other.CompareTag("Player")) {
            player = other.gameObject;
            if (!player.GetComponentInChildren<Animator>().GetBool("CrouchOn")) {
                if (player.GetComponent<MoveCharacter>().CurrentVelocity() > 0.1f) {
                    RuidoActivo = true;
                } else {
                    RuidoActivo = false;
                }

            }

        }

    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            RuidoActivo = false;
        }

    }


    public Vector3 EventSound() {
        return transform.position;
    }

}
