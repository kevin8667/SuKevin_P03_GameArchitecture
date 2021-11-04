using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUpState : State
{
    Text _stateText;
    SceneFSM _stateMachine;
    public SetUpState(SceneFSM stateMachine, Text stateText)
    {
        _stateMachine = stateMachine;
        _stateText = stateText;
    }


    public override void Enter()
    {
        base.Enter();

        _stateText.text = "Set Up State...";
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (StateDuration >= 1.5f)
        {
            _stateMachine.ChangeState(_stateMachine.PlayState);
        }
    }

}
