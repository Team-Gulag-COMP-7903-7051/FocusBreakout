using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _fireRate;
    [SerializeField] private float _rotationSpeed;

    private float _nextTimeToFire;
    private GameObject _target;
    private bool _onTarget;
    

    void Start() {
        _nextTimeToFire = 0f;
        _onTarget = false;
    }

    void Update() {
        
        if (_onTarget) {
            TargetBlob();
        } else {
            FindBlob();
        }
        Debug.DrawRay(transform.position, transform.forward * 100);
    }

    private void TargetBlob() {
        if (_target == null) {
            _onTarget = false;
            return;
        }

        Vector3 targetDirection = GetTargetDirection(_target);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, targetDirection, out hit, 100) && hit.collider.CompareTag("Blob")) {
            transform.rotation = Quaternion.FromToRotation(Vector3.forward, targetDirection);

            if (Time.time >= _nextTimeToFire) {
                _nextTimeToFire = Time.time + _fireRate;
                _bullet.Direction = targetDirection;

                Instantiate(_bullet, transform.position, Quaternion.identity);
            }
        } else {
            _onTarget = false;
        }
    }

    private void FindBlob() {
        Vector3 targetDirection = GetTargetDirection(BlobManager.GetRandomBlob());
        RaycastHit hit;

        if (Physics.Raycast(transform.position, targetDirection, out hit, 100) && hit.collider.CompareTag("Blob")) {
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
