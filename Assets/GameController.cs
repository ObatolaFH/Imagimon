using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject playerImagimon;
    public GameObject npcImagimon;

    public AudioSource startSong;

    public int currentLifeBarStatus = 100;
    public int currentNpcLifeBarStatus = 100;

    public static int playerXP;
    public Text xpText;

    public GameObject playerLifeBar;
    public GameObject npcLifeBar;
    public GameObject playerLevel;
    public GameObject npcLevel;
    public bool playerDied = false;

    public bool gameIsRunning;

    public bool levelUp = false;

    public AudioSource fightSong;
    public AudioSource createSong;

    public Button homeButton;
    public Button nextLevelButton;
    public Button retryButton;

    public void playerTable(int playerLevel, int currentLifeBarStatus, int playerXP)
    {
     //
    }
    public void npcTable(int npcLevel, int currentNpcLifeBarStatus)
    {
    //
    }

    void Awake()
    {
        //playerTable();
        //playerImagimon();
        //npcTable();
        //npcImagimon();
        //StartCoroutine(StartGame());
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void StartGame()
    {
        gameIsRunning = true;
        startSong.Play();
    }

    void StopGame()
    {
        gameIsRunning = false;
        startSong.Stop();
    }

    void Update()
    {
        if (!gameIsRunning)
        {
            return;
        }
    }

    public void AddXP(int xpPoints)
    {
        playerXP += xpPoints;
        xpText.text = playerXP.ToString();
    }

    public void calculateLevel(int currentLevel)
    {
        int xpPoints = 0; //10 for level 2, 11 for lvl3, 12 for lvl4
        int level = 0;
        int levelThreshold = 10;

        if (xpPoints >= (currentLevel - 1) * ((currentLevel - 1) + 1) / 2 + currentLevel * 10)
        {
            level++;
        }


    }

    public void FightLogic()
    {
        //attacks, strengths, type of attack, Imagimon-type of npc

        if (currentNpcLifeBarStatus < 1)
        {
            winFight();
        }
        else
        {
            loseFight();
        }
    }
    public void winFight()
    {
        AddXP(playerXP);
        StopGame();
    }
    public void loseFight()
    {
        playerDied = true;
        StopGame();
    }
}