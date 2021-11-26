using UnityEngine;

/// <summary>
/// Triggers an animation for the _objToAnimate.
/// Attach this script to the GameObject with an Animator component.
/// The Animator must have an Animation with a Trigger parameter.
/// </summary>
public class TriggerAnimation : MonoBehaviour {
    [SerializeField] private GameObject _objToAnimate;  // The GameObject to be animated on trigger
    [SerializeField] private string _paramName;         // Name of Animation's Trigger Parameter

    private Animator _animator;

    private void OnTriggerEnter(Collider other) {
        _animator = _objToAnimate.GetComponent<Animator>();
        _animator.SetTrigger(_paramName);
    }
}
