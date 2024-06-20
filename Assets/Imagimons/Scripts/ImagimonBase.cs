using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Imagimon", menuName = "Imagimon/Create new Imagimon")]
public class ImagimonBase : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;
    [SerializeField] ImagimonType type;

    [SerializeField] int maxHP;
    [SerializeField] int attack;
    [SerializeField] int defence;
    [SerializeField] int spAttack;
    [SerializeField] int spDefence;
    [SerializeField] int initiative;

    [SerializeField] List<LearnableMove> learnableMoves;


    public string Name
    {
        get { return name; }
    }

    public Sprite FrontSprite
    {
        get { return frontSprite; }
    }

    public Sprite BackSprite
    {
        get { return backSprite; }
    }

    public ImagimonType Type
    {
        get { return type; }
    }

    public int MaxHP
    {
        get { return maxHP; }
    }

    public int Attack
    {
        get { return attack; }
    }

    public int Defence
    {
        get { return defence; }
    }

    public int SpAttack
    {
        get { return spAttack; }
    }

    public int SpDefence
    {
        get { return spDefence; }
    }

    public int Initiative
    {
        get { return initiative; }
    }

    public List<LearnableMove> LearnableMoves 
    { 
        get { return learnableMoves; } 
    }
}

[System.Serializable]
public class LearnableMove
{
    [SerializeField] MoveBase moveBase;
    [SerializeField] int level;

    public MoveBase Base
    {
        get { return moveBase; }
    }

    public int Level
    {
        get { return level; }
    }

}

public enum ImagimonType
{
    Normal,
    Fire,
    Water

}

