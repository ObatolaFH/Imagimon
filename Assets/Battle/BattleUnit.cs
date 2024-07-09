using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] List<ImagimonBase> teamBase;
    [SerializeField] List<ImagimonBase> enemyTeamBase;
    [SerializeField] ImagimonBase _base;
    [SerializeField] int level;
    [SerializeField] bool isPlayerUnit;



    public Imagimon Imagimon { get; set; }


    public void Start()
    {
        if (isPlayerUnit)
        {
            LoadImagimonData();
        }
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

        for (int j = 0; j < imagimonList.Count; j++)
        {
            List<LearnableMove> chosenMoves = new List<LearnableMove>();

            for (int i = 0; i < 4; i++)
            {
                chosenMoves.Add(new LearnableMove(new MoveBase(imagimonList[j].attacks[i]), 1));
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
                ImagimonData data = imagimonList[j];
                Sprite sprite = data.BytesToSprite();

                ImagimonBase temp = ScriptableObject.CreateInstance<ImagimonBase>();

                //teamBase[j] = ScriptableObject.CreateInstance<ImagimonBase>();
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

                Debug.Log("Imagimon Name: " + data.imagimonName);
                Debug.Log("Imagimon Stats: " + string.Join(", ", data.stats));
                Debug.Log("Imagimon Attacks: " + string.Join(", ", data.attacks));
            }

        }
        _base = teamBase[0];
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
        _base = teamBase[index];
    }
}
