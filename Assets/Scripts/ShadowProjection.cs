using UnityEngine;

public class ShadowProjection : MonoBehaviour {

    static string ShadowPath = "Prefabs/Shadow";
    private GameObject CastingObject;
    private GameObject ShadowPrefab;
    private GameObject Shadow;
    private Transform ShadowParent;

    void Start() {
        this.ShadowPrefab = Resources.Load<GameObject>(ShadowProjection.ShadowPath);
        this.ShadowParent = GameObject.Find("Shadows").transform;
        this.SetShadow(this.gameObject);
    }

    public void SetShadow(GameObject castingObject) {
        if (this.ShadowPrefab != null) {
            this.CastingObject = castingObject;
            this.Shadow = Object.Instantiate(this.ShadowPrefab, this.ShadowParent, true);
        }
    }

    void Update() {
        if (this.Shadow != null && this.CastingObject != null) {
            Vector3 worldPosition = this.CastingObject.transform.TransformPoint(this.CastingObject.transform.localPosition);
            this.Shadow.transform.position = new Vector3(worldPosition.x, 0.01f, worldPosition.z);
        }

        if (this.CastingObject == null) {
            Object.Destroy(this.gameObject);
        }
    }
}