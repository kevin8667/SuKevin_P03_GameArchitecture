using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExecutionState : State
{
    Text _stateText;
    CommandFSM _stateMachine;

    public ExecutionState(CommandFSM stateMachine, Text stateText)
    {
        _stateMachine = stateMachine;
        _stateText = stateText;
    }

    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();

        _stateText.text = "Executing...";

        Debug.Log(_stateMachine.CurrentState);
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
            _stateMachine.ChangeState(_stateMachine.Commanding);
        }
    }
}
