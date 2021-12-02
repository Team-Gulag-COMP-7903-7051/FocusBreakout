using UnityEngine;

public class TriggerAnimationSequence : TriggerAnimation {
    [SerializeField] private GameObject _nextObjToAnimate;
    [SerializeField] private string _nextAnimationParamName;

    protected override void Action() {
        Animator nextAnimator = _nextObjToAnimate.GetComponent<Animator>();

        // Triggers first animation
        base.Action();

        // Trigger the next animation.
        // NOTE: Animation must be a Trigger parameter. Transition must be on condition.
        nextAnimator.SetTrigger(_nextAnimationParamName);
        Destroy(this);  // Remove script from _objToAnimate to only trigger it once.
    }
}
