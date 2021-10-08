using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardManager : MonoBehaviour
{
    [SerializeField]
    private Bullet bullet;

    void Start() {

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            Instantiate(bullet, new Vector3(1, 0, -20), Quaternion.identity);
        }
    }
}
