using UnityEngine;
using UnityEngine.AI;

public class SearchlightController : MonoBehaviour {
    [SerializeField] private Transform _searchlight;
    [SerializeField] private float _movementRange;
    [SerializeField] private float _radius;

    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start() {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        if (!_agent.hasPath) {
            MoveSearchlight();
        }
    }

    private bool RandomPoint(Vector3 centre, float range, out Vector3 result) {
        for (int i = 0; i < 30; i++) {
            Vector3 randomPoint = centre + Random.insideUnitSphere * range;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) {
                result = hit.position;
                return true;
            }
        }

        result = Vector3.zero;

        return false;
    }

    private Vector3 GetRandomPoint(Transform point = null, float radius = 0) {
        Vector3 generatedPoint;
        Vector3 centre = (point == null) ? transform.position : point.position;
        float pointRange = (radius == 0) ? _movementRange : radius;

        if (RandomPoint(centre, pointRange, out generatedPoint)) {
            Debug.DrawRay(generatedPoint, Vector3.up, Color.black, 1);
            return generatedPoint;
        }

        return (point == null) ? Vector3.zero : point.position;
    }

    private void MoveSearchlight() {
        Vector3 newPosition = GetRandomPoint(transform, _radius);
        // Update NavMeshAgent position on Search Area
        _agent.SetDestination(newPosition);
        // Ensure that the NavMeshAgent rotation doesn't affect spotlight position
        if (_searchlight.rotation.eulerAngles.x != 90f) {
            _searchlight.Rotate(90, 0, 0);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
#endif

}
