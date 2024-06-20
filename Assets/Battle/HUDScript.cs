using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDScript : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text levelText;
    [SerializeField] HPBar hpBar;


    public void SetData(Imagimon imagimon)
    {
        nameText.text = imagimon.Base.Name; 
        levelText.text = "Lvl " + imagimon.Level;
        hpBar.SetHP((float)imagimon.HP / imagimon.MaxHP);
    }
}