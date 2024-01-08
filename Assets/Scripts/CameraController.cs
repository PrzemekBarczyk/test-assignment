using UnityEngine;

/// <summary>
/// Used to follow selected leader
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField] private CharacterManager _characterManager;

    [SerializeField] private float _smoothness = 2f;

    private Vector3 _offsetToLeader;

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

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, leaderPosition + _offsetToLeader, _smoothness * Time.deltaTime);

            transform.position = smoothedPosition;
        }
    }
}
