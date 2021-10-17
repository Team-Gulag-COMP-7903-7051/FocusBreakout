using UnityEngine;

public class PlatformAttach : MonoBehaviour
{
    public GameObject Player;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject == Player) {
            Debug.Log("Parent set!");
            Player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == Player) {
            Player.transform.parent = null;
        }
    }
}