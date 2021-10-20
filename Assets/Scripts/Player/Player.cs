using UnityEngine;

public class Player : Blob {
    [SerializeField] private HealthBar _healthBar; // UI representation of player's health

    void Start() {
        _healthBar.SetMaxHealth(Health);
    }

    void Update() { }

    public override void TakeDamage(int dmg) {
        base.TakeDamage(dmg);
        _healthBar.SetHealth(Health);
    }

    protected override void Die() {
        GameObject sceneManager = GameObject.Find("SceneManager");
        sceneManager.GetComponent<SceneNavigation>().LoadScene("GameOverScene");
    }
}
