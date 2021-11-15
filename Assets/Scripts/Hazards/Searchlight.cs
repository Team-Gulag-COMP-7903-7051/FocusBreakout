using UnityEngine;

/// <summary>
/// Script should be attached to a Spot Light GameObject in the scene.
/// </summary>
public class Searchlight : MonoBehaviour {
    [SerializeField] private Transform _thePlayer;

    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _minZ;
    [SerializeField] private float _maxZ;

    [SerializeField] private float _speed;  // Searchlight's movement speed

    private Vector3 _targetPos;
    private bool _isMoving = true;

    private void Start() {
        _targetPos = transform.position;
    }

    void FixedUpdate() {
        Search();
    }

    private Vector2 GetRandomTarget() {
        float x = Random.Range(_minX, _maxX);
        float y = transform.position.y;
        float z = Random.Range(_minZ, _maxZ);
        return new Vector3(x, y, z);
    }

    private void OnTriggerEnter(Collider collision) {
        _isMoving = false;
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

        if (_isMoving) {
            if (transform.position == _targetPos) {
                _targetPos = GetRandomTarget();
            } else {
                float step = _speed * Time.fixedDeltaTime;
                transform.position = Vector3.MoveTowards(transform.position, _targetPos, step);
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
