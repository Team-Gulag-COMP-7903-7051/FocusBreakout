using UnityEngine;

public class HealthPack : Item {
    [SerializeField] private int _healFactor;

    protected override void OnTriggerAction(Player player) {
        if (player.CurrentHealth != player.MaxHealth) {
            player.Heal(_healFactor);
            AudioManager.Instance.Play("Heal");
            Destroy(gameObject);
        }
    }

    private void OnValidate() {
        if (_healFactor < 0) {
            _healFactor = 0;
        }
    }
}
