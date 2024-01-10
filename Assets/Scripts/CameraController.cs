using UnityEngine;

/// <summary>
/// Used to follow selected leader
/// </summary>
public class CameraController : MonoBehaviour, IPersistent
{
    [SerializeField] private CharacterManager _characterManager;

    [SerializeField] private float _smoothness = 2f;

    private Vector3 _offsetToLeader;

    private Vector3 _currentVelecity;

    private void Start()
    {
        _offsetToLeader = transform.position;
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

            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, leaderPosition + _offsetToLeader, ref _currentVelecity, _smoothness);

            transform.position = smoothedPosition;
        }
    }

    public void SaveData(GamePersistentData persistentData)
    {
        persistentData.CameraPosition = transform.position;
    }

    public void LoadData(GamePersistentData persistentData)
    {
        if (persistentData != null && persistentData.CameraPosition != null)
        {
            transform.position = persistentData.CameraPosition;
        }
    }
}
