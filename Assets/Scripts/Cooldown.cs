using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    [Header("DashBG")]
    public Image dashCooldown;
    private float cooldown = 3;
    bool isCooldown = false;
    public KeyCode dashCode;
    
    // Start is called before the first frame update
    void Start()
    {
        dashCooldown.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        DashCoolDown();
    }

    void DashCoolDown()
    {
        if (Input.GetKey(dashCode) && isCooldown == false) {
            isCooldown = true;
            dashCooldown.fillAmount = 1;
        }

        if (isCooldown) {
            dashCooldown.fillAmount -= 1 / cooldown * Time.deltaTime;

            if (dashCooldown.fillAmount <= 0){
                dashCooldown.fillAmount = 0;
                isCooldown = false;
            }
        }
    }
}
