using System.Collections;
using UnityEngine;

public class GroupDestinationIndicator : MonoBehaviour
{
    [SerializeField] float _activeTimeInSeconds = 1f;

    private void OnEnable()
    {
        StartCoroutine(WaitAndDisableCoroutine());
    }

    private IEnumerator WaitAndDisableCoroutine()
    {
        yield return new WaitForSeconds(_activeTimeInSeconds);
        gameObject.SetActive(false);
    }
}
