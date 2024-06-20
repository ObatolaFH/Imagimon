using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

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

    void PlayerAction()
    {
        state = BattleState.PlayerMove;
        StartCoroutine(dialogBox.SetDialog($"Test"));

    }

    private void Update()
    {
        if (state == BattleState.PlayerMove)
        {

        }
    }

    
}
