using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ImagimonTheGame
{
    public class ScoreDisplay : MonoBehaviour
    {
        public Text scoreText; // Reference to the UI Text element

        private void Start()
        {
            UpdateScoreText();
        }

        public void UpdateScoreText()
        {
            if (scoreText != null)
            {
                scoreText.text = "Player XP: " + ScoreManager.Instance.Score;
            }
            else
            {
                Debug.LogWarning("Score Text UI element is not assigned.");
            }
        }
    }
}
