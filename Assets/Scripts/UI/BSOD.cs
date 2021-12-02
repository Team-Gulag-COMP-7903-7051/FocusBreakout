using UnityEngine;
using UnityEngine.SceneManagement;

public class BSOD : MonoBehaviour {
    private int _count = 0;

    // Update is called once per frame
    void Update() {
        _count++;
        if (_count > 1000) {
            SceneManager.LoadScene("BSODEnding"); ;
        }
    }
}
