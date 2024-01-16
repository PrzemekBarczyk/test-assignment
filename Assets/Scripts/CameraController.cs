using UnityEngine;

/// <summary>
/// Controls the camera to follow a selected leader character smoothly.
/// Implements the IPersistent interface for saving and loading camera position
/// </summary>
public class CameraController : MonoBehaviour, IPersistent
{
    [SerializeField] private CharacterManager _characterManager;

    [SerializeField][Range(0f, 1f)] private float _smoothness = 0.3f;

    private Vector3 _initialOffset;

    private Vector3 _currentVelecity;

    private void Start()
    {
        _initialOffset = transform.position;

        ObjectValidator.IsObjectNull(_characterManager, "Character manager is null");
    }

    private void LateUpdate()
    {
        FollowLeader();
    }

    private void FollowLeader()
    {
        if (_characterManager.Leader)
        {
            Vector3 leaderPosition = _characterManager.Leader.transform.position;

            Vector3 smoothedPosition =
                Vector3.SmoothDamp(transform.position, leaderPosition + _initialOffset, ref _currentVelecity, _smoothness);

            transform.position = smoothedPosition;
        }
    }

    #region IPersistent Implementation
    public void SaveData(GamePersistentData gamePersistentData)
    {
        if (ObjectValidator.IsObjectNull(gamePersistentData, "Game persistent data is null"))
            return;
        
        gamePersistentData.CameraPosition = transform.position;
    }

    public void LoadData(GamePersistentData gamePersistentData)
    {
        if (ObjectValidator.IsObjectNull(gamePersistentData, "Game persistent data is null"))
            return;

        transform.position = gamePersistentData.CameraPosition;
    }
    #endregion
}
