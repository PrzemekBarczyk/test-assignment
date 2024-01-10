using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private int _poolSize = 10;

    private List<GameObject> _objectPool;

    private void Start()
    {
        InitializeObjectPool();
    }

    private void InitializeObjectPool()
    {
        _objectPool = new List<GameObject>();

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(_objectPrefab);
            obj.SetActive(false);
            _objectPool.Add(obj);
        }
    }

    public GameObject GetObjectFromPool(Vector3 position)
    {
        foreach (GameObject obj in _objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = position;
                obj.SetActive(true);
                return obj;
            }
        }

        // if all objects in the pool are currently in use - instantiate a new object.
        GameObject newObj = Instantiate(_objectPrefab, position, Quaternion.identity);
        _objectPool.Add(newObj);
        return newObj;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}
