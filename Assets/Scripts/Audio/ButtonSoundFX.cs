using UnityEngine;

public class ButtonSoundFX : MonoBehaviour {
    private AudioManager _audioManager;

    private void Awake() {
        _audioManager = AudioManager.Instance;
    }

    public void PlayButtonClick() {
        _audioManager.Play("ButtonClick");
    }

    public void PlayButtonHover() {
        _audioManager.Play("ButtonHover");
    }
}
