using UnityEngine;

// Requires a collider to work
// Should be extended by classes if the player can pick it up
public abstract class Item : MonoBehaviour {
    // Ensures the item being picked up is by the Player
    private void OnTriggerEnter(Collider col) {
        Player player = col.gameObject.GetComponent<Player>();

        if (player != null) {
            OnTriggerAction(player);
        }
    }

    // What to do when OnTriggerEnter is triggered
    protected abstract void OnTriggerAction(Player player);
}
