using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoatCapture : MonoBehaviour{

    private int scoreValue;

    public void SetScoreValue(int score){
        scoreValue = score;
    }
    
    private void OnCollisionEnter(Collision other){
        if (other.transform.CompareTag("Player")){
            if (GetComponent<EnemyBoatHealth>().GetCurrentHealth() <= 0){
                EnemyBoatMovementController movementController = GetComponent<EnemyBoatMovementController>();
                movementController.enabled = true;
                movementController.StartPlayerFollow();
                
                ScoreManager.Instance.AddScore(scoreValue);
                
                PlayerShipController ship = other.transform.GetComponent<PlayerShipController>();
                MusicManager.Instance.PlayCaptureSound();
                ship.CreateConnection(this.gameObject);
                
                Destroy(this);
            }
        }
    }
}
