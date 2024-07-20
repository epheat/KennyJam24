using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoatMovementController : MonoBehaviour{
    [SerializeField] private EnemyBoatMovementLogic movementLogic;

    [SerializeField] private float minMovementDecisionSeconds;
    [SerializeField] private float maxMovementDecisionSeconds;

    private float speedModifier;
    private float turnSpeed;
    
    private float movementDecisionTimer;
    private Rigidbody rb;

    private Quaternion desiredRotation;

    private void Start(){
        rb = GetComponent<Rigidbody>();
    }

    public void SetMovementSettings(float speed, float turn){
        speedModifier = speed;
        turnSpeed = turn;
    }


    private void Update(){
        if (movementDecisionTimer <= 0){
            desiredRotation = movementLogic.GetNextTurnRotation();
            movementDecisionTimer = UnityEngine.Random.Range(minMovementDecisionSeconds, maxMovementDecisionSeconds);
        }
        else{
            movementDecisionTimer -= Time.deltaTime;
        }
    }
    
    void FixedUpdate() {
        Quaternion newRotation = Quaternion.RotateTowards(rb.rotation, desiredRotation, turnSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(newRotation);
        
        Vector3 forwardMovement = transform.forward * speedModifier * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMovement);
    }
}
