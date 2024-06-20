using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Imagimon/Create new move")]
public class MoveBase : ScriptableObject
{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] ImagimonType type;
    [SerializeField] MoveType moveType;
    [SerializeField] int power;
    [SerializeField] int accuracy;
    [SerializeField] int pp;

    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }

    public ImagimonType Type
    {
        get { return type; }
    }

    public MoveType MoveType 
    { 
        get { return moveType; } 
    }

    public int Power
    {
        get { return power; }
    }

    public int Accuracy
    {
        get { return accuracy; }
    }

    public int PP
    {
        get { return pp; }
    }
}

public enum MoveType
{
    Physical,
    Special
}
