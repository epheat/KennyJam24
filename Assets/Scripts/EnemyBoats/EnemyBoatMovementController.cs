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
    private Transform playerBoat;

    public bool isFollowing;

    private void Start(){
        rb = GetComponent<Rigidbody>();
        playerBoat = GameObject.FindWithTag("Player").transform;
    }

    public void SetMovementSettings(float speed, float turn){
        speedModifier = speed;
        turnSpeed = turn;
    }

    public void StartPlayerFollow(){
        isFollowing = true;
    }


    private void Update(){
        if (isFollowing){
            return;
        }
        
        if (movementDecisionTimer <= 0){
            desiredRotation = movementLogic.GetNextTurnRotation();
            movementDecisionTimer = UnityEngine.Random.Range(minMovementDecisionSeconds, maxMovementDecisionSeconds);
        }
        else{
            movementDecisionTimer -= Time.deltaTime;
        }
    }
    
    void FixedUpdate() {
        
        Quaternion newRotation;
        Vector3 forwardMovement;
        if (!isFollowing){
            newRotation = Quaternion.RotateTowards(rb.rotation, desiredRotation, turnSpeed * Time.fixedDeltaTime);
            forwardMovement = transform.forward * speedModifier * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMovement);
        }
        else{
            Vector3 direction = (playerBoat.transform.position - transform.position).normalized;
            newRotation = Quaternion.LookRotation(direction);

            rb.velocity = playerBoat.GetComponent<Rigidbody>().velocity;
            Debug.Log(playerBoat.GetComponent<Rigidbody>().velocity);
        }
        
        rb.MoveRotation(newRotation);
        
    }
}
