using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitLevel : MonoBehaviour {

    public GameManager GameManager;
    public GameObject CollectKeyText;

    public GameObject Timer;

    private float _timeToAppear = 4f;
    private float _timeWhenDisappear;
    private Animator _animator;

    private void OnTriggerEnter(Collider other) {
        
        if (GameManager._keyCollected) {
            StopAllCoroutines();

            Debug.Log(Timer.GetComponent<Text>().text);

            SceneManager.LoadScene("GameWinScene"); //Switch to GameWinScene
        } else {
            EnableText(); //Enable UI text prompting to collect key
        }
    }

    public void EnableText() {
        CollectKeyText.SetActive(true);
        _timeWhenDisappear = Time.time + _timeToAppear; // Calulcate Time when message should disappear
        _animator = CollectKeyText.GetComponent<Animator>();
        _animator.SetTrigger("Fade");
    }

}
