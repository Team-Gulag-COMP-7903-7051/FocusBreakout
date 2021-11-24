using TMPro;
using UnityEngine;

// Displays game over message through Data Manager
public class GameOverText : MonoBehaviour {
    void Awake() {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        text.text = DataManager.GameOverMessage;
    }
}
