using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChanger : MonoBehaviour
{
    public Dropdown dropdown1;
    public Dropdown dropdown2;
    public Dropdown dropdown3;
    public Dropdown dropdown4;
    public Image targetImage;

    public List<Sprite> dogsprites;
    public List<Sprite> birdsprites;
    public List<Sprite> catsprites;
    public List<Sprite> foxsprites;
    public List<Sprite> raccoonsprites;

    private List<List<Sprite>> rightspritelist;

    void Start()
    {
        rightspritelist = new List<List<Sprite>> { dogsprites, birdsprites, catsprites, foxsprites, raccoonsprites };

        InitializeDropdown(dropdown1, new List<string> { "Dog", "Bird", "Cat", "Fox", "Raccoon" });
        dropdown1.onValueChanged.AddListener(delegate { Dropdown1ValueChanged(dropdown1); });

        InitializeDropdown(dropdown2, new List<string>());
        InitializeDropdown(dropdown3, new List<string>());
        InitializeDropdown(dropdown4, new List<string>());

        dropdown2.onValueChanged.AddListener(delegate { Dropdown2ValueChanged(dropdown2); });
        dropdown3.onValueChanged.AddListener(delegate { Dropdown3ValueChanged(dropdown3); });
        dropdown4.onValueChanged.AddListener(delegate { Dropdown4ValueChanged(dropdown4); });
    }

    void InitializeDropdown(Dropdown dropdown, List<string> options)
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }

    void Dropdown1ValueChanged(Dropdown change)
    {
        int index = change.value;
        List<string> options = new List<string>();
        foreach (var sprite in rightspritelist[index])
        {
            options.Add(sprite.name);
        }
        InitializeDropdown(dropdown2, options);
        
        // Clear subsequent dropdowns
        InitializeDropdown(dropdown3, new List<string>());
        InitializeDropdown(dropdown4, new List<string>());

        // Update targetImage to the first sprite of the selected category
        if (rightspritelist[index].Count > 0)
        {
            targetImage.sprite = rightspritelist[index][0];
        }
    }

    void Dropdown2ValueChanged(Dropdown change)
    {
        int parentIndex = dropdown1.value;
        int index = change.value;

        List<string> options = new List<string>();
        foreach (var sprite in rightspritelist[parentIndex])
        {
            options.Add(sprite.name);
        }
        InitializeDropdown(dropdown3, options);

        // Clear subsequent dropdown
        InitializeDropdown(dropdown4, new List<string>());

        // Update targetImage to the selected sprite
        if (index >= 0 && index < rightspritelist[parentIndex].Count)
        {
            targetImage.sprite = rightspritelist[parentIndex][index];
        }
    }

    void Dropdown3ValueChanged(Dropdown change)
    {
        int parentIndex = dropdown1.value;
        int index = change.value;

        List<string> options = new List<string>();
        foreach (var sprite in rightspritelist[parentIndex])
        {
            options.Add(sprite.name);
        }
        InitializeDropdown(dropdown4, options);

        // Update targetImage to the selected sprite
        if (index >= 0 && index < rightspritelist[parentIndex].Count)
        {
            targetImage.sprite = rightspritelist[parentIndex][index];
        }
    }

    void Dropdown4ValueChanged(Dropdown change)
    {
        int parentIndex = dropdown1.value;
        int index = change.value;

        // Update targetImage to the selected sprite
        if (index >= 0 && index < rightspritelist[parentIndex].Count)
        {
            targetImage.sprite = rightspritelist[parentIndex][index];
        }
    }
}
