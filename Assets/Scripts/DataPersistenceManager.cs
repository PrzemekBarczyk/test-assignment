using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    [SerializeField] private string _saveFileName = "save.save";

    private SaveFileHandler _saveFileHandler;

    private List<IPersistent> _persistentObjects;

    private void Start()
    {
        _saveFileHandler = new SaveFileHandler(Application.persistentDataPath, _saveFileName);

        FindPersistentObjects();
    }

    private void FindPersistentObjects()
    {
        _persistentObjects = new List<IPersistent>(FindObjectsOfType<MonoBehaviour>().OfType<IPersistent>());
    }

    public void SaveGame()
    {
        GamePersistentData persistentData = new GamePersistentData();

        foreach (IPersistent dataPersistenceObj in _persistentObjects)
        {
            dataPersistenceObj.SaveData(persistentData);
        }

        _saveFileHandler.Save(persistentData);
    }

    public void LoadGame()
    {
        GamePersistentData persistentData = _saveFileHandler.Load();

        foreach (IPersistent dataPersistenceObj in _persistentObjects)
        {
            dataPersistenceObj.LoadData(persistentData);
        }
    }
}
