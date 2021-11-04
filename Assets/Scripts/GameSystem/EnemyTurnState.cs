using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTurnState : State
{
    Text _stateText;
    BattleSceneFSM _stateMachine;

     public EnemyTurnState(BattleSceneFSM stateMachine, Text stateText)
    {
        _stateMachine = stateMachine;
        _stateText = stateText;
    }

    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();

        _stateText.text = "Enemy's Turn";
    }

    public override void Update()
    {
        base.Update();
        if (StateDuration >= 1.5f)
        {
            _stateMachine.ChangeState(_stateMachine.PlayerTurn);
        }
    }
}
