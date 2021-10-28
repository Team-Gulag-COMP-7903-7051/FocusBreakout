using UnityEngine;

public class BulletController : MonoBehaviour {
    [SerializeField] private BulletHit _bulletHit;
    [SerializeField] private float _fireRate;

    private GameObject _target;
    private LineRenderer _lineRenderer;
    private AudioSource _gunshot;
    private float _nextTimeToFire;

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
                    _target.GetComponent<Blob>().TakeDamage(10);

                    BulletHit _particleSystem = Instantiate(_bulletHit, _target.transform.position, Quaternion.FromToRotation(transform.position, _target.transform.position));
                    _particleSystem.transform.parent = _target.transform;
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
    }
}
