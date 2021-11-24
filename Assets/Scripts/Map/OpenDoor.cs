using UnityEngine;

public class OpenDoor : MonoBehaviour {

    [SerializeField] private GameObject _door;

    private Animator _animator;

    private void OnTriggerEnter(Collider other) {
        _animator = _door.GetComponent<Animator>();
        _animator.SetTrigger("Open");
    }

}