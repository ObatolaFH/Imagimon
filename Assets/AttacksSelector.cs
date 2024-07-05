using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AttacksSelector : MonoBehaviour
{
    public static AttacksSelector Instance { get; private set; }
    public List<Toggle> optionToggles;

    public List<string> attacks = new List<string>();
    public int maxSelections = 4;
    private int currentSelections = 0;

    private Dictionary<string, bool> toggleStates = new Dictionary<string, bool>();
     
     void Start()
     {
        foreach (Toggle toggle in optionToggles)
        toggle.onValueChanged.AddListener(delegate {
            OnToggleValueChanged(toggle);
            });
     }
     
     private void Awake()
    {
        // Ensure that there is only one instance of the AttackManager
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

    void OnToggleValueChanged(Toggle changedToggle)
    {
        // Get the previous state of the toggle
        bool previousState = toggleStates.ContainsKey(changedToggle.name) ? toggleStates[changedToggle.name] : false;

        if (changedToggle.isOn)
        {
            if (currentSelections < maxSelections)
            {
                // Add the attack if the toggle is turned on and we are under the max limit
                if (!previousState) // Only add if it wasn't already on
                {
                    attacks.Add(changedToggle.name);
                    currentSelections++;
                }
            } else {
                changedToggle.isOn = false;
                }
        } else {
            // Remove the attack if the toggle is turned off
            if (previousState) {
                attacks.Remove(changedToggle.name);
                currentSelections--;
            }
        }
        // Update the previous state
        toggleStates[changedToggle.name] = changedToggle.isOn;
    }   
    
    
    /*
    // In order to access the attacks list from another script:
    
        List<string> attacks = AttacksSelector.Instance.attacks;

        // Example usage: Print all attacks
        foreach (string attack in attacks)
        {
            Debug.Log(attack);
        }
        */
}
