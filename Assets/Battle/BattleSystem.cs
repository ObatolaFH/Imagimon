using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    int attackChoice;

    BattleState state;

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
        enemyHud.UpdateHP();

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
        playerHud.UpdateHP();

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
        state = BattleState.PlayerMove;
        StartCoroutine(dialogBox.SetDialog($"Choose an attack:"));


        attack1Button.onClick.AddListener(() => OnAttackButtonPressed(0));
        attack2Button.onClick.AddListener(() => OnAttackButtonPressed(1));
        attack3Button.onClick.AddListener(() => OnAttackButtonPressed(2));
        attack4Button.onClick.AddListener(() => OnAttackButtonPressed(3));

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
