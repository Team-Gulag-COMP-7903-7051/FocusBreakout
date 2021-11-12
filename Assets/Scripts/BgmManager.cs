using UnityEngine;

public class BgmManager : MonoBehaviour
{
    public static BgmManager BgmInstance;

    private void Awake()
    {
        if (BgmInstance != null && BgmInstance != this) {
            Destroy(this.gameObject);
            return;
        }

        BgmInstance = this;
        DontDestroyOnLoad(this);
    }

}
