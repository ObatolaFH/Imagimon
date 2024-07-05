using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChanger : MonoBehaviour
{
    public Dropdown dropdown1;
    public Dropdown dropdown2;
    public Dropdown dropdown3;
    
    public List<Sprite> dogsprites;
    public List<Sprite> birdsprites;
    public List<Sprite> catsprites;
    public List<Sprite> foxsprites;
    public List<Sprite> raccoonsprites;
    public List<Sprite> shihtzu;
    public List<Sprite> beagle;
    public List<Sprite> labrador;
    public List<Sprite> bluebird;
    public List<Sprite> greenbird;
    public List<Sprite> greybird;
    public List<Sprite> redbird;
    public List<Sprite> greycat;
    public List<Sprite> orangecat;
    public List<Sprite> blackcat;
    public List<Sprite> whitecat;
    public List<Sprite> brownfox;
    public List<Sprite> yellowfox;
    public List<Sprite> greyraccoon;
    public List<Sprite> iceblueraccoon;
    private List<List<Sprite>> dogsprites2;
    private List<List<Sprite>> birdsprites2;
    private List<List<Sprite>> catsprites2;
    private List<List<Sprite>> foxsprites2;
    private List<List<Sprite>> raccoonsprites2;
    private List<List<Sprite>> rightspritelist;
    private List<List<List<Sprite>>> rightspritelist2;

    private int previousDropdown1Value = -1; // Placeholder blocking
    public Image targetImage;
    private Sprite chosenImagimonSprite;
    private string imagimonName;
    public InputField nameInputField;

    void Start()
    {
        dogsprites2 = new List<List<Sprite>> { shihtzu, beagle, labrador };
        birdsprites2 = new List<List<Sprite>> { bluebird, greenbird, greybird, redbird };
        catsprites2 = new List<List<Sprite>> { greycat, orangecat, blackcat, whitecat };
        foxsprites2 = new List<List<Sprite>> { brownfox, yellowfox };
        raccoonsprites2 = new List<List<Sprite>> { greyraccoon, iceblueraccoon };

        rightspritelist = new List<List<Sprite>> { dogsprites, birdsprites, catsprites, foxsprites, raccoonsprites };
        rightspritelist2 = new List<List<List<Sprite>>> { dogsprites2, birdsprites2, catsprites2, foxsprites2, raccoonsprites2 };
        
        InitializeDropdown(dropdown1, new List<string> { "Select Type", "Dog", "Bird", "Cat", "Fox", "Raccoon" });
        dropdown1.onValueChanged.AddListener(delegate { Dropdown1ValueChanged(dropdown1); });

        InitializeDropdown(dropdown2, new List<string> { "Select Color" });
        InitializeDropdown(dropdown3, new List<string> { "Select 2nd Color" });

        dropdown2.onValueChanged.AddListener(delegate { Dropdown2ValueChanged(dropdown2); });
        dropdown3.onValueChanged.AddListener(delegate { Dropdown3ValueChanged(dropdown3); });

        // Set placeholders
        dropdown1.value = 0;
        dropdown2.value = 0;
        dropdown3.value = 0;
        chosenImagimonSprite = targetImage.sprite;

        Dropdown1ValueChanged(dropdown1); // Ensure the initial selection triggers the method
        imagimonName = "";
    }

    void InitializeDropdown(Dropdown dropdown, List<string> options)
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }

    void Dropdown1ValueChanged(Dropdown change)
    {
        int index = change.value - 1; // Placeholder
        if (index < 0) {
            // Revert to the previous valid value if the placeholder is selected
            dropdown1.value = previousDropdown1Value + 1;
            return;
        }

        previousDropdown1Value = index; // Update the previous valid value

        List<string> options = new List<string>();
        foreach (var sprite in rightspritelist[index])
        {
            options.Add(sprite.name);
        }
        InitializeDropdown(dropdown2, options);
        
        // Clear dropdown3
        InitializeDropdown(dropdown3, new List<string>());

        // Update targetImage to the first sprite of the selected category
        if (rightspritelist[index].Count > 0)
        {
            targetImage.sprite = rightspritelist[index][0];
            chosenImagimonSprite = targetImage.sprite;

        }
        // Update dropdown2
        dropdown2.value = 0;
        Dropdown2ValueChanged(dropdown2); //if same choice
    }

    void Dropdown2ValueChanged(Dropdown change)
    {
        int parentIndex = dropdown1.value - 1; //Placeholder
        int parent2Index = dropdown2.value;
        int index = change.value;
        List<string> options = new List<string>();
        if (parentIndex < 0) return; // Placeholder selected, do nothing

        Debug.Log("parentIndex: " + parentIndex);
        Debug.Log("parent2Index: " + parent2Index);
        Debug.Log("currentIndex: " + index);

        if (parent2Index >= 0 && parent2Index < rightspritelist2[parentIndex].Count)
        {
            foreach (var sprite in rightspritelist2[parentIndex][parent2Index])
            {
                options.Add(sprite.name);
            }
            InitializeDropdown(dropdown3, options);

            // Update targetImage to the selected sprite
            if (index >= 0 && index < rightspritelist[parentIndex].Count)
            {
                targetImage.sprite = rightspritelist[parentIndex][index];
                chosenImagimonSprite = targetImage.sprite;

            }
        }
        else
        {
            Debug.LogWarning("parent2Index is out of bounds");
        }
    }

    void Dropdown3ValueChanged(Dropdown change)
    {
        int parentIndex = dropdown1.value - 1; // Placeholder
        int secParentIndex = dropdown2.value;
        int index = change.value;
        if (parentIndex < 0) return; // Placeholder selected, do nothing

        // Update targetImage to the selected sprite
        if (index >= 0 && index < rightspritelist2[parentIndex][secParentIndex].Count)
        {
            targetImage.sprite = rightspritelist2[parentIndex][secParentIndex][index];
            chosenImagimonSprite = targetImage.sprite;
        }
    }

    public void SaveImagimon()
    {
        //NAME
        imagimonName = nameInputField.text;
        Debug.Log("Imagimon's Name is " + imagimonName);

        //IMAGIMON-SPRITE
        Debug.Log("Saved ImagimonSprite: " + chosenImagimonSprite.name);

        //ATTACKS
        List<string> attacks = AttacksSelector.Instance.attacks;
        string allAttacks = string.Join(", ", attacks);
        Debug.Log("Saved ImagimonAttacks: " + allAttacks);
        foreach (string attack in attacks)
        {
            Debug.Log(attack);
        }
        //STATS
        List<int> stats = StatsManager.Instance.stats;
        Debug.Log("Stats: " + string.Join(", ", stats));
        foreach (int stat in stats)
        {
            Debug.Log(stat);
        }
        //TO DO: give SaveImagimon to ImagimonBase?
    }
}
