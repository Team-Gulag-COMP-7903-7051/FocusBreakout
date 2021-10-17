using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    [Header("DashBG")]
    public Image DashCooldown;
    private float _cooldown = 3;
    bool isCooldown = false;
    public KeyCode _dashCode;
    
    // Start is called before the first frame update
    void Start()
    {
        DashCooldown.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        DashCoolDown();
    }

    void DashCoolDown()
    {
        if (Input.GetKey(_dashCode) && isCooldown == false) {
            isCooldown = true;
            DashCooldown.fillAmount = 1;
        }

        if (isCooldown) {
            DashCooldown.fillAmount -= 1 / _cooldown * Time.deltaTime;

            if (DashCooldown.fillAmount <= 0){
                DashCooldown.fillAmount = 0;
                isCooldown = false;
            }
        }
    }
}
