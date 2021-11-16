using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour {

    public GameManager GameManager;
    public GameObject CollectKeyText;

    private float _timeToAppear = 4f;
    private float _timeWhenDisappear;
    private Animator _animator;

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
        _animator = CollectKeyText.GetComponent<Animator>();
        _animator.SetTrigger("Fade");
    }

}
