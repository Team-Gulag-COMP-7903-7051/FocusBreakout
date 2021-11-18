using UnityEngine;

public class WaterGlitchDamage : MonoBehaviour {

    [SerializeField] private int _damage = 25;
    [SerializeField] private float _dmgInterval = 1f;
    [SerializeField] private Player _player;

    private float _period;

    private void Start() {
        _period = _dmgInterval;
    }

    private void Update() {
        _period += Time.fixedDeltaTime;
    }

    private void OnTriggerStay(Collider player) {
        if (player.gameObject.GetComponent("Player")) {
            if (_period > _dmgInterval) {
                _player.TakeDamage(_damage);
                _period = 0f;
            }
        }
    }
}
