using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ImagimonTheGame
{
    public class SaveManager : MonoBehaviour
    {
        private static SaveManager instance;
        public static SaveManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<SaveManager>();
                    if (instance == null)
                    {
                        GameObject go = new GameObject("SaveManager");
                        instance = go.AddComponent<SaveManager>();
                    }
                    DontDestroyOnLoad(instance.gameObject);
                }
                return instance;
            }
        }

        private const string filePath = "/imagimonData.json";
        private int playerXP;
        private const string xpKey = "PlayerXP";

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        public void SaveImagimonData(List<ImagimonData> imagimonList)
        {
            if (imagimonList.Count > 5)
            {
                imagimonList = imagimonList.GetRange(0, 5); // Ensure only 5 Imagimon are saved
            }

            SaveData data = new SaveData
            {
                imagimonList = imagimonList,
                playerXP = GetPlayerXP(),
            };
                            Debug.Log("playerrrrrrrr" + GetPlayerXP());


            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(Application.persistentDataPath + filePath, json);
            Debug.Log("Imagimon data saved to " + Application.persistentDataPath + filePath);
        }

        public List<ImagimonData> LoadImagimonData()
        {
            string fullPath = Application.persistentDataPath + filePath;
            if (File.Exists(fullPath))
            {
                string json = File.ReadAllText(fullPath);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                SetPlayerXP(data.playerXP);
                return data.imagimonList;
            }

            return new List<ImagimonData>();
        }

        public int GetPlayerXP()
        {
            
            string numberString = string.Format("{0}", playerXP);
            Debug.Log(PlayerPrefs.GetInt(numberString));
            return PlayerPrefs.GetInt(xpKey, playerXP);
        }

        public void SetPlayerXP(int xp)
        {
            playerXP = xp;
            Debug.Log("XXXXP" + xp);
            PlayerPrefs.SetInt(xpKey, xp);
            PlayerPrefs.Save();
        }
        
        public void AddPlayerXP(int xpToAdd)
        {
            int currentXP = GetPlayerXP();
            SetPlayerXP(currentXP + xpToAdd);
            Debug.Log("Player XXXXXP Updated: " + GetPlayerXP());
        }
        public int SavePlayerXP(int xp)
        {
            int currentXP = SaveManager.Instance.playerXP;
            currentXP += xp;
                SetPlayerXP(playerXP);
                return playerXP;
        }
    }

    [System.Serializable]
    public class SaveData
    {
        public List<ImagimonData> imagimonList;
        public int playerXP;
    }
}
