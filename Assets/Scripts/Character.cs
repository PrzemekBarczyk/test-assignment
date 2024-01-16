using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Represents a single character with navigation capabilities
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class Character : MonoBehaviour, IPersistent
{
    [SerializeField] private GameObject _selectionIndicator;

    [SerializeField] float _stoppingDistanceAsLeader = 0;
    [SerializeField] float _stoppingDistanceAsFollower = 3;

    private NavMeshAgent _navMeshAgent;

    private CharacterStateManager _stateManager;

    public string CurrentStateName => _stateManager.CurrentState.Name;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _stateManager = new CharacterStateManager();
        _stateManager.ChangeState(new CharacterIdleState());
    }

    private void OnValidate()
    {
        if (!NameValidator.HasUniqueName<Character>(this))
        {
            Debug.LogError($"Character \"{gameObject.name}\" must have unique name. Please change it");
        }
    }

    private void Update()
    {
        _stateManager.CurrentState.UpdateState(_stateManager);
    }

    public void SetNavMeshParams(float speed, float angularSpeed, float acceleration)
    {
        _navMeshAgent.speed = speed;
        _navMeshAgent.angularSpeed = angularSpeed;
        _navMeshAgent.acceleration = acceleration;
    }

    public void LeadGroup(Vector3 destination)
    {
        _navMeshAgent.stoppingDistance = _stoppingDistanceAsLeader;
        _stateManager.ChangeState(new CharacterLeadState(_navMeshAgent, destination));
    }

    public void FollowLeader(Character leader)
    {
        _navMeshAgent.stoppingDistance = _stoppingDistanceAsFollower;
        _stateManager.ChangeState(new CharacterFollowState(_navMeshAgent, leader));
    }

    public void Highlight(bool value)
    {
        _selectionIndicator.SetActive(value);
    }

    #region IPersistent Implementation
    public void SaveData(GamePersistentData gamePersistentData)
    {
        if (ObjectValidator.IsObjectNull(gamePersistentData, "Game persistent data is null"))
            return;
        if (ObjectValidator.IsObjectNull(gamePersistentData.Characters, "Characters list is null"))
            return;
        
        CharacterPersistentDataWrapper characterPersistentData = 
            new CharacterPersistentDataWrapper(name, transform.position, transform.rotation);

        gamePersistentData.Characters.Add(characterPersistentData);
    }

    public void LoadData(GamePersistentData gamePersistentData)
    {
        if (ObjectValidator.IsObjectNull(gamePersistentData, "Game persistent data is null"))
            return;
        if (ObjectValidator.IsObjectNull(gamePersistentData.Characters, "Characters list is null"))
            return;

        CharacterPersistentDataWrapper data =
            gamePersistentData.Characters.Find(c => c.Name == name);

        if (ObjectValidator.IsObjectNull(data, "Can't find character with this name")) return;

        // make sure character won't move
        _stateManager.ChangeState(new CharacterIdleState());

        // load saved position
        transform.position = data.Position;
        transform.rotation = data.Rotation;
    }
    #endregion
}
