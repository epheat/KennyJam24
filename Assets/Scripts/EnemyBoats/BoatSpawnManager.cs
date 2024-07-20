using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoatSpawnManager : MonoBehaviour{
    [SerializeField] private Vector2 centerPoint;
    [SerializeField] private Vector2 radius;

    [SerializeField] private float spawnRate;
    [SerializeField] private List<EnemyBoat> boats;
    [SerializeField] private List<GameObject> spawnedBoats;

    private void SpawnBoat(){
        Vector3 spawnLocation = new Vector3(Random.Range(centerPoint.x - radius.x, centerPoint.x + radius.x), 0, Random.Range(centerPoint.y - radius.y, centerPoint.y + radius.y));
        GameObject boatToSpawn = Instantiate(GetRandomBoat().prefab, spawnLocation, quaternion.identity);
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
