using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImagimonTheGame
{
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

    public void Initialize(string name, Sprite frontSprite, Sprite backSprite, ImagimonType type,
                           int maxHP, int attack, int defence, int spAttack, int spDefence, int initiative,
                           List<LearnableMove> learnableMoves)
    {
        this.name = name;
        this.frontSprite = frontSprite;
        this.backSprite = backSprite;
        this.type = type;
        this.maxHP = maxHP;
        this.attack = attack;
        this.defence = defence;
        this.spAttack = spAttack;
        this.spDefence = spDefence;
        this.initiative = initiative;
        this.learnableMoves = learnableMoves;
    }


    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public Sprite FrontSprite
    {
        get { return frontSprite; }
        set { frontSprite = value; }
    }

    public Sprite BackSprite
    {
        get { return backSprite; }
        set { backSprite = value; }
    }

    public ImagimonType Type
    {
        get { return type; }
        set { type = value; }
    }

    public int MaxHP
    {
        get { return maxHP; }
        set { maxHP = value; }
    }

    public int Attack
    {
        get { return attack; }
        set { attack = value; }
    }

    public int Defence
    {
        get { return defence; }
        set { defence = value; }
    }

    public int SpAttack
    {
        get { return spAttack; }
        set { spAttack = value; }
    }

    public int SpDefence
    {
        get { return spDefence; }
        set { spDefence = value; }
    }

    public int Initiative
    {
        get { return initiative; }
        set { initiative = value; }
    }

    public List<LearnableMove> LearnableMoves 
    { 
        get { return learnableMoves; } 
        set { learnableMoves = value; }
    }
}

[System.Serializable]
public class LearnableMove
{
    [SerializeField] MoveBase moveBase;
    [SerializeField] int level;

    public LearnableMove(MoveBase moveBase, int level)
    {
        this.moveBase = moveBase;
        this.level = level;
    }

    public MoveBase Base
    {
        get { return moveBase; }
        set { moveBase = value; }
    }

    public int Level
    {
        get { return level; }
        set { level = value; }
    }

}

public enum ImagimonType
{
    Normal,
    Fire,
    Water

}
}

