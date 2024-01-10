using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Checks for user input and responds to it
/// </summary>
public class InputManager : MonoBehaviour
{
    [SerializeField] string _characterTag = "Character";
    [SerializeField] string _groundTag = "Ground";

    [SerializeField] CharacterManager _characterManager;
    [SerializeField] ObjectPooler _groupDestinationIndicatorsPool;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            HandleRightMouseClick();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            HandleLeftMouseClick();
        }
    }

    private void HandleLeftMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (hit.collider.CompareTag(_groundTag))
            {
                _characterManager.DeselectLeader();
            }
            else if (hit.collider.CompareTag(_characterTag))
            {
                Character selectedCharacter = hit.collider.gameObject.GetComponentInParent<Character>();

                if (selectedCharacter)
                {
                    _characterManager.SelectLeader(selectedCharacter);
                }
            }
        }
    }

    private void HandleRightMouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (_characterManager.Leader && Physics.Raycast(ray, out hit))
        {
            _groupDestinationIndicatorsPool.GetObjectFromPool(hit.point);
            _characterManager.MoveGroup(hit.point);
        }
    }
}
