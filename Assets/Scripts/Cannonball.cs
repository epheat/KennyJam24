using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cannonball : MonoBehaviour
{
    public Rigidbody Rigidbody;
    [SerializeField] private GameObject Explosion;
    public GameObject sourceGO;
    public int damage = 5;
    private Animator ExplosionAnimator;
    private Camera MainCamera;

    void Awake() {
        this.Rigidbody = this.GetComponent<Rigidbody>();
        this.ExplosionAnimator = this.Explosion.GetComponent<Animator>();
        this.MainCamera = Camera.main;
    }

    void Update() {
        if (this.MainCamera != null) {
            transform.forward = this.MainCamera.transform.forward;
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject == sourceGO){
            return;
        }
        this.ExplosionAnimator.Play("Explosion");
        this.StartCoroutine(this.DestroyAfterDelay(1f));
    }

    IEnumerator DestroyAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);
        Object.Destroy(this.gameObject);
    }
    
}
