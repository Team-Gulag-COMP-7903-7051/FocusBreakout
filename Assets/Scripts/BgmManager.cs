using UnityEngine;

public class BgmManager : MonoBehaviour {
    public static BgmManager BgmInstance;

    private void Awake() {
        if (BgmInstance != null && BgmInstance != this  ) {
            Destroy(this.gameObject);// then we will destory the game object
            return;//return not doing anything
        }

        BgmInstance = this;
        DontDestroyOnLoad(this);//won‘t destory the scene and pass the game object;
    }
}
