using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState
{
    Start, PlayerMove, EnemyMove, Busy
}

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




    private int currentImagimonIndex = 0; 

    int attackChoice;

    BattleState state;

    private bool switchState = false;

    private void Start()
    {
        StartCoroutine(SetupBattle());

    }

    public IEnumerator SetupBattle()
    {
        playerUnit.Setup();
        enemyUnit.Setup();
        playerHud.SetData(playerUnit.Imagimon);
        enemyHud.SetData(enemyUnit.Imagimon);

        dialogBox.SetMoveNames(playerUnit.Imagimon.Moves);

        yield return dialogBox.SetDialog($"You are fighting against {enemyUnit.Imagimon.Base.Name}.");
        yield return new WaitForSeconds(1f);

        PlayerAction();
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
            yield return dialogBox.SetDialog($"{enemyUnit.Imagimon.Base.Name} Fainted");
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
        switchState = true;

        if (switchState)
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
        else
        {

            attack1Button.onClick.RemoveAllListeners();
            attack2Button.onClick.RemoveAllListeners();
            attack3Button.onClick.RemoveAllListeners();
            attack4Button.onClick.RemoveAllListeners();


            PlayerAction();
        }
    }

    IEnumerator ImagimonSelector(int imagimonIndex)
    {
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


        switchState = false;

        PlayerAction();
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
