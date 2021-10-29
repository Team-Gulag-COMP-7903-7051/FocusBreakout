using System.Collections;
using UnityEngine;

public class Player : Blob {
    [SerializeField] private HealthBar _healthBar; // UI representation of player's health

    private Renderer _renderer;
    // _transDur - _transDurRange should not be negative
    // said resulting float may be used in WaitForSeconds()
    private readonly float _transDur = BulletHit.MaxDuration;
    private const float _transDurRange = 0.05f;

    void Start() {
        _renderer = gameObject.GetComponent<Renderer>();
        _healthBar.SetMaxHealth(Health);
    }

    void Update() { }

    public override void TakeDamage(int dmg) {
        base.TakeDamage(dmg);
        StartCoroutine("TransparentCoroutine");
        _healthBar.SetHealth(Health);
    }

    protected override void Die() {
        GameObject sceneManager = GameObject.Find("SceneManager");
        sceneManager.GetComponent<SceneNavigation>().LoadScene("GameOverScene");
    }

    // Make player transparent for x seconds
    // Used when player is hit.
    IEnumerator TransparentCoroutine() {
        Renderer[] rendererArray = gameObject.GetComponentsInChildren<Renderer>();
        float time = Random.Range(_transDur - _transDurRange, _transDur + _transDurRange);

        foreach (Renderer r in rendererArray) {
            r.enabled = false;
        }
        _renderer.enabled = false;

        yield return new WaitForSeconds(time);

        foreach (Renderer r in rendererArray) {
            r.enabled = true;
        }
        _renderer.enabled = true;
    }
}
