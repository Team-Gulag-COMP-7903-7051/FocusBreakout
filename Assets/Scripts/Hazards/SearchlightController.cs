using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

/// <summary>
/// Controls the movement and behaviour of an enemy Searchlight.
/// 
/// <para>
/// Script should be attached to a GameObject with a NavMeshAgent component.
/// </para>
/// </summary>
public class SearchlightController : MonoBehaviour {
    [SerializeField] private Transform _searchlight;    // The child Spolight object.
    [SerializeField] private float _movementRange;
    [SerializeField] private float _radius;

    [SerializeField] private List<Transform> _path;   // List of positions along the Searchlight's patrol path

    [SerializeField] private int _damage = 1;
    [SerializeField] private float _dmgInterval = 1f;
    [SerializeField] private float _delay = 2f;  // How long the Searchlight lingers at each point

    private NavMeshAgent _agent;
    private int _currentPosition; // Current location represented as the corresponding index in _path (or -1)
    private int _lastPosition;  // Last known location represented as the corresponding index in _path
    private float _period;

    // Start is called before the first frame update
    void Start() {
        _agent = GetComponent<NavMeshAgent>();
        //_path = new List<Transform>();
        _currentPosition = -1; // Location is not a designated point along the patrol path
        _lastPosition = _currentPosition;
        _period = _dmgInterval; // Ensure damage is done instantly the 1st time player is detected
    }

    // Update is called once per frame
    void Update() {
        if (!_agent.hasPath) {
            MoveSearchlight();
        }
    }

    private void FixedUpdate() {
        Search();
    }


    /// <summary>
    /// Send out a SphereCast to detect if the player is standing
    /// in the searchlight.
    /// </summary>
    private void Search() {
        // Cast SphereCast relative to _searchlight's position
        Vector3 origin = _searchlight.position;
        Vector3 dir = _searchlight.forward;
        float radius = 5f;
        float castLen = Math.Abs(_searchlight.position.z);  // Match length of ray to dist. of light from floor
        RaycastHit hit;

        if (Physics.SphereCast(origin, radius, dir, out hit, castLen)) {
            if (hit.collider.CompareTag("Blob")) {
                FollowPlayer(hit.transform.position);
                Attack(hit.transform.GetComponent<Player>());
            } else {
                // Ensure next time player is detected they take immediate damage
                _period = _dmgInterval;
            }
        }
    }

    private void FollowPlayer(Vector3 target) {
        _agent.SetDestination(target);
    }

    /// <summary>
    /// Do damage to the target at the specified interval.
    /// </summary>
    /// <param name="target"></param>
    private void Attack(Player target) {
        if (target == null) {
            Debug.LogError($"The target {this.name} is trying to attack does not have the " +
                $"Player.cs script attached or it is null");
            return;
        }

        if (_period > _dmgInterval) {
            target.TakeDamage(_damage);
            _period = 0f;
        }
        _period += Time.fixedDeltaTime;
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
        //Vector3 newPosition = GetRandomPoint(transform, _radius);
        //// Update NavMeshAgent position on Search Area
        //_agent.SetDestination(newPosition);

        if (_currentPosition == -1 || _currentPosition + 1 == _path.Count) {
            // Searchlight hasn't moved from spawn or has reached end of path
            _currentPosition = 0;
            _agent.SetDestination(_path[_currentPosition].position);
        } else if (_currentPosition + 1 < _path.Count) {
            // Next point is not the end of the path; move to it
            _agent.SetDestination(_path[++_currentPosition].position);
        }
    }

}
