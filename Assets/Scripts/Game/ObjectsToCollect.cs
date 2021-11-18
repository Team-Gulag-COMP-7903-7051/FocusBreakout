using UnityEngine;
using UnityEngine.UI;

public class ObjectsToCollect : MonoBehaviour {

    private GameObject _keyUI;
    public GameManager GameManager;

    private void Start() {
        _keyUI = GameObject.Find("UIKey");
    }

    private void OnTriggerEnter(Collider player) {
        if (player.gameObject.GetComponent("Player")) {
            Debug.Log("Object hit by Player");
            gameObject.SetActive(false);
        }

        if (gameObject.name == "Glitch(Key)") {
            _keyUI.GetComponent<Image>().enabled = true;
            GameManager.KeyCollected = true;
        }
    }
}
