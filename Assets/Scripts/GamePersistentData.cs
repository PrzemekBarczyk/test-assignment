using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GamePersistentData
{
    // character data
    public List<CharacterPersistentDataWrapper> Characters = new List<CharacterPersistentDataWrapper>();
    public string LeaderName = "";
    public int GroupSpeed;
    public int GroupAngularSpeed;
    public int GroupAcceleration;

    // camera data
    public Vector3 CameraPosition = Vector3.zero;
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
