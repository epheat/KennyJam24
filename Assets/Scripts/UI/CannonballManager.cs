using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CannonballManager : MonoBehaviour{
    public GameObject uiBall1;
    public GameObject uiBall2;
    public GameObject uiBall3;
    public GameObject uiBall4;
    public GameObject uiBall5;
    public GameObject uiBall6;

    public TextMeshProUGUI ballsText;
    public Slider ballRefillSlider;

    [SerializeField] private int startingBalls;

    private int currentBalls;
    private int maxBalls;
    private int hardCapMaxBalls = 6;

    [SerializeField] private float ballRefillIntervalSeconds;
    private float ballRefillTimer;

    public static CannonballManager Instance;

    private void Awake(){
        Instance = this;
    }

    private void Start(){
        currentBalls = startingBalls;
        maxBalls = startingBalls;
        ballRefillTimer = ballRefillIntervalSeconds;
        AssignBalls();
    }

    private void Update(){
        if (currentBalls < maxBalls){
            ballRefillTimer -= Time.deltaTime;
            if (ballRefillTimer <= 0){
                AddBall();
                ballRefillTimer = ballRefillIntervalSeconds;
            }
            ballRefillSlider.value = 1 - (ballRefillTimer / ballRefillIntervalSeconds);
        }
    }

    private void AssignBalls(){
        ballsText.text = currentBalls.ToString();
        uiBall1.SetActive(currentBalls >= 1);
        uiBall2.SetActive(currentBalls >= 2);
        uiBall3.SetActive(currentBalls >= 3);
        uiBall4.SetActive(currentBalls >= 4);
        uiBall5.SetActive(currentBalls >= 5);
        uiBall6.SetActive(currentBalls >= 6);
    }

    public bool CanUseBall(){
        return currentBalls > 0;
    }

    public void UseBall(){
        currentBalls--;
        AssignBalls();
    }
    
    public void AddBall(){
        if (currentBalls < maxBalls){
            currentBalls++;
        }
        AssignBalls();
    }

    public void IncreaseMaxBalls() {
        if (this.maxBalls < this.hardCapMaxBalls) {
            this.maxBalls++;
        }
    }
}
