using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollower : MonoBehaviour
{

    private Camera cam;
    [SerializeField] GameObject objectToFollow;
    [SerializeField] private float positionSmoothTime = 0.3f;
    [SerializeField] private float rotationSmoothTime = 0.3f;

    private Vector3 positionVelocity = Vector3.zero;

    void Start()
    {
        this.cam = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.objectToFollow != null) {
            Vector3 targetPosition = this.objectToFollow.transform.position;
            this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPosition, ref positionVelocity, positionSmoothTime);

            Quaternion targetRotation = this.objectToFollow.transform.rotation;
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime / this.rotationSmoothTime);
        }
    }
}
