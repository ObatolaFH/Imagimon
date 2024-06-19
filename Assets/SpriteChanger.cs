using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChanger : MonoBehaviour
{
    public Dropdown dropdown1;
    public Dropdown dropdown2;
    public Dropdown dropdown3;
    public Image targetImage;
    public Image imagimonImage;
    public List<Sprite> dogsprites;
    public List<Sprite> birdsprites;
    public List<Sprite> catsprites;
    public List<Sprite> foxsprites;
    public List<Sprite> raccoonsprites;
    public List<Sprite> dogsprites2;
    public List<Sprite> birdsprites2;
    public List<Sprite> catsprites2;
    public List<Sprite> foxsprites2;
    public List<Sprite> raccoonsprites2;
    private List<List<Sprite>> rightspritelist;
    private List<List<Sprite>> rightspritelist2;

    void Start()
    {
        rightspritelist = new List<List<Sprite>> { dogsprites, birdsprites, catsprites, foxsprites, raccoonsprites };
        rightspritelist2 = new List<List<Sprite>> { dogsprites2, birdsprites2, catsprites2, foxsprites2, raccoonsprites2 };
        InitializeDropdown(dropdown1, new List<string> { "Dog", "Bird", "Cat", "Fox", "Raccoon" });
        dropdown1.onValueChanged.AddListener(delegate { Dropdown1ValueChanged(dropdown1); });

        InitializeDropdown(dropdown2, new List<string>());
        InitializeDropdown(dropdown3, new List<string>());

        dropdown2.onValueChanged.AddListener(delegate { Dropdown2ValueChanged(dropdown2); });
        dropdown3.onValueChanged.AddListener(delegate { Dropdown3ValueChanged(dropdown3); });
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
        
        // Clear dropdown
        InitializeDropdown(dropdown3, new List<string>());

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
        foreach (var sprite in rightspritelist2[parentIndex])
        {
            options.Add(sprite.name);
        }
        InitializeDropdown(dropdown3, options);

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

        // Update targetImage to the selected sprite
        if (index >= 0 && index < rightspritelist2[parentIndex].Count)
        {
            targetImage.sprite = rightspritelist2[parentIndex][index];
            //imagimonImage.sprite = targetImage.sprite;
        }
        // Store the chosen sprite in a chosen Imagimon variable
    }

}
