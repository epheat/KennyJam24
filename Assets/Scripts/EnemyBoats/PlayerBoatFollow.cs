using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoatFollow : MonoBehaviour{
    private Transform playerBoat;

    private void Start(){
        playerBoat = GameObject.FindWithTag("Player").transform;
    }
    
    private void Update(){
        transform.position = playerBoat.position;
    }
}
