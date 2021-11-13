using System.Collections;
using UnityEngine;

// Made for fun
public class Run : MonoBehaviour { 
    [SerializeField] private GameObject _blobManager;
    [SerializeField] private GameObject _shooter;
    [SerializeField] private AudioSource _audioSource;

    private const float _trackStartTime = 128;
    private const float _time = 4.5f;

    void Start() {
        _blobManager.SetActive(false);
        _shooter.SetActive(false);
        _audioSource.time = _trackStartTime;
        _audioSource.Play();

        StartCoroutine(RunCoroutine());
    }

    IEnumerator RunCoroutine() {
        yield return new WaitForSeconds(_time);
        _blobManager.SetActive(true);
        _shooter.SetActive(true);
    }
}
