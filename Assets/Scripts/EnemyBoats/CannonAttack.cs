
using System;
using System.Collections.Generic;
using UnityEngine;

public class CannonAttack : EnemyAttackLogic{

    [SerializeField] private GameObject cannonballPrefab;
    [SerializeField] private List<Transform> cannons;

    [SerializeField] private float cannonVelo;

    private float attackTimer;
    [SerializeField] private float attackCooldownSeconds = 2f;
    
    private void Update(){
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldownSeconds){
            Attack();
            attackTimer = 0;
        }
    }

    public override void Attack(){
        ShootCannonball();
    }

    private void ShootCannonball(){
        Transform cannon = cannons[UnityEngine.Random.Range(0, cannons.Count)];
        GameObject cGO = Instantiate(cannonballPrefab, cannon.position, Quaternion.identity);
        Cannonball cannonball = cGO.GetComponent<Cannonball>();
        cannonball.sourceGO = this.gameObject;
        cannonball.Rigidbody.velocity = cannon.forward * cannonVelo;
    }
}