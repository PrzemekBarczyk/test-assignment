using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Represents single character
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class Character : MonoBehaviour, IPersistent
{
    [SerializeField] private GameObject _indicator;

    [SerializeField] float _stoppingDistanceAsLeader = 0;
    [SerializeField] float _stoppingDistanceAsFollower = 3;

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
        _navMeshAgent.stoppingDistance = _stoppingDistanceAsLeader;
        _navMeshAgent.SetDestination(destination);
    }

    public void FollowLeader(Transform leader)
    {
        _leaderToFollow = leader;
        _navMeshAgent.stoppingDistance = _stoppingDistanceAsFollower;
    }

    public void Highlight(bool value)
    {
        _indicator.SetActive(value);
    }

    public void SaveData(GamePersistentData gamePersistentData)
    {
        CharacterPersistentDataWrapper characterPersistentData = new CharacterPersistentDataWrapper(name, transform.position, transform.rotation);

        gamePersistentData.Characters.Add(characterPersistentData);
    }

    public void LoadData(GamePersistentData gamePersistenetData)
    {
        if (gamePersistenetData != null && gamePersistenetData.Characters != null)
        {
            CharacterPersistentDataWrapper data = gamePersistenetData.Characters.Find(c => c.Name == name);

            transform.position = data.Position;
            transform.rotation = data.Rotation;

            _navMeshAgent.ResetPath();
        }
    }
}
