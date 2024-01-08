using UnityEngine;

/// <summary>
/// Checks for user input and responds to it
/// </summary>
public class InputManager : MonoBehaviour
{
    [SerializeField] string _characterTag = "Character";
    [SerializeField] string _groundTag = "Ground";

    [SerializeField] CharacterManager _characterManager;

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

        if (Physics.Raycast(ray, out hit))
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

        if (Physics.Raycast(ray, out hit))
        {
            _characterManager.MoveGroup(hit.point);
        }
    }
}
