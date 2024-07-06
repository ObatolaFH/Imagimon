using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    private const string filePath = "/imagimonData.json";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure this instance persists across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    public void SaveImagimonData(List<ImagimonData> imagimonList)
    {
        if (imagimonList.Count > 5)
        {
            imagimonList = imagimonList.GetRange(0, 5); // Ensure only 5 Imagimon are saved
        }

        string json = JsonUtility.ToJson(new ImagimonDataWrapper(imagimonList), true);
        File.WriteAllText(Application.persistentDataPath + filePath, json);
        Debug.Log("Imagimon data saved to " + Application.persistentDataPath + filePath);
    }

    public List<ImagimonData> LoadImagimonData()
    {
        string fullPath = Application.persistentDataPath + filePath;
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            ImagimonDataWrapper wrapper = JsonUtility.FromJson<ImagimonDataWrapper>(json);
            return wrapper.imagimonList;
        }

        return new List<ImagimonData>();
    }
}

[System.Serializable]
public class ImagimonDataWrapper
{
    public List<ImagimonData> imagimonList;

    public ImagimonDataWrapper(List<ImagimonData> imagimonList)
    {
        this.imagimonList = imagimonList;
    }
}
