using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Represents single character
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class Character : MonoBehaviour
{
    [SerializeField] private GameObject _indicator;

    private NavMeshAgent _navMeshAgent;

    private Transform _leaderToFollow;

    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (_leaderToFollow)
            _navMeshAgent.SetDestination(_leaderToFollow.position);
    }

    public void SetNavMeshParams(float speed, float angularSpeed, float acceleration)
    {
        _navMeshAgent.speed = speed;
        _navMeshAgent.angularSpeed = angularSpeed;
        _navMeshAgent.acceleration = acceleration;
    }

    public void LeadGroup(Vector3 destination)
    {
        _leaderToFollow = null;
        _navMeshAgent.SetDestination(destination);
    }

    public void FollowLeader(Transform leader)
    {
        _leaderToFollow = leader;
    }

    public void Highlight(bool value)
    {
        _indicator.SetActive(value);
    }
}
