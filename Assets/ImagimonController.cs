using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImagimonController : MonoBehaviour
{
    public GameObject ImagimonName;
    public GameObject ImagimonType;
    public GameObject PrimeColor;
    public GameObject SecondaryColor;
    public GameObject SignatureMove;
    public GameObject ImagimonStats;

    string Option1 = "cat";
    string Option2 = "bird";
    string Option3 = "cow";
    string Option4 = "racoon";

    string chosenImagimonType = "default";
    string chosenPrimeColor = "default";

    //if(dropdown OnCLick "Cat"){then bool isChanged true & chosenImagimonType = Cat}
    //switch (case 1: case 2: case 3: case 4:)


    public bool isChanged;
    //Image infusion here
    public void Update()
    {
        if (!isChanged)
        {
            ImagimonType = chosenImagimonType;
            //PrimeColor = chosenPrimeColor;

            isChanged = false;
        }
    }

    /*IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        
    }*/
}