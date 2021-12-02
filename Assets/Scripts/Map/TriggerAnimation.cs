using UnityEngine;

/// <summary>
/// Triggers an animation for the _objToAnimate.
/// Attach this script to the GameObject with a Collider.
/// The Animator must have an Animation with a Trigger parameter.
/// </summary>
public class TriggerAnimation : MonoBehaviour {
    [SerializeField] protected GameObject _objToAnimate;  // The GameObject to be animated on trigger
    [SerializeField] protected string _paramName;         // Name of Animation's Trigger Parameter

    protected Animator _animator;

    private void OnTriggerEnter(Collider other) {
        _animator = _objToAnimate.GetComponent<Animator>();
        Action();
    }

    protected virtual void Action() {
        _animator.SetTrigger(_paramName);
    }
}
