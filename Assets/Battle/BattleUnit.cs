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

    public void Setup()
    {
        Imagimon = new Imagimon(_base, level);
        if (isPlayerUnit )
        {
            GetComponent<Image>().sprite = Imagimon.Base.BackSprite;
        }
        else
        {
            GetComponent<Image>().sprite = Imagimon.Base.FrontSprite;
        }
    }

}
