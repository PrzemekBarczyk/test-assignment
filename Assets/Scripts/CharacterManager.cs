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
    public void SaveData(GamePersistentData persistentData)
    {
        if (persistentData != null && Leader)
        {
            persistentData.LeaderName = Leader.name;

            persistentData.GroupSpeed = _groupSpeed;
            persistentData.GroupAngularSpeed = _groupAngularSpeed;
            persistentData.GroupAcceleration = _groupAcceleration;
        }
    }

    public void LoadData(GamePersistentData persistentData)
    {
        if (persistentData != null && persistentData.LeaderName != null)
        {
            Leader = Array.Find(Characters, c => c.name == persistentData.LeaderName);

            if (Leader) SelectLeader(Leader);

            _groupSpeed = persistentData.GroupSpeed;
            _groupAngularSpeed = persistentData.GroupAngularSpeed;
            _groupAcceleration = persistentData.GroupAcceleration;
        }
    }
    #endregion
}
