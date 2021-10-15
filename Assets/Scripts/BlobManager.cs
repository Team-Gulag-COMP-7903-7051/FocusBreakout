using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobManager : MonoBehaviour
{
    [SerializeField] private Blob _basicBlob;
    [SerializeField] private int _maxBlobs;

    private static List<GameObject> _blobList;
    void Start()
    {
        _blobList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Blob"));
    }

    private void Update() {
        if (_blobList.Count < _maxBlobs) {
            _blobList.Add(Instantiate(_basicBlob, GetRandomLocation(), Quaternion.identity).gameObject);
        }
    }

    private Vector3 GetRandomLocation() {
        float x = Random.Range(-22f, 22f);
        float y = Random.Range(5f, 10f);
        float z = Random.Range(-20f, -5f);
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
