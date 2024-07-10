using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ImagimonTheGame
{
public class TeamManager : MonoBehaviour
{
    [SerializeField] private Image imagimon1Image;
    [SerializeField] private Text imagimon1Name;
    [SerializeField] private Button imagimon1DownButton;

    [SerializeField] private Image imagimon2Image;
    [SerializeField] private Text imagimon2Name;
    [SerializeField] private Button imagimon2UpButton;
    [SerializeField] private Button imagimon2DownButton;

    [SerializeField] private Image imagimon3Image;
    [SerializeField] private Text imagimon3Name;
    [SerializeField] private Button imagimon3UpButton;
    [SerializeField] private Button imagimon3DownButton;

    [SerializeField] private Image imagimon4Image;
    [SerializeField] private Text imagimon4Name;
    [SerializeField] private Button imagimon4UpButton;
    [SerializeField] private Button imagimon4DownButton;

    [SerializeField] private Image imagimon5Image;
    [SerializeField] private Text imagimon5Name;
    [SerializeField] private Button imagimon5UpButton;

    private List<ImagimonData> imagimonList;
    private SaveManager saveManager;

    private void Start()
    {
        saveManager = SaveManager.Instance;
        if (saveManager == null)
        {
            Debug.LogError("SaveManager instance not found!");
        }
        else
        {
            Debug.Log("SaveManager instance successfully referenced.");
            }
            SetImagimons();

        // Add listeners to buttons
        imagimon1DownButton.onClick.AddListener(() => MoveImagimon(0, 1));
        imagimon2UpButton.onClick.AddListener(() => MoveImagimon(1, -1));
        imagimon2DownButton.onClick.AddListener(() => MoveImagimon(1, 1));
        imagimon3UpButton.onClick.AddListener(() => MoveImagimon(2, -1));
        imagimon3DownButton.onClick.AddListener(() => MoveImagimon(2, 1));
        imagimon4UpButton.onClick.AddListener(() => MoveImagimon(3, -1));
        imagimon4DownButton.onClick.AddListener(() => MoveImagimon(3, 1));
        imagimon5UpButton.onClick.AddListener(() => MoveImagimon(4, -1));
    }

    public void SetImagimons()
    {
        imagimonList = SaveManager.Instance.LoadImagimonData();

        if (imagimonList.Count > 0) UpdateImagimonUI(imagimon1Image, imagimon1Name, imagimonList[0]);
        if (imagimonList.Count > 1) UpdateImagimonUI(imagimon2Image, imagimon2Name, imagimonList[1]);
        if (imagimonList.Count > 2) UpdateImagimonUI(imagimon3Image, imagimon3Name, imagimonList[2]);
        if (imagimonList.Count > 3) UpdateImagimonUI(imagimon4Image, imagimon4Name, imagimonList[3]);
        if (imagimonList.Count > 4) UpdateImagimonUI(imagimon5Image, imagimon5Name, imagimonList[4]);
    }

    private void UpdateImagimonUI(Image imageComponent, Text nameComponent, ImagimonData data)
    {
        Sprite sprite = data.BytesToSprite();
        imageComponent.sprite = sprite;
        nameComponent.text = data.imagimonName;
    }

    private void MoveImagimon(int index, int direction)
    {
        int newIndex = index + direction;

        if (newIndex < 0 || newIndex >= imagimonList.Count)
            return; // Out of bounds

        // Swap positions in the list
        ImagimonData temp = imagimonList[index];
        imagimonList[index] = imagimonList[newIndex];
        imagimonList[newIndex] = temp;

        // Save the updated list
        SaveManager.Instance.SaveImagimonData(imagimonList);

        // Refresh UI
        SetImagimons();
    }
}
}