using UnityEngine;

public class HealthPack : MonoBehaviour {
    [SerializeField] private int _healFactor;

    private void OnTriggerEnter(Collider col) {
        Player player = col.gameObject.GetComponent<Player>();
        if (player != null && player.CurrentHealth != player.MaxHealth) {
            player.Heal(_healFactor);
            Destroy(gameObject);
        }  
    }

    private void OnValidate() {
        if (_healFactor < 0) {
            _healFactor = 0;
        }
    }
}
