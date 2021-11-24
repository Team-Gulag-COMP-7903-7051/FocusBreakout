using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Displays game over message through Data Manager
public class GameOverText : MonoBehaviour {
    void Start() {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        text.text = "Game Over\n" + DataManager.GameOverMessage;
    }
}
