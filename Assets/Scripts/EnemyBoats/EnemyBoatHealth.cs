using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBoatHealth : MonoBehaviour{
    public Slider healthSlider;
    
    private float currentHeath;
    
    public void SetHealth(float maxHealth){
        currentHeath = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }
    
    private void TakeDamage(int damage){
        healthSlider.value -= damage;
        currentHeath -= damage;
    }

    private void Die(){
        
    }

    private void OnCollisionEnter(Collision other){
        if (other.transform.GetComponent<Cannonball>()){
            TakeDamage(1);
        }
    }
}
