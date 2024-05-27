using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropDownController : MonoBehaviour
{
    string TypeDropDown = "empty";
    public void DropDownSample(int index)
    {
        switch (index)
        {
            case 0: TypeDropDown = "Bird"; break;
            case 1: TypeDropDown = "Cow"; break;
            case 2: TypeDropDown = "Fox"; break;
            case 3: TypeDropDown = "Cat"; break;
        }
    }
   
}
