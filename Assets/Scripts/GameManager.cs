using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Bullet bullet;
    private float minX = -24;
    private float maxX = 22;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) {
            Instantiate(bullet, new Vector3(1, 0, -20), Quaternion.identity);
        }
    }
}
