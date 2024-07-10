using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum BattleState
{
    Start, PlayerMove, EnemyMove, Busy
}
namespace ImagimonTheGame {
public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] HUDScript playerHud;
    [SerializeField] HUDScript enemyHud;
    [SerializeField] BattleDialogBox dialogBox;

    [SerializeField] Button attack1Button;
    [SerializeField] Button attack2Button;
    [SerializeField] Button attack3Button;
    [SerializeField] Button attack4Button;
    [SerializeField] Button switchButton;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Text gameOver;


    private int[] playerLives = { 100, 100, 100, 100, 100 };
    private int[] enemyLives = { 100, 100, 100, 100, 100 };


    private int currentImagimonIndex = 0;
    private int enemyImagimonIndex = 0;
    public ScoreDisplay scoreDisplay; // Reference to the ScoreDisplay script


    int attackChoice;

    BattleState state;

    

    private void Start()
    {
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
        if (ScoreManager.Instance == null)
            {
                Debug.LogError("ScoreManager instance is not found!");
            }
        StartCoroutine(SetupBattle());
        
        

    }

    public IEnumerator SetupBattle()
    {
        playerUnit.Setup();
        enemyUnit.Setup();
        playerHud.SetData(playerUnit.Imagimon);
        enemyHud.SetData(enemyUnit.Imagimon);
        gameOverPanel.SetActive(false);

        dialogBox.SetMoveNames(playerUnit.Imagimon.Moves);

        yield return dialogBox.SetDialog($"You are fighting against {enemyUnit.Imagimon.Base.Name}.");
        yield return new WaitForSeconds(1f);

        if(playerUnit.Imagimon.Base.Initiative > enemyUnit.Imagimon.Base.Initiative)
        {
            Debug.Log("fast");
            PlayerAction();
        }
        else if(playerUnit.Imagimon.Base.Initiative == enemyUnit.Imagimon.Base.Initiative)
        {
            int ran = Random.Range(0, 1);
            if (ran == 0)
            {

                PlayerAction();
            }
            else
            {
                StartCoroutine(EnemyMove());
            }
        }
        else
        {
            Debug.Log("slow");
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy;
        var move = playerUnit.Imagimon.Moves[attackChoice];
        yield return dialogBox.SetDialog($"{playerUnit.Imagimon.Base.Name} used {move.Base.Name}");
        yield return new WaitForSeconds(1f);

        bool isFainted = enemyUnit.Imagimon.TakeDamage(move, playerUnit.Imagimon);
        yield return enemyHud.UpdateHP();

        if (isFainted)
        {
            yield return dialogBox.SetDialog($"{enemyUnit.Imagimon.Base.Name} Fainted");
            enemyLives[enemyImagimonIndex] = 0;
            StartCoroutine(EnemyDied());
        }
        else
        {
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator EnemyMove()
    {
        state = BattleState.EnemyMove;
        var move = enemyUnit.Imagimon.GetRandomMove();
        
        yield return dialogBox.SetDialog($"{enemyUnit.Imagimon.Base.Name} used {move.Base.Name}");
        yield return new WaitForSeconds(1f);

        bool isFainted = playerUnit.Imagimon.TakeDamage(move, enemyUnit.Imagimon);
        yield return playerHud.UpdateHP();

        if (isFainted)
        {
            yield return dialogBox.SetDialog($"{playerUnit.Imagimon.Base.Name} Fainted");
            playerLives[currentImagimonIndex] = 0;
            Debug.Log("You ded1");
            StartCoroutine(YouDied());

        }
        else
        {
            PlayerAction();
        }
    }

    void PlayerAction()
    {
        dialogBox.SetMoveNames(playerUnit.Imagimon.Moves);
        state = BattleState.PlayerMove;
        StartCoroutine(dialogBox.SetDialog($"Choose an attack:"));
         /*if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.Score += 10;
            }
            else
            {
                Debug.LogError("ScoreManager instance is not valid!");
            }
         if (scoreDisplay != null)
            {
                scoreDisplay.UpdateScoreText();
            }
            else
            {
                Debug.LogError("ScoreDisplay is not assigned!");
            }*/


        attack1Button.onClick.AddListener(() => OnAttackButtonPressed(0));
        attack2Button.onClick.AddListener(() => OnAttackButtonPressed(1));
        attack3Button.onClick.AddListener(() => OnAttackButtonPressed(2));
        attack4Button.onClick.AddListener(() => OnAttackButtonPressed(3));
        switchButton.onClick.AddListener(() => SwitchImagimon());
    }

    void OnAttackButtonPressed(int attackIndex)
    {
        if (state != BattleState.PlayerMove)
        {
            return;
        }

        attack1Button.interactable = false;
        attack2Button.interactable = false;
        attack3Button.interactable = false;
        attack4Button.interactable = false;

        StartCoroutine(HandleAttack(attackIndex));
    }


    IEnumerator HandleAttack(int attackIndex)
    {
        state = BattleState.Busy;
        attackChoice = attackIndex;


        yield return PerformPlayerMove();

        yield return new WaitForSeconds(1f);


        attack1Button.interactable = true;
        attack2Button.interactable = true;
        attack3Button.interactable = true;
        attack4Button.interactable = true;

        state = BattleState.EnemyMove;

    }

    private void SwitchImagimon()
    {

        attack1Button.onClick.RemoveAllListeners();
        attack2Button.onClick.RemoveAllListeners();
        attack3Button.onClick.RemoveAllListeners();
        attack4Button.onClick.RemoveAllListeners();
        switchButton.onClick.RemoveAllListeners();



        int[] indTemp = { 0, 1, 2, 3, 4 };
        int[] indTemp_new = new int[indTemp.Length - 1];

        for (int i = 0; i < indTemp.Length; i++)
        {
            if (currentImagimonIndex == indTemp[i])
            {
                for (int j = 0, k = 0; j < indTemp.Length; j++)
                {
                    if (i != j)
                    {
                        indTemp_new[k] = indTemp[j];
                        k++;
                    }
                }
            }
            break;
        }

        TMP_Text attack1ButtonText = attack1Button.GetComponentInChildren<TMP_Text>();
        TMP_Text attack2ButtonText = attack2Button.GetComponentInChildren<TMP_Text>();
        TMP_Text attack3ButtonText = attack3Button.GetComponentInChildren<TMP_Text>();
        TMP_Text attack4ButtonText = attack4Button.GetComponentInChildren<TMP_Text>();

        attack1ButtonText.text = $"{playerUnit.TeamBase[indTemp_new[0]].Name}";
        attack2ButtonText.text = $"{playerUnit.TeamBase[indTemp_new[1]].Name}";
        attack3ButtonText.text = $"{playerUnit.TeamBase[indTemp_new[2]].Name}";
        attack4ButtonText.text = $"{playerUnit.TeamBase[indTemp_new[3]].Name}";

        attack1Button.onClick.AddListener(() => StartCoroutine(ImagimonSelector(indTemp_new[0])));
        attack2Button.onClick.AddListener(() => StartCoroutine(ImagimonSelector(indTemp_new[1])));
        attack3Button.onClick.AddListener(() => StartCoroutine(ImagimonSelector(indTemp_new[2])));
        attack4Button.onClick.AddListener(() => StartCoroutine(ImagimonSelector(indTemp_new[3])));
        switchButton.onClick.AddListener(() => StartCoroutine(ImagimonSelector(currentImagimonIndex)));

        Debug.Log(playerUnit._Base.Name);
    }

    IEnumerator ImagimonSelector(int imagimonIndex)
    {
        if (playerLives[imagimonIndex] == 0) {
            yield return dialogBox.SetDialog($"{playerUnit.Imagimon.Base.Name} has alredy fainted");
            yield return new WaitForSeconds(1f);
        }
        else {
            playerUnit.ChangeImagimon(imagimonIndex);
            currentImagimonIndex = imagimonIndex;
            playerUnit.Setup();
            playerHud.SetData(playerUnit.Imagimon);
            yield return dialogBox.SetDialog($"You chose {playerUnit.Imagimon.Base.Name}");
            yield return new WaitForSeconds(1f);
            state = BattleState.Start;

            attack1Button.onClick.RemoveAllListeners();
            attack2Button.onClick.RemoveAllListeners();
            attack3Button.onClick.RemoveAllListeners();
            attack4Button.onClick.RemoveAllListeners();
            switchButton.onClick.RemoveAllListeners();


            PlayerAction();
        }
    }

    public IEnumerator YouDied()
    {
        Debug.Log("You ded2");
        if (playerLives[0] + playerLives[1] + playerLives[2] + playerLives[3] + playerLives[4] == 0)
        {
            yield return dialogBox.SetDialog($"You lost");
            yield return new WaitForSeconds(1f);
            yield return null; ;
        }

        for(int i = 0; i < 10; i++) { 
            if (playerLives[(i + 5) % 5] != 0){
                Debug.Log("You ded3");
                StartCoroutine(ImagimonSelector((i + 5) % 5));
                Debug.Log("You ded 4 i = " + (i + 5) % 5);
                Debug.Log(playerUnit._Base.Name);

                if (playerUnit.Imagimon.Base.Initiative > enemyUnit.Imagimon.Base.Initiative)
                {
                    PlayerAction();
                }
                else if (playerUnit.Imagimon.Base.Initiative == enemyUnit.Imagimon.Base.Initiative)
                {
                    int ran = Random.Range(0, 1);
                    if (ran == 0)
                    {
                        PlayerAction();
                    }
                    else
                    {
                        StartCoroutine(EnemyMove());
                    }
                }
                else
                {
                    StartCoroutine(EnemyMove());
                }

                break;
            }
        }
    }

    public IEnumerator EnemyDied()
    {
        if (enemyLives[0] + enemyLives[1] + enemyLives[2] + enemyLives[3] + enemyLives[4] == 0)
        {
            yield return dialogBox.SetDialog($"You won");
            SaveManager.Instance.AddPlayerXP(10);
            Debug.Log("Player won the battle! 10 XP added.");
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.Score += 10;
            }
            else
            {
                Debug.LogError("ScoreManager instance is not valid!");
            }
         if (scoreDisplay != null)
            {
                scoreDisplay.UpdateScoreText();
            }
            else
            {
                Debug.LogError("ScoreDisplay is not assigned!");
            }
            
            
            //gameOverPanel.SetActive(true);
            //gameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            yield return null; ;
            //gameOver.gameObject.SetActive(false);
            //gameOverPanel.SetActive(false);
        }

        for (int i = 0; i < 10; i++)
        {
            if (enemyLives[(i + 5) % 5] != 0)
            {
                enemyUnit.ChangeImagimon((i + 5) % 5);
                enemyUnit.ChangeImagimon(enemyImagimonIndex);
                enemyUnit.Setup();
                enemyHud.SetData(enemyUnit.Imagimon);
                Debug.Log($"Enemy chose {enemyUnit.Imagimon.Base.Name}");

                yield return dialogBox.SetDialog($"Your enemy chose {enemyUnit.Imagimon.Base.Name}");
                yield return new WaitForSeconds(1f);
                state = BattleState.Start;

                enemyImagimonIndex++;

                if (playerUnit.Imagimon.Base.Initiative > enemyUnit.Imagimon.Base.Initiative)
                {
                    PlayerAction();
                }
                else if (playerUnit.Imagimon.Base.Initiative == enemyUnit.Imagimon.Base.Initiative)
                {
                    int ran = Random.Range(0, 1);
                    if (ran == 0)
                    {
                        PlayerAction();
                    }
                    else
                    {
                        StartCoroutine(EnemyMove());
                    }
                }
                else
                {
                    StartCoroutine(EnemyMove());
                }

                break;
            }
        }
    }

    private void Update()
    {
        /*
        if (state == BattleState.PlayerMove)
        {
            StartCoroutine(PerformPlayerMove());
            //state = BattleState.EnemyMove;
        }
        */
    }
}
}