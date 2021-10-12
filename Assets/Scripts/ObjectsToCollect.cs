using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsToCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Blob")
            Debug.Log("Object hit by Blob");
        gameObject.SetActive(false);
    }
}
