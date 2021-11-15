using UnityEngine;

/// <summary>
/// Script should be attached to a Spot Light GameObject in the scene.
/// 
/// <para>NOTE: The Searchlight must be a child to a GameObject with SearchlightController.cs.
/// The position of Searchlight must then be adjusted to (0, 20, 0) in the editor.</para>
/// </summary>
public class Searchlight : MonoBehaviour {

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
        float castLen = 20f;
        RaycastHit hit;

        if (Physics.SphereCast(origin, radius, dir, out hit, castLen)) {

            if (hit.collider.CompareTag("Blob")) {
                print("hello there");
                // Trigger FollowPlayer()
                // Trigger "Bombardment" method to attack player at this pos
            }
        }
    }
}
