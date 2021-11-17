using UnityEngine;

public class BgmManager : MonoBehaviour {
    public static BgmManager BgmInstance;

    private void Awake() {
        if (BgmInstance != null && BgmInstance != this  ) {
            // then we will destory the game object
            Destroy(this.gameObject);
            //and return not doing anything
            return;
        }

        BgmInstance = this;

        //won‘t destory the scene and pass the game object;
        DontDestroyOnLoad(this);
    }
}
