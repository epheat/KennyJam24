using UnityEngine;

public class InputManager : MonoBehaviour {

    [SerializeField] private Camera Camera;

    public static InputManager Instance { get; private set; }
    
    public delegate void OnMouseClick(Vector3 position);
    public event OnMouseClick MouseClickEvent;

    void Awake() {
        if (InputManager.Instance == null) {
            InputManager.Instance = this;
        } else {
            Object.Destroy(this.gameObject);
        }
    }

    void Update() {
        // Left mouse button
        if (Input.GetMouseButtonDown(0)) {
            // raycast from the camera to the first collider
            RaycastHit hit;
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit)) {
                this.MouseClickEvent(hit.point);
            }
        }
    }
}
