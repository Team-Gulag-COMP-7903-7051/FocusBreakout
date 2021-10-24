using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _rotationSpeed;

    private GameObject _target;
    private Vector3 _targetDirection;
    private LineRenderer _lineRenderer;
    private AudioSource _gunshot;
    private float _nextTimeToFire;

    void Start() {
        _lineRenderer = GetComponent<LineRenderer>();
        _gunshot = GetComponent<AudioSource>();
        _nextTimeToFire = 0f;
        _targetDirection = transform.position;
        _lineRenderer.SetPosition(0, transform.position);
    }

    void Update() {
        /*        if (_target == null) {
                    GetTargetBlob();
                } else {
                    _lineRenderer.SetPosition(1, _target.transform.position);
                    ShootBlob();
                }*/
        ShootBlob();
    }

    // Finds a blobs that the shooter can hit
    private void GetTargetBlob() {
        RaycastHit hit;

        _targetDirection = GetTargetDirection(BlobManager.GetRandomBlob());

        if (Physics.Raycast(transform.position, _targetDirection, out hit, 100) &&
            hit.collider.CompareTag("Blob")) {
            _target = hit.collider.gameObject;
        }
    }


    // Follows and shoots target blob
    private void ShootBlob() {
        Vector3 targetDirection;
        RaycastHit hit;

        if (_target == null) {
            _target = BlobManager.GetRandomBlob();
        }
        targetDirection = GetTargetDirection(_target);

        if (Physics.Raycast(transform.position, targetDirection, out hit, 100)) {
            if (hit.collider.CompareTag("Blob")) {
                transform.rotation = Quaternion.FromToRotation(transform.position, targetDirection);
                _lineRenderer.SetPosition(1, _target.transform.position);

                if (Time.time >= _nextTimeToFire) {
                    _nextTimeToFire = Time.time + _fireRate;
                    _bullet.Direction = targetDirection;

                    Instantiate(_bullet, transform.position, Quaternion.identity);
                    _gunshot.Play();
                }
            } else {
                _target = null;
            }
        }
    }

    private Vector3 GetTargetDirection(GameObject obj) {
        return obj.transform.position - transform.position;
    }

    private void OnValidate() {
        if (_fireRate < 0) {
            _fireRate = 0;
        }

        if (_rotationSpeed < 0) {
            _rotationSpeed = 0;
        }
    }
}
