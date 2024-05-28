using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImagimonController : MonoBehaviour
{
   // public string ImagimonName;
   // public string imagimonType;
    //public string ImagimonType;
    public GameObject PrimeColor;
    public GameObject SecondaryColor;
    public GameObject SignatureMove;
    public GameObject ImagimonStats;

    //public Image oldImage;
   // public Sprite newImage;
    //public List<Sprite> sprites;

    //public string chosenImagimonType;
   // string chosenPrimeColor = "default";
/*
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
    public void ImageChange()
    {
        oldImage.sprite = newImage;
    }
 */

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