using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardManager : MonoBehaviour
{
    [SerializeField]
    private Bullet _bullet;
    [SerializeField]
    private int _bulletAmount;
    private List<Blob> _blobList;
    private Player _target;

    void Start() {
        _blobList = new List<Blob>();
        _target = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update() {
        Vector3 startPos = new Vector3(1, 0, -20);
        _bullet.Direction = _target.transform.position - startPos;
        if (Random.Range(0, _bulletAmount) == 0) {
            Instantiate(_bullet, startPos, Quaternion.identity);
        }
    }

    private void UpdateBlobList() {

    }
}
