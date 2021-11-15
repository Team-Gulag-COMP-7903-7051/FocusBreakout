using UnityEngine;

/// <summary>
/// Script should be attached to a Spot Light GameObject in the scene.
/// </summary>
public class Searchlight : MonoBehaviour {
    [SerializeField] private Transform _thePlayer;

    void FixedUpdate() {
        Search();
    }

    /// <summary>
    /// Send out a raycast to detect if the player is standing
    /// in the searchlight.
    /// </summary>
    private void Search() {
        Vector3 origin = transform.position;
        Vector3 dir = transform.forward;
        float radius = 5f;
        float castLen = 20;
        RaycastHit hit;

        if (Physics.SphereCast(origin, radius, dir, out hit, castLen)) {

            if (hit.collider.CompareTag("Blob")) {
                print("hello there");
            }
        }
    }

    /// <summary>
    /// The player was detected in the searchlight; follow the player
    /// until they are out of range.
    /// </summary>
    private void FollowPlayer() {

    }
}
