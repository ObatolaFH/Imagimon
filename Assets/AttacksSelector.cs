using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AttacksSelector : MonoBehaviour
{
    public List<Toggle> optionToggles;

    public List<string> attacks = new List<string>();

    public int maxSelections = 4;

    private int currentSelections = 0;

    void Start()
    {
        // Add listener to each toggle
        foreach (Toggle toggle in optionToggles)
        {
            toggle.onValueChanged.AddListener(delegate {
                OnToggleValueChanged(toggle);
            });
        }
    }

    void OnToggleValueChanged(Toggle changedToggle)
    {
        if (changedToggle.isOn)
        {
            if (currentSelections < maxSelections)
            {
                attacks.Add(changedToggle.name);
                currentSelections++;
            }
            else
            {
                changedToggle.isOn = false; // Deselect if over limit
            }
        }
        else
        {
            attacks.Remove(changedToggle.name);
            currentSelections--;
        }
    }
    //Get saved Attacks
}
