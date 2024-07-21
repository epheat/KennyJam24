using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoatCapture : MonoBehaviour{

    private int scoreValue;

    public void SetScoreValue(int score){
        scoreValue = score;
    }

    public void StartFollow(){
        EnemyBoatMovementController movementController = GetComponent<EnemyBoatMovementController>();
        movementController.enabled = true;
        movementController.StartPlayerFollow();
                
        ScoreManager.Instance.AddScore(scoreValue);
                
        MusicManager.Instance.PlayCaptureSound();
        PlayerShipController.Instance.CreateConnection(this.gameObject);
        Destroy(this);
    }
    
    private void OnCollisionEnter(Collision other){
        if (other.transform.CompareTag("Player")){
            if (GetComponent<EnemyBoatHealth>().GetCurrentHealth() <= 0){
                StartFollow();
            }
        }
    }
}
