using UnityEngine;

public class ObjectsToCollect : MonoBehaviour {
    private void OnTriggerEnter(Collider player) {
        if (player.gameObject.name == "Player") {
            Debug.Log("Object hit by Player");
            gameObject.SetActive(false);
        }
    }
}
