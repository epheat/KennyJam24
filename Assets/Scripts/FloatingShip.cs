using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingShip : MonoBehaviour {

    [SerializeField] private bool PlayFloatingAnim;
    [SerializeField] private float Amplitude = 5f;
    [SerializeField] private float RotationXSpeed = 1.0f;
    [SerializeField] private float RotationZSpeed = 0.8f; 

    private float time;

    void Start() {

    }

    void Update() {
        if (this.PlayFloatingAnim) {
            time += Time.deltaTime;
            float rotationX = Mathf.Sin(time * this.RotationXSpeed) * this.Amplitude;
            float rotationZ = Mathf.Sin(time * this.RotationZSpeed) * this.Amplitude;
            this.transform.localRotation = Quaternion.Euler(rotationX, transform.localRotation.eulerAngles.y, rotationZ);
        }
    }
}
