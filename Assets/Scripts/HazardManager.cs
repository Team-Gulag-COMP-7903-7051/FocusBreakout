using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardManager : MonoBehaviour
{
    [SerializeField]
    private Bullet bullet;
    private List<Blob> blobList;
    private Player target;

    void Start() {
        blobList = new List<Blob>();
        target = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update() {
        Vector3 startPos = new Vector3(1, 0, -20);
        bullet.Direction = target.transform.position - startPos;
        if (Random.Range(0, 500) == 0) {
            Instantiate(bullet, startPos, Quaternion.identity);
        }
    }

    private void UpdateBlobList() {

    }
}
