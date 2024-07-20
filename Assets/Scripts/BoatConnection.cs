
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BoatConnection : MonoBehaviour {

    private LineRenderer Line;
    private Transform start, end;

    private void Start() {
        this.Line = this.GetComponent<LineRenderer>();
    }

    public void SetEndPoints(Transform start, Transform end) {
        this.start = start;
        this.end = end;
    }

    private void Update() {
        if (this.start != null && this.end != null) {
            this.Line.SetPositions(new Vector3[] { this.start.position, this.end.position });
        }
    }

}