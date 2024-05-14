using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    void Start()
    {
        print("Scene is loaded");
    }

    void Update()
    {
        
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HomeScene()
    {
        SceneManager.LoadScene("MenuScene");
    }
}


