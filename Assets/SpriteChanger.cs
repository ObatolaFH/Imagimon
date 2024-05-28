using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChanger : MonoBehaviour
{
    public Dropdown dropdown;
    public Image targetImage;
    public List<Sprite> sprites;

    void Start()
    {
        // Clear any existing options in the dropdown
        dropdown.ClearOptions();

        // Create a list of dropdown options based on the sprite names
        List<string> options = new List<string>();
        foreach (var sprite in sprites)
        {
            options.Add(sprite.name);
        }

        // Add the options to the dropdown
        dropdown.AddOptions(options);

        // Add a listener to handle the dropdown value changes
        dropdown.onValueChanged.AddListener(delegate { DropdownValueChanged(dropdown); });

        // Set the initial sprite
        if (sprites.Count > 0)
        {
            targetImage.sprite = sprites[0];
        }
    }

    void DropdownValueChanged(Dropdown change)
    {
        int index = change.value;
        if (index >= 0 && index < sprites.Count)
        {
            targetImage.sprite = sprites[index];
        }
    }
}
