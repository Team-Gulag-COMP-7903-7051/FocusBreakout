public class SingleAnimation : TriggerAnimation {
    /// <summary>
    /// Triggers an animation, then removes this script from the GameObject
    /// to prevent subsequent triggers.
    /// </summary>
    protected override void Action() {
        base.Action();
        Destroy(this);
    }
}
