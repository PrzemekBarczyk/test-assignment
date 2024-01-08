using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GamePersistentData
{
    public List<CharacterPersistentDataWrapper> Characters;
    public string LeaderName;

    public Vector3 CameraPosition;

    public GamePersistentData()
    {
        Characters = new List<CharacterPersistentDataWrapper>();
        LeaderName = "";

        CameraPosition = Vector3.zero;
    }
}

[System.Serializable]
public class CharacterPersistentDataWrapper
{
    public string Name;
    public Vector3 Position;
    public Quaternion Rotation;

    public CharacterPersistentDataWrapper(string name, Vector3 position, Quaternion rotation)
    {
        Name = name;
        Position = position;
        Rotation = rotation;
    }
}
