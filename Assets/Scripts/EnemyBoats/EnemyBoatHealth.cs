using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBoatHealth : MonoBehaviour{
    public Slider healthSlider;
    
    private float currentHeath;

    public float GetCurrentHealth() => currentHeath;
    
    public void SetHealth(float maxHealth){
        currentHeath = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }
    
    private void TakeDamage(int damage){
        healthSlider.value -= damage;
        currentHeath -= damage;

        if (currentHeath <= 0){
            Die();
        }
    }

    private void Die(){
        GetComponent<EnemyBoatMovementController>().enabled = false;
    }

    private void OnCollisionEnter(Collision other){
        if (other.transform.GetComponent<Cannonball>()){
            TakeDamage(1);
        }
    }
}
