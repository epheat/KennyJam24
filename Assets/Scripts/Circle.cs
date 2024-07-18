using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {

    [SerializeField] private Color color;
    private SpriteRenderer image;


    // Start is called before the first frame update
    void Start() {
        this.image = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        this.color.r += 0.0001f;
        if (this.color.r >= 1) {
            this.color.r = 0;
        }
        this.image.color = this.color;
    }
}
