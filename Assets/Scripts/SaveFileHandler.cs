using System;
using System.IO;
using UnityEngine;

public class SaveFileHandler
{
    private string _fullPath = "";

    public SaveFileHandler(string saveDirPath, string saveFileName)
    {
        _fullPath = Path.Combine(saveDirPath, saveFileName);
    }

    public void Save(GamePersistentData persistentData)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_fullPath));

            string dataToSave = JsonUtility.ToJson(persistentData, true);

            using (FileStream stream = new FileStream(_fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToSave);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public GamePersistentData Load()
    {
        GamePersistentData persistentData = null;

        if (File.Exists(_fullPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(_fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                persistentData = JsonUtility.FromJson<GamePersistentData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        return persistentData;
    }
}
