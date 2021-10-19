using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour {

    [Header("Dash")]
    public Image CooldownIcon1;
    public float CoolDown = 3;
    public KeyCode DashCode;
    public bool isCooldown = false;
    
    
    // Start is called before the first frame update
    void Start() {
        CooldownIcon1.fillAmount = 0;
    }

    // Update is called once per frame
    void Update() {
        DashCoolDown();
    }

    void DashCoolDown() {
        if (Input.GetKey(DashCode) && isCooldown == false) {
            isCooldown = true;
            CooldownIcon1.fillAmount = 1;
        }

        if (isCooldown) {
            CooldownIcon1.fillAmount -= 1 / CoolDown * Time.deltaTime;

            if (CooldownIcon1.fillAmount <= 0) {
                CooldownIcon1.fillAmount = 0;
                isCooldown = false;
            }
        }
    }
}
