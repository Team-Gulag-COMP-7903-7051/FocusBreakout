using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

// Simulates bullet shooting at blobs
public class BulletController : MonoBehaviour {
    [SerializeField] private float _fireRate;
    [SerializeField] private int _damage;
    [SerializeField] private float _spread; // based on blob radius
    [SerializeField] private GameObject _bulletTerrainHit;
    [SerializeField] private Audio[] _audioArray;

    // _hitEffectDur - _hitEffectDurRange should not be negative
    // the resulting float may be used in WaitForSeconds()
    private const float _hitEffectDur = 0.1f;
    private const float _hitEffectDurRange = 0.05f;

    private GameObject _target;
    private LineRenderer _lineRenderer;
    private ParticleSystem _muzzleFlash;
    private float _nextTimeToFire;

    void Start() {
        _lineRenderer = GetComponent<LineRenderer>();
        foreach (Audio audio in _audioArray) {
            audio.AudioSource = gameObject.AddComponent<AudioSource>();
        }
        
        _muzzleFlash = transform.Find("MuzzleFlash").GetComponent<ParticleSystem>();
        _nextTimeToFire = 0f;

        _bulletTerrainHit = Instantiate(_bulletTerrainHit, transform.position, Quaternion.identity);
        _bulletTerrainHit.SetActive(false);
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
                targetDirection = AimSpread(targetDirection); // Apply shooter spread/accuracy 
                transform.rotation = Quaternion.FromToRotation(Vector3.forward, targetDirection);
                _lineRenderer.SetPosition(1, hit.collider.transform.position);

                if (Time.time >= _nextTimeToFire) {
                    ShootBlob(targetDirection);
                }
            } else {
                _target = null;
            }
        }
    }

    // Imitates a gun shooting at a blob
    private void ShootBlob(Vector3 dir) {
        RaycastHit hit;

        _nextTimeToFire = Time.time + _fireRate;
        _muzzleFlash.Play();
        _audioArray[0].Play();

        if (Physics.Raycast(transform.position, dir, out hit, Constants.MaxMapDistance)) {
            if (hit.collider.CompareTag("Blob")) {
                _target.GetComponent<Blob>().TakeDamage(_damage);
            } else {
                _bulletTerrainHit.transform.position = hit.point;
                AudioSource.PlayClipAtPoint(_audioArray[1].Clip, hit.point, _audioArray[1].Volume);
                StartCoroutine("TerrainHitCoroutine");
            }
        }
    }

    private Vector3 GetTargetDirection(GameObject obj) {
        return obj.transform.position - transform.position;
    }

    // picks a random point inside a sphere with _spread as the radius
    private Vector3 AimSpread(Vector3 pos) {
        return Random.insideUnitSphere * _spread + pos;
    }

    IEnumerator TerrainHitCoroutine() {
        float time = Random.Range(_hitEffectDur - _hitEffectDurRange, _hitEffectDur + _hitEffectDurRange);
        _bulletTerrainHit.SetActive(true);

        yield return new WaitForSeconds(time);
        _bulletTerrainHit.SetActive(false);
    }

    private void OnValidate() {
        if (_fireRate < 0) {
            _fireRate = 0;
        }

        if (_damage < 0) {
            _damage = 0;
        }

        if (_spread < 0) {
            _spread = 0;
        }
    }
}
