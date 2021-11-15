using UnityEngine;

public class HealthPack : MonoBehaviour {
    [SerializeField] private GameObject _stick;
    [SerializeField] private int _stickAmount;
    [SerializeField] private int _healFactor;

    void Start() {
        for (int i = 0; i < _stickAmount; i++) {
            GameObject stick = Instantiate(_stick, gameObject.transform.position, Quaternion.identity);
            stick.transform.parent = gameObject.transform;
        }
    }

    private void OnTriggerEnter(Collider col) {
        Player player = col.gameObject.GetComponent<Player>();
        if (player != null && player.CurrentHealth != player.MaxHealth) {
            player.Heal(_healFactor);
            Destroy(gameObject);
        }  
    }

    private void OnValidate() {
        if (_stickAmount < 0) {
            _stickAmount = 0;
        }

        if (_healFactor < 0) {
            _healFactor = 0;
        }
    }
}
