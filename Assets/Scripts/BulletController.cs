using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _rotationSpeed;

    private GameObject _target;
    private LineRenderer _lineRenderer;
    private float _nextTimeToFire;
    private bool _onTarget;

    void Start() {
        _lineRenderer = GetComponent<LineRenderer>();
        _nextTimeToFire = 0f;
        _onTarget = false;
    }

    void Update() {
        _lineRenderer.SetPosition(0, transform.position);
        TargetBlob();
/*        if (_onTarget) {
            TargetBlob();
        } else {
            FindBlob();
        }*/
        Debug.DrawRay(transform.position, transform.forward * 100);
    }

    // Shoots a bullet if there is a target
    private void TargetBlob() {
        Vector3 targetDirection;
        RaycastHit hit;

        if (_target == null) {
            targetDirection = GetTargetDirection(BlobManager.GetRandomBlob());
        } else {
            targetDirection = GetTargetDirection(_target);
        }

        if (Physics.Raycast(transform.position, targetDirection, out hit, 100)) {
            if (hit.collider.CompareTag("Blob")) {
                _target = hit.collider.gameObject;
                transform.rotation = Quaternion.FromToRotation(Vector3.forward, targetDirection);

                if (Time.time >= _nextTimeToFire) {
                    _nextTimeToFire = Time.time + _fireRate;
                    _bullet.Direction = targetDirection;

                    Instantiate(_bullet, transform.position, Quaternion.identity);
                }
            } else {
                _target = null;
            }

            _lineRenderer.SetPosition(1, hit.collider.transform.position);
        }
    }

    // Finds a blob that is visible from the shooter's perspective
    private void FindBlob() {
        Vector3 targetDirection = GetTargetDirection(BlobManager.GetRandomBlob());
        RaycastHit hit;

        if (Physics.Raycast(transform.position, targetDirection, out hit, 100) &&
            hit.collider.CompareTag("Blob")) {
            _onTarget = true;
            _target = hit.collider.gameObject;
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
