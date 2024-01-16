using System;
using UnityEngine;

/// <summary>
/// Manages a group of characters and facilitates their movement
/// </summary>
public class CharacterManager : MonoBehaviour, IPersistent
{
    public Character[] Characters { get; private set; }
    public Character Leader { get; private set; }

    private int _groupSpeed;
    private int _groupAngularSpeed;
    private int _groupAcceleration;

    private const int MIN_SPEED = 1;
    private const int MAX_SPEED = 5;

    private const int MIN_ANGULAR_SPEED = 360;
    private const int MAX_ANGULAR_SPEED = 720;

    private const int MIN_ACCELERATION = 5;
    private const int MAX_ACCELERATION = 10;

    private void Start()
    {
        FindCharacters();
        RandomizeParams();
        SetParamsInCharacters();
    }

    private void FindCharacters()
    {
        Characters = FindObjectsOfType<Character>();
    }

    private void RandomizeParams()
    {
        _groupSpeed = UnityEngine.Random.Range(MIN_SPEED, MAX_SPEED);
        _groupAngularSpeed = UnityEngine.Random.Range(MIN_ANGULAR_SPEED, MAX_ANGULAR_SPEED);
        _groupAcceleration = UnityEngine.Random.Range(MIN_ACCELERATION, MAX_ACCELERATION);
    }

    private void SetParamsInCharacters()
    {
        foreach (Character character in Characters)
        {
            character.SetNavMeshParams(_groupSpeed, _groupAngularSpeed, _groupAcceleration);
        }
    }

    public void SelectLeader(Character newLeader)
    {
        if (newLeader)
        {
            Character previousLeader = Leader;

            if (previousLeader) previousLeader.Highlight(false);

            Leader = newLeader;

            Leader.Highlight(true);
        }
    }

    public void DeselectLeader()
    {
        if (Leader)
        {
            Leader.Highlight(false);
            Leader = null;
        }
    }

    public void MoveGroup(Vector3 newPosition)
    {
        if (Leader)
        {
            Leader.LeadGroup(newPosition);

            foreach (Character character in Characters)
            {
                if (character != Leader)
                {
                    character.FollowLeader(Leader.transform);
                }
            }
        }
    }

    #region IPersistent Implementation
    public void SaveData(GamePersistentData gamePersistentData)
    {
        if (ObjectValidator.IsObjectNull(gamePersistentData, "Game persistent data is null"))
            return;
        
        // save leader if there is any
        if (Leader) gamePersistentData.LeaderName = Leader.name;
        
        // save group params
        gamePersistentData.GroupSpeed = _groupSpeed;
        gamePersistentData.GroupAngularSpeed = _groupAngularSpeed;
        gamePersistentData.GroupAcceleration = _groupAcceleration;
    }

    public void LoadData(GamePersistentData gamePersistentData)
    {
        if (ObjectValidator.IsObjectNull(gamePersistentData, "Game persistent data is null"))
            return;

        // load group params
        _groupSpeed = gamePersistentData.GroupSpeed;
        _groupAngularSpeed = gamePersistentData.GroupAngularSpeed;
        _groupAcceleration = gamePersistentData.GroupAcceleration;

        SetParamsInCharacters();

        // load leader
        DeselectLeader();

        if (ObjectValidator.IsObjectNull(gamePersistentData.LeaderName, "Leader name is null"))
            return; // by default leader name is empty string -> if null, then sth isn't right
            
        Character loadedLeader =
            Array.Find(Characters, c => c.name == gamePersistentData.LeaderName);

        if (loadedLeader) SelectLeader(loadedLeader);
    }
    #endregion
}
