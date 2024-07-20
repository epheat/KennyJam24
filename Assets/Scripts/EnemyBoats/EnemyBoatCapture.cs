using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoatCapture : MonoBehaviour
{
    private void OnCollisionEnter(Collision other){
        if (other.transform.CompareTag("Player")){
            if (GetComponent<EnemyBoatHealth>().GetCurrentHealth() <= 0){
                EnemyBoatMovementController movementController = GetComponent<EnemyBoatMovementController>();
                movementController.enabled = true;
                movementController.StartPlayerFollow();
            }
        }
    }
}
