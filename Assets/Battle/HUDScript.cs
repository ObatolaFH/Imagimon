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

    Imagimon _imagimon;

    public void SetData(Imagimon imagimon)
    {
        _imagimon = imagimon;
        nameText.text = imagimon.Base.Name; 
        levelText.text = "Lvl " + imagimon.Level;
        hpBar.SetHP((float)imagimon.HP / imagimon.MaxHP);
    }

    public void UpdateHP()
    {
        hpBar.SetHP((float)_imagimon.HP / _imagimon.MaxHP);
    }
}
