using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour{
    [SerializeField] private string sceneName;

    public void SwitchScene(){
        SceneManager.LoadScene(sceneName);
    }
}
