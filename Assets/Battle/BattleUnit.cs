using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] ImagimonBase _base;
    [SerializeField] int level;
    [SerializeField] bool isPlayerUnit;

    public Imagimon Imagimon { get; set; }

    public void Start()
    {
        LoadImagimonData();
    }

    public void Setup()
    {
        Imagimon = new Imagimon(_base, level);
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
        List<ImagimonData> imagimonList = SaveManager.Instance.LoadImagimonData();
        List<LearnableMove> chosenMoves = new List<LearnableMove>();

        for (int i = 0; i < 4; i++)
        {
            chosenMoves.Add(new LearnableMove(new MoveBase(imagimonList[0].attacks[i]), 1));
            //chosenMoves.Add(new Move(new MoveBase(imagimonList[0].attacks[i]), 10));
        }


        /*
        List<LearnableMove> chosenMoves = [Move(MoveBase(imagimonList[0].attacks[0]), 10),
            Move(MoveBase(imagimonList[0].attacks[1]), 10),
            Move(MoveBase(imagimonList[0].attacks[2]), 10),
            Move(MoveBase(imagimonList[0].attacks[3]), 10)];
        */

        if (imagimonList.Count > 0)
        {
            ImagimonData data = imagimonList[0];
            Sprite sprite = data.BytesToSprite();

            _base = ScriptableObject.CreateInstance<ImagimonBase>();
            _base.Initialize(
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

            Debug.Log("Imagimon Name: " + data.imagimonName);
            Debug.Log("Imagimon Stats: " + string.Join(", ", data.stats));
            Debug.Log("Imagimon Attacks: " + string.Join(", ", data.attacks));
        }
    }
}
