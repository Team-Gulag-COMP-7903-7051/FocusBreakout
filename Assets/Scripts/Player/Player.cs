using Cinemachine;
using System.Collections;
using UnityEngine;

public class Player : Blob {
    [SerializeField] private float _cameraShake; // Camera shake when hit
    [SerializeField] private HealthBar _healthBar; // UI representation of player's health
    [SerializeField] private CinemachineVirtualCamera _cinemachineCamera; // Cinemachine camera following player
    [SerializeField] private GameObject _bulletHitPrefab; // particle effect when hit by bullet

    // _hitEffectDur - _hitEffectDurRange should not be negative
    // the resulting float may be used in WaitForSeconds()
    private const float _hitEffectDur = 0.2f;
    private const float _hitEffectDurRange = 0.05f;
    private Renderer _renderer;
    private GameObject _bulletHit;
    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = gameObject.AddComponent<AudioSource>();
    }
    void Start() {
        _renderer = GetComponent<Renderer>();
        _healthBar.SetMaxHealth(Health);

        _bulletHit = Instantiate(_bulletHitPrefab, transform.position, Quaternion.identity);
        _bulletHit.transform.parent = transform;
        _bulletHit.SetActive(false);
    }

    public void BulletMissed() {
        _audioSource.Play();
        print("asdhas");
    }

    public override void TakeDamage(int dmg) {
        base.TakeDamage(dmg);
        StartCoroutine("BulletHitCoroutine");
        _healthBar.SetHealth(Health);
    }

    protected override void Die() {
        GameObject sceneManager = GameObject.Find("SceneManager");
        sceneManager.GetComponent<SceneNavigation>().LoadScene("GameOverScene");
    }

    // VFX when player is hit.
    IEnumerator BulletHitCoroutine() {
        Renderer[] rendererArray = gameObject.GetComponentsInChildren<Renderer>();
        float time = Random.Range(_hitEffectDur - _hitEffectDurRange, _hitEffectDur + _hitEffectDurRange);

        foreach (Renderer r in rendererArray) {
            r.enabled = false;
        }
        _renderer.enabled = false;
        _bulletHit.SetActive(true);
        CameraShake(_cameraShake);

        yield return new WaitForSeconds(time);

        foreach (Renderer r in rendererArray) {
            r.enabled = true;
        }
        _renderer.enabled = true;
        _bulletHit.SetActive(false);
        CameraShake(0);
    }

    private void CameraShake(float intensity) {
        CinemachineBasicMultiChannelPerlin cinemachineBMCP = 
            _cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBMCP.m_AmplitudeGain = intensity;
    }

    protected override void OnValidate() {
        base.OnValidate();
        if (_cameraShake < 0) {
            _cameraShake = 0;
        }
    }
}
