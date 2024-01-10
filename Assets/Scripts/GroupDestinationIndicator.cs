using System.Collections;
using UnityEngine;

public class GroupDestinationIndicator : MonoBehaviour
{
    [SerializeField] float _activeTimeInSeconds = 1f;

    private Vector3 _startScale;

    private void Awake()
    {
        _startScale = transform.localScale;
    }

    private void OnEnable()
    {
        transform.localScale = _startScale;
        StartCoroutine(ShrinkAndDisableCoroutine());
    }

    private IEnumerator ShrinkAndDisableCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _activeTimeInSeconds)
        {
            transform.localScale = Vector3.Lerp(_startScale, Vector3.zero, elapsedTime / _activeTimeInSeconds);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localScale = Vector3.zero;

        gameObject.SetActive(false);
    }
}
