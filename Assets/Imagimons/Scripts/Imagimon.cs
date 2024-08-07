using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImagimonTheGame
{
public class Imagimon
{
    public ImagimonBase Base { get; set; }
    public int Level {get; set; }
    
    public int HP { get; set; }

    public List<Move> Moves { get; set; }

    public Imagimon(ImagimonBase iBase, int iLevel)
    {
        Base = iBase;
        Level = iLevel;
        HP = MaxHP;

        Moves = new List<Move>();
        foreach (var move in Base.LearnableMoves)
        {
            if (move.Level <= Level)
                Moves.Add(new Move(move.Base, 15));

            if (Moves.Count >= 4)
                break;
            
        }
    }

    public int Attack
    {
        get { return Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5; }
    }

    public int Defence
    {
        get { return Mathf.FloorToInt((Base.Defence * Level) / 100f) + 5; }
    }

    public int SpAttack
    {
        get { return Mathf.FloorToInt((Base.SpAttack * Level) / 100f) + 5; }
    }

    public int SpDefence
    {
        get { return Mathf.FloorToInt((Base.SpDefence * Level) / 100f) + 5; }
    }

    public int Initiative
    {
        get { return Mathf.FloorToInt((Base.Initiative * Level) / 100f) + 5; }
    }

    public int MaxHP
    {
        get { return Mathf.FloorToInt((Base.MaxHP * Level) / 100f) + 10; }
    }

    public bool TakeDamage(Move move, Imagimon attacker)
    {
        float modifiers = Random.Range(0.85f, 1f);
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * move.Base.Power * ((float)attacker.Attack / Defence) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            return true;
        }
        return false;
    }

    public Move GetRandomMove()
    {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }
}
}
