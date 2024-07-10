using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImagimonTheGame
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public int Score
        {
            get { return PlayerPrefs.GetInt("Score", 0); }
            set { PlayerPrefs.SetInt("Score", value); }
        }
    }
}
