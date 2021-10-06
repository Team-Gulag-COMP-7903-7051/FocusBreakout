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
        if(Random.Range(0, 20) == 0) {
            float num = Random.Range(minX, maxX);
            Instantiate(bullet, new Vector3(num, 0, -20), Quaternion.identity);
        }
    }
}
