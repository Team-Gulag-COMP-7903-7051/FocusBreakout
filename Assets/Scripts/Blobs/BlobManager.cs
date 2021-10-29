using System.Collections.Generic;
using UnityEngine;

public class BlobManager : MonoBehaviour
{
    [SerializeField] private Blob _basicBlob;
    [SerializeField] private Blob _blinkingBlob;
    [SerializeField] private Blob _movingBlob;
    [SerializeField] private int _maxBlobs;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _minY;
    [SerializeField] private float _maxY;
    [SerializeField] private float _minZ;
    [SerializeField] private float _maxZ;

    private static List<GameObject> _blobList;

    void Start() {
        _blobList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Blob"));
    }

    private void Update() {
        // Spawn blobs if possible
        if (_blobList.Count < _maxBlobs) {
            int num = Random.Range(0, 2);

            switch (num) {
                case 0:
                    _blobList.Add(Instantiate(_blinkingBlob, GetRandomLocation(), Quaternion.identity).gameObject);
                    break;
                case 1:
                    _blobList.Add(Instantiate(_movingBlob, GetRandomLocation(), Quaternion.identity).gameObject);
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

}
