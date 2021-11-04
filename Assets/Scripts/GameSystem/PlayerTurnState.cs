using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnState : State
{

    Text _stateText;
    BattleSceneFSM _stateMachine;

    public PlayerTurnState(BattleSceneFSM stateMachine, Text stateText)
    {
        _stateMachine = stateMachine;
        _stateText = stateText;
    }
    public override void Enter()
    {

        _stateText.text = "Player's Turn";
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        base.Update();
    }

}
