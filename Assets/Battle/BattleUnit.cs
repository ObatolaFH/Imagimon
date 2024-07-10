using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ImagimonTheGame
{
    public class BattleUnit : MonoBehaviour
    {
        [SerializeField] List<ImagimonBase> teamBase = new List<ImagimonBase>();
        [SerializeField] List<ImagimonBase> enemyTeamBase = new List<ImagimonBase>();
        [SerializeField] ImagimonBase _base;
        [SerializeField] int level;
        [SerializeField] bool isPlayerUnit;

        private SaveManager saveManager;

        public Imagimon Imagimon { get; set; }

        public void Start()
        {
            saveManager = SaveManager.Instance;
            if (saveManager == null)
            {
                Debug.LogError("SaveManager instance not found!");
            }
            else
            {
                Debug.Log("SaveManager instance successfully referenced: " + saveManager.name);
            }

            if (isPlayerUnit)
            {
                LoadImagimonData();
            }
        }

        public void Setup()
        {
            Imagimon = new Imagimon(_base, level);
            Debug.Log($"Setting up {Imagimon.Base.Name} with level {level}");

            if (isPlayerUnit)
            {
                GetComponent<Image>().sprite = Imagimon.Base.BackSprite;
            }
            else
            {
                GetComponent<Image>().sprite = Imagimon.Base.FrontSprite;
            }
        }

        public void LoadImagimonData()
{
    if (isPlayerUnit)
    {
        List<ImagimonData> imagimonList = SaveManager.Instance.LoadImagimonData();

        Debug.Log($"Loaded {imagimonList.Count} ImagimonData entries from save.");

        for (int j = 0; j < imagimonList.Count; j++)
        {
            Debug.Log($"Processing ImagimonData entry {j}: {imagimonList[j].imagimonName}");

            List<LearnableMove> chosenMoves = new List<LearnableMove>();

            for (int i = 0; i < 4 && i < imagimonList[j].attacks.Count; i++)
            {
                Debug.Log($"Adding attack {i}: {imagimonList[j].attacks[i]}");

                // Instantiate MoveBase using ScriptableObject.CreateInstance if it's a ScriptableObject
                MoveBase moveBase = ScriptableObject.CreateInstance<MoveBase>();
                moveBase.Name = imagimonList[j].attacks[i]; // Example assuming 'Name' is a property of MoveBase
                
                chosenMoves.Add(new LearnableMove(moveBase, 1));
            }

            if (imagimonList.Count > 0)
            {
                ImagimonData data = imagimonList[j];
                Sprite sprite = data.BytesToSprite();

                ImagimonBase temp = ScriptableObject.CreateInstance<ImagimonBase>();

                temp.Initialize(
                    data.imagimonName,
                    sprite,
                    sprite,
                    ImagimonType.Normal,
                    100, // maxHP
                    data.stats[0], // attack
                    data.stats[1], // defence
                    data.stats[3], // spAttack
                    data.stats[4], // spDefence
                    data.stats[2],  // initiative
                    chosenMoves
                );

                teamBase.Add(temp);

                Debug.Log($"Added Imagimon to teamBase: {temp.name}");
                Debug.Log("Imagimon Stats: " + string.Join(", ", data.stats));
                Debug.Log("Imagimon Attacks: " + string.Join(", ", data.attacks));
            }
        }

        if (teamBase.Count > 0)
        {
            _base = teamBase[0];
            Debug.Log($"Set _base to first Imagimon in teamBase: {_base.name}");
        }
        else
        {
            Debug.LogWarning("No Imagimons loaded into the team.");
        }
    }
    else
    {
        if (enemyTeamBase.Count > 0)
        {
            _base = enemyTeamBase[0];
            Debug.Log($"Set _base to first Imagimon in enemyTeamBase: {_base.name}");
        }
        else
        {
            Debug.LogWarning("No Imagimons in the enemy team base.");
        }
    }




        }

        public ImagimonBase _Base
        {
            get { return _base; }
            set { _base = value; }
        }

        public List<ImagimonBase> TeamBase
        {
            get { return teamBase; }
        }

        public void ChangeImagimon(int index)
        {

            List<ImagimonBase> baseList = isPlayerUnit ? teamBase : enemyTeamBase;

    if (index >= 0 && index < baseList.Count)
    {
        _base = baseList[index];
    }
    else
    {
        Debug.LogWarning($"Invalid index {index} for {(isPlayerUnit ? "player unit" : "enemy unit")} team base.");
        // Optionally handle the error scenario here, such as logging or displaying a message.
    }
            if (isPlayerUnit)
            {
                if (index >= 0 && index < teamBase.Count)
                {
                    _base = teamBase[index];
                }
                else
                {
                    Debug.LogWarning("Invalid index for player unit team base.");
                }
            }
            else
            {
                if (index >= 0 && index < enemyTeamBase.Count)
                {
                    _base = enemyTeamBase[index];
                }
                else
                {
                    Debug.LogWarning("Invalid index for enemy unit team base.");
                }
            }
        }
    }
}
