using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BSOD : MonoBehaviour
{
    int count = 0;

    // Update is called once per frame
    void Update() {
        count++;
        if (count > 1000) {
            SceneManager.LoadScene("BSODEnding"); ;
        }
    }
}
