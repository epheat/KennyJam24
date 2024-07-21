using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour{
    [SerializeField] private string sceneName;

    public void SwitchScene(){
        Time.timeScale = 1.0f; // resume time passing when restarting the game
        SceneManager.LoadScene(sceneName);
    }
}
