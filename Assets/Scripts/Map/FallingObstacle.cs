using UnityEngine;

public class FallingObstacle : TriggerAnimation {
    [SerializeField] private string _sfx = "FallingObject";   // SFX to play when animation is triggered.

    protected override void Action() {
        base.Action();
        // GameObject should only animate once, so remove the script.
        AudioManager.Instance.Play(_sfx);
        Destroy(this);
    }
}
