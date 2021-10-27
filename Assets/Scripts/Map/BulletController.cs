using UnityEngine;

public class BulletController : MonoBehaviour {
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _rotationSpeed;

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

        if (_rotationSpeed < 0) {
            _rotationSpeed = 0;
        }
    }
}
