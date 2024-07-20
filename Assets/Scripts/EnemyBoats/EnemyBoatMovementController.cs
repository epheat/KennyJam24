using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoatMovementController : MonoBehaviour{
    [SerializeField] private EnemyBoatMovementLogic movementLogic;

    [SerializeField] private float minMovementDecisionSeconds;
    [SerializeField] private float maxMovementDecisionSeconds;
    [SerializeField] private float playerBoatFollowOffset;

    private float speedModifier;
    private float turnSpeed;
    
    private float movementDecisionTimer;
    private Rigidbody rb;

    private Quaternion desiredRotation;
    private Transform playerBoat;
    

    public bool isFollowing;
    private Vector3 randomOffset;

    private void Start(){
        rb = GetComponent<Rigidbody>();
        playerBoat = GameObject.FindWithTag("Player").transform;
        randomOffset = RandomOffset();
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
            if (playerBoat.GetComponent<PlayerShipController>().IsStopped && Mathf.Abs(Vector3.Distance(transform.position, PlayerBoatFollowPoint())) < 3){
                speedModifier = 0;
            }
            else{
                speedModifier = playerBoat.GetComponent<PlayerShipController>().GetMoveSpeed() * .9f;
            }
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
        if (!isFollowing){
            newRotation = Quaternion.RotateTowards(rb.rotation, desiredRotation, turnSpeed * Time.fixedDeltaTime);
        }
        else{
            Vector3 direction = (PlayerBoatFollowPoint() - transform.position).normalized;
            newRotation = Quaternion.LookRotation(direction);
        }
        rb.MoveRotation(newRotation);
        
        if (!isFollowing || Mathf.Abs(Vector3.Distance(transform.position, PlayerBoatFollowPoint())) > 1){
            Vector3 forwardMovement = transform.forward * speedModifier * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMovement);
        }
    }

    private Vector3 PlayerBoatFollowPoint()
    {
        // Calculate the follow point with the random offset
        return playerBoat.position - (playerBoat.forward * playerBoatFollowOffset) + randomOffset;
    }

    private Vector3 RandomOffset(){
        float randX = UnityEngine.Random.Range(-1f, 1f);
        float randZ = UnityEngine.Random.Range(-1f, 1f);
    
        // Create a random offset within a unit sphere on the XZ plane
        return new Vector3(randX, 0, randZ).normalized * playerBoatFollowOffset;
    }
}
