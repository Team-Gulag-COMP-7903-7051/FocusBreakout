using UnityEngine;

public class Blob : MonoBehaviour {
    [SerializeField] private int _health;
    [SerializeField] private float _speed;

    public virtual void TakeDamage(int dmg) {
        _health -= dmg;
        if (_health <= 0) {
            Die();
        }
    }

    protected virtual void Die() {
        BlobManager.RemoveBlob(this);
        Destroy(gameObject);
    }

    public int Health {
        get { return _health; }
    }

    public float Speed {
        get { return _speed; }
    }

    private void OnValidate() {
        if (_health < 1) {
            _health = 1;
        }

        if (_speed < 1) {
            _speed = 1;
        }
    }
}
