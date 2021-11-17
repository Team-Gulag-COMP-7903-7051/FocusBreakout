using UnityEngine;

public class Key : Item {
    protected override void OnTriggerAction(Player player) {
        Debug.Log("Object hit by Player");
        gameObject.SetActive(false);
    }
}
