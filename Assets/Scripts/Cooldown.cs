using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour {
    [Header("DashBG")]
    public Image DashCooldown;
    public float CoolDown = 3;
    public KeyCode DashCode;
    private bool isCooldown = false;
    
    
    // Start is called before the first frame update
    void Start() {
        DashCooldown.fillAmount = 0;
    }

    // Update is called once per frame
    void Update() {
        DashCoolDown();
    }

    void DashCoolDown() {
        if (Input.GetKey(DashCode) && isCooldown == false) {
            isCooldown = true;
            DashCooldown.fillAmount = 1;
        }

        if (isCooldown) {
            DashCooldown.fillAmount -= 1 / CoolDown * Time.deltaTime;

            if (DashCooldown.fillAmount <= 0) {
                DashCooldown.fillAmount = 0;
                isCooldown = false;
            }
        }
    }
}
