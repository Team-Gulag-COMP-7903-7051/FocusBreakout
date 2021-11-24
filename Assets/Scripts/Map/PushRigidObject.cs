using UnityEngine;

public class PushRigidObject : MonoBehaviour {
    [SerializeField] private float _pushPower = 5.0f;


    private void OnControllerColliderHit (ControllerColliderHit hit) {

        Rigidbody body = hit.collider.attachedRigidbody;

        if(body != null) {
            Vector3 pushDir = hit.gameObject.transform.position - transform.position;
            pushDir.y = 0;
            pushDir.Normalize();

            body.AddForceAtPosition(pushDir * _pushPower, transform.position, ForceMode.Impulse);
        }
    }
}
