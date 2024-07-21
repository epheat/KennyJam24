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
            MusicManager.Instance.PlayDeathSound();
            Die();
        } else {
            MusicManager.Instance.PlayImpactSound();
        }
    }

    private void Die(){
        GetComponent<EnemyBoatMovementController>().enabled = false;
        GetComponent<Collider>().isTrigger = true;
        healthSlider.gameObject.SetActive(false);

        EnemyAttackLogic attackLogic = GetComponent<EnemyAttackLogic>();
        if (attackLogic != null){
            attackLogic.enabled = false;
        }
        
        GetComponent<EnemyBoatCapture>().StartFollow();
    }

    private void OnCollisionEnter(Collision other){
        if (currentHeath <= 0){
            return;
        }
        Cannonball cannonball = other.transform.GetComponent<Cannonball>();
        if (cannonball != null){
            if (cannonball.sourceGO != this.gameObject){
                TakeDamage(cannonball.damage);
            }
        }
        
    }
}
