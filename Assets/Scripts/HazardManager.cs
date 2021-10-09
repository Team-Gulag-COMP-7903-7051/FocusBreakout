using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardManager : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _minFireRate;
    [SerializeField] private float _maxFireRate;
    private List<GameObject> _blobList;

    void Start() {
        _blobList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Blob"));
       
        StartCoroutine(BulletCoroutine(new Vector3(1, 0, -20)));
    }

    void Update() {}

    private void UpdateBlobList() {}

    // Uses Unity's Random.Range(), values are swapped if _maxFireRate is less than _minFireRate
    private IEnumerator BulletCoroutine(Vector3 startPos) {
        while (true) {
            float time = Random.Range(_minFireRate, _maxFireRate);

            int index = Random.Range(0, _blobList.Count);
            yield return new WaitForSeconds(time);
            _bullet.Direction = _blobList[index].transform.position - startPos;
            Instantiate(_bullet, startPos, Quaternion.identity); 
        }
    }

    private void OnValidate() {
        if (_minFireRate < 0) {
            _minFireRate = 0;
        }

        if (_maxFireRate < 0) {
            _maxFireRate = 0;
        }
    }
}
