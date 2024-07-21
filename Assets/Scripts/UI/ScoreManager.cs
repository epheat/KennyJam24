using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI endGameText;
    private int currentScore;
    
    public static ScoreManager Instance;

    private void Awake(){
        Instance = this;
    }

    public void AddScore(int score){
        currentScore += score;
        scoreText.text = currentScore.ToString();
        endGameText.text = "Final Score: + " + currentScore.ToString();
    }
}
