using UnityEngine;

public class SfxManager : MonoBehaviour {
    public AudioSource AudioSfx;

    public AudioClip AudioClick;

    public static SfxManager sfxInstance;

    private void Awake() {
        if (sfxInstance != null && sfxInstance != this) {
            Destroy(this.gameObject);
            return;
        }

        sfxInstance = this;
        DontDestroyOnLoad(this);
    }
}
