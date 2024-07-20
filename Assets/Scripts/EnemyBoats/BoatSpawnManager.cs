using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Util;
using Random = UnityEngine.Random;

public class BoatSpawnManager : MonoBehaviour{
    [SerializeField] private Vector2 centerPoint;
    [SerializeField] private Vector2 radius;

    [SerializeField] private float spawnRate;
    [SerializeField] private List<EnemyBoat> boats;
    [SerializeField] private List<GameObject> spawnedBoats;

    private void SpawnBoat(){
        Vector3 spawnLocation = new Vector3(Random.Range(centerPoint.x - radius.x, centerPoint.x + radius.x), 0, Random.Range(centerPoint.y - radius.y, centerPoint.y + radius.y));
        EnemyBoat boatSO = GetRandomBoat();
        GameObject boatToSpawn = Instantiate(boatSO.prefab, spawnLocation, TransformUtil.GetLockedYRandomRotation());
        boatToSpawn.GetComponent<EnemyBoatMovementController>().SetMovementSettings(boatSO.speed, boatSO.turnSpeed);
    }
    
    public void DestroyAllBoats(){
        foreach(GameObject boat in spawnedBoats){
            Destroy(boat);
        }
    }

    private EnemyBoat GetRandomBoat(){
        return boats[Random.Range(0, boats.Count)];
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            SpawnBoat();
        }
    }
}
