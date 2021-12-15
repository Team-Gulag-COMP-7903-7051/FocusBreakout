using System.Collections;
using UnityEngine;

public class DamagePerSeconds : MonoBehaviour {
    public bool DealingDamage { get; set; }

    [SerializeField] private int _damage;        // Amount of damage
    [SerializeField] private float _interval;    // Time between each instance of damage

    private float _period = 0;      // Time since last interval

    public IEnumerator ApplyDamage(Player target) {
        if (target == null) {
            Debug.LogError($"The target {this.name} is trying to attack does not have the " +
                $"Player.cs script attached or it is null");
            yield return null;
        }

        do {
            DealingDamage = true;
            target.TakeDamage(_damage);
            yield return new WaitForSeconds(_interval);
        } while (++_period < _interval);

        DealingDamage = false;
    }
}
