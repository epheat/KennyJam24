using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

    private float floatSpeed = 2.0f;
    private float floatAmount = 0.3f;

    private float time;
    private Vector3 initialPosition;



    void Start() {
        this.initialPosition = this.transform.position;
    }

    void Update() {
        time += Time.deltaTime;
        float rotationY = time * this.floatSpeed * 100;
        this.transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, rotationY, transform.localRotation.eulerAngles.z);
        float newY = initialPosition.y + Mathf.Sin(time * this.floatSpeed) * this.floatAmount;
        this.transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Collect!");

            PlayerShipController ship = other.GetComponent<PlayerShipController>();
            // apply powerup
        }
    }
}
