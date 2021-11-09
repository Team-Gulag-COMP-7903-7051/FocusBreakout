using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobManager : MonoBehaviour {
    [SerializeField] private BlinkingBlob _blinkingBlob;
    [SerializeField] private MovingBlob _movingBlob;
    [SerializeField] private int _maxBlobs;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _minY;
    [SerializeField] private float _maxY;
    [SerializeField] private float _minZ;
    [SerializeField] private float _maxZ;
    [SerializeField] private Vector3 _startingMoveDirection;

    private static List<GameObject> _blobList;
    private const int _numDifferentBlobs = 2; // # of differnt blob types that can be spawned

    void Start() {
        _blobList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Blob"));
    }

    private void Update() {
        // Spawn blobs if possible
        if (_blobList.Count < _maxBlobs) {
            int num = Random.Range(0, _numDifferentBlobs);

            switch (num) {
                case 0:
                    _blobList.Add(Instantiate(_blinkingBlob, GetRandomLocation(), Quaternion.identity).gameObject);
                    break;
                case 1:
                    MovingBlob blob = Instantiate(_movingBlob, GetRandomLocation(), Quaternion.identity);
                    if (Random.Range(0, 2) == 0) {
                        _startingMoveDirection *= -1;
                    }
                    blob.WorldDirection = _startingMoveDirection;
                    _blobList.Add(blob.gameObject);
                    break;
                default:
                    Debug.LogError("BlobManager spawn error. Random int(" + num + ") outside of switch statement.");
                    break;
            }
        }
    }

    private Vector3 GetRandomLocation() {
        float x = Random.Range(_minX, _maxX);
        float y = Random.Range(_minY, _maxY);
        float z = Random.Range(_minZ, _maxZ);
        return new Vector3(x, y, z);
    }

    public static int GetBlobCount() {
        return _blobList.Count;
    }

    public static GameObject GetRandomBlob() {
        int index = Random.Range(0, _blobList.Count);
        return _blobList[index];
    }

    public static void AddBlob(GameObject blob) {
        _blobList.Add(blob);
    }

    public static void RemoveBlob(Blob blob) {
        _blobList.Remove(blob.gameObject);
    }

    // Beta
    IEnumerator Run() {
        yield return new WaitForSeconds(5);
        print("run");
        _maxBlobs = 5000;
    }

    private void OnValidate() {
        // Restrict StartingMoveDirection Vector3 values between -1 and 1.
        if (_startingMoveDirection.x < -1) {
            _startingMoveDirection.x = -1;
        } else if (_startingMoveDirection.x > 1) {
            _startingMoveDirection.x = 1;
        } else if (_startingMoveDirection.y < -1) {
            _startingMoveDirection.y = -1;
        } else if (_startingMoveDirection.y > 1) {
            _startingMoveDirection.y = 1;
        } else if (_startingMoveDirection.z < -1) {
            _startingMoveDirection.z = -1;
        } else if (_startingMoveDirection.z > 1) {
            _startingMoveDirection.z = 1;
        }
    }
}
