using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The UI representation of the Player's health.
/// </summary>
public class HealthBar : MonoBehaviour {

    public Slider Slider;
    public Gradient Gradient;
    public Image Fill;

    public void SetMaxHealth(int maxHealth) {
        Slider.maxValue = maxHealth;
        Slider.value = maxHealth;

        Fill.color = Gradient.Evaluate(1f); // Set gradient to colour at 100%
    }

    public void SetHealth(int health) {
        Slider.value = health;

        Fill.color = Gradient.Evaluate(Slider.normalizedValue);
    }
}
