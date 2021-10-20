using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour {

    public GameManager GameManager;
    public GameObject CollectKeyText;

    private float _timeToAppear = 4f;
    private float _timeWhenDisappear;

    private void OnTriggerEnter(Collider other) {
        
        if (GameManager._keyCollected) {
            StopAllCoroutines();
            SceneManager.LoadScene("GameWinScene"); //Switch to GameWinScene
        } else {
            EnableText(); //Enable UI text prompting to collect key
        }
    }

    public void EnableText() {
        CollectKeyText.SetActive(true);
        _timeWhenDisappear = Time.time + _timeToAppear; // Calulcate Time when message should disappear
    }

    private void Update() {
        if (CollectKeyText.activeSelf == true && (Time.time >= _timeWhenDisappear)) { //Message should disappear after time calculated in EnableText()
            CollectKeyText.SetActive(false);
        }
    }

}
