using System.Collections;
using UnityEngine;
using Cinemachine;

public class Player : Blob {
    [SerializeField] private float _cameraShake; // Camera shake when hit
    [SerializeField] private HealthBar _healthBar; // UI representation of player's health
    [SerializeField] private CinemachineVirtualCamera _cinemachineCamera; // Cinemachine camera following player

    private Renderer _renderer;
    // _transDur - _transDurRange should not be negative
    // said resulting float may be used in WaitForSeconds()
    private readonly float _transDur = BulletHit.MaxDuration;
    private const float _transDurRange = 0.05f;

    void Start() {
        _renderer = GetComponent<Renderer>();
        _healthBar.SetMaxHealth(Health);
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

    // Make player transparent for x seconds
    // Used when player is hit.
    IEnumerator BulletHitCoroutine() {
        Renderer[] rendererArray = gameObject.GetComponentsInChildren<Renderer>();
        float time = Random.Range(_transDur - _transDurRange, _transDur + _transDurRange);

        foreach (Renderer r in rendererArray) {
            r.enabled = false;
        }
        _renderer.enabled = false;
        CameraShake(_cameraShake);

        yield return new WaitForSeconds(time);

        foreach (Renderer r in rendererArray) {
            r.enabled = true;
        }
        _renderer.enabled = true;
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
