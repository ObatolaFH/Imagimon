using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import the UI namespace


namespace ImagimonTheGame
{
    public class GameInitializer : MonoBehaviour
    {
        public int xp;
        public Text xpText; // Reference to the UI Text element

        private void Start()
        {
            List<ImagimonData> imagimonList = SaveManager.Instance.LoadImagimonData();
            xp = SaveManager.Instance.GetPlayerXP();
            Debug.Log("Game initialized. Player XP: " + xp);

            if (xpText != null)
            {
                xpText.text = "Player XP: " + xp;
            }
            else
            {
                Debug.LogWarning("XP Text UI element is not assigned.");
            }
        }
    }
}
