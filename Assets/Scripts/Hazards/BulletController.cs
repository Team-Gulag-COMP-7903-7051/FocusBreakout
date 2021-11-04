using System;
using UnityEngine;
using Random = UnityEngine.Random;

// Simulates bullet shooting at blobs
public class BulletController : MonoBehaviour {
    [SerializeField] private float _fireRate;
    [SerializeField] private int _damage;
    [SerializeField] [Range(0, 1)] private float _accuracy;

    private GameObject _target;
    private LineRenderer _lineRenderer;
    private AudioSource _gunshot;
    private ParticleSystem _muzzleFlash;
    private float _nextTimeToFire;

    void Start() {
        _lineRenderer = GetComponent<LineRenderer>();
        _gunshot = GetComponent<AudioSource>();
        _muzzleFlash = transform.Find("MuzzleFlash").GetComponent<ParticleSystem>();
        _nextTimeToFire = 0f;
    }

    void Update() {
        _lineRenderer.SetPosition(0, transform.position);
        TargetBlob();
    }

    // Shooter will find and follow a blob (as long as it's "visible")
    private void TargetBlob() {
        Vector3 targetDirection;
        RaycastHit hit;

        // Find blob
        if (_target == null) {
            targetDirection = GetTargetDirection(BlobManager.GetRandomBlob());
        } else {
            targetDirection = GetTargetDirection(_target);
        }

        // Check if it's "visible" by the shooter
        if (Physics.Raycast(transform.position, targetDirection, out hit, Constants.MaxMapDistance)) {
            // Follow blob until it is no longer "visible"
            if (hit.collider.CompareTag("Blob")) {
                _target = hit.collider.gameObject;
                transform.rotation = Quaternion.FromToRotation(Vector3.forward, targetDirection);

                if (Time.time >= _nextTimeToFire) {
                    ShootBlob();
                }
            } else {
                _target = null;
            }
            _lineRenderer.SetPosition(1, hit.collider.transform.position);
        }
    }

    // Imitates a gun shooting at a blob
    private void ShootBlob() {
        _nextTimeToFire = Time.time + _fireRate;
        _muzzleFlash.Play();
        _gunshot.Play();

        // Temporary function to simulate shooter missing
        if (Random.Range(0f, 1f) <= _accuracy) {
            _target.GetComponent<Blob>().TakeDamage(_damage);
        } else if (_target.name == "Player") {
            _target.GetComponent<Player>().BulletMissed();
            print("missed player");
        } else {
            print("missed");
        }
    }

    private Vector3 GetTargetDirection(GameObject obj) {
        return obj.transform.position - transform.position;
    }

    public float Accuracy {
        get { return _accuracy; }
        set {
            if (value < 0 || value > 1) {
                throw new ArgumentOutOfRangeException(
                    "BulletController's Accuracy needs to be between 0 and 1 (both inclusive)");
            } else {
                _accuracy = value;
            }
        }
    }
    
    private void OnValidate() {
        if (_fireRate < 0) {
            _fireRate = 0;
        }

        if (_damage < 0) {
            _damage = 0;
        }
    }
}
