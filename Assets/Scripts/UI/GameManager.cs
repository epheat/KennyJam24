using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public float TimeRemaining = 50;
    [SerializeField] private TextMeshProUGUI TimeRemainingText;

    private void Start() {

    }

    private void Update() {
        this.TimeRemaining -= Time.deltaTime;
        this.TimeRemainingText.text = this.TimeRemaining.ToString("N0");

        if (this.TimeRemaining <= 0) {
            Debug.Log("Time's up!");
        }
    }
}