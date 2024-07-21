using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEnableObjects : MonoBehaviour{
    [SerializeField] private List<GameObject> toDisable;
    [SerializeField] private List<GameObject> toEnable;

    public void Change(){
        foreach(GameObject obj in toEnable){
            obj.SetActive(true);
        }
        foreach(GameObject obj in toDisable){
            obj.SetActive(false);
        }
    }
}
