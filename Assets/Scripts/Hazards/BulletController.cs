using UnityEngine;

// Simulates a bullet shot by simulating its effects (including damage to blobs)
public class BulletController : MonoBehaviour {
    [SerializeField] private GameObject _muzzleFlash;
    [SerializeField] private BulletHit _bulletHit;
    [SerializeField] private float _fireRate;
    [SerializeField] private int _damage;

    private GameObject _target;
    private LineRenderer _lineRenderer;
    private AudioSource _gunshot;
    private float _nextTimeToFire;

    private const float _particleEffectDuration = 1f;

    void Start() {
        _lineRenderer = GetComponent<LineRenderer>();
        _gunshot = GetComponent<AudioSource>();
        _nextTimeToFire = 0f;
    }

    void Update() {
        _lineRenderer.SetPosition(0, transform.position);
        TargetBlob();
    }

    // Shoots a "bullet" if there is a target
    private void TargetBlob() {
        Vector3 targetDirection;
        RaycastHit hit;

        if (_target == null) {
            targetDirection = GetTargetDirection(BlobManager.GetRandomBlob());
        } else {
            targetDirection = GetTargetDirection(_target);
        }

        if (Physics.Raycast(transform.position, targetDirection, out hit, 420)) {
            if (hit.collider.CompareTag("Blob")) {
                _target = hit.collider.gameObject;
                transform.rotation = Quaternion.FromToRotation(Vector3.forward, targetDirection);

                if (Time.time >= _nextTimeToFire) {
                    _nextTimeToFire = Time.time + _fireRate;
                    _target.GetComponent<Blob>().TakeDamage(_damage);

                    // fix muzzle flash (particle system rotation)
                    Transform barrelTip = transform.Find("Barrel").Find("BarrelTip");
                    GameObject muzzle = Instantiate(_muzzleFlash, barrelTip.position, Quaternion.FromToRotation(barrelTip.position, _target.transform.position));
                    Destroy(muzzle, _particleEffectDuration);

                    // Bullet hitting blobs
                    BulletHit bulletHit = Instantiate(_bulletHit, _target.transform.position, Quaternion.identity);
                    bulletHit.transform.parent = _target.transform;
                    Destroy(bulletHit, _particleEffectDuration);
                    _gunshot.Play();
                }
            } else {
                _target = null;
            }
/*            float rad = 0.025f;
            float x = Random.Range(-rad, rad);
            float y = Random.Range(-rad, rad);
            float z = Random.Range(-rad, rad);
            Vector3 v = new Vector3(hit.collider.transform.position.x + x, hit.collider.transform.position.y + y, hit.collider.transform.position.z + z);
            _lineRenderer.SetPosition(1, v);*/

            _lineRenderer.SetPosition(1, hit.collider.transform.position);
        }
    }

    private Vector3 GetTargetDirection(GameObject obj) {
        return obj.transform.position - transform.position;
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
