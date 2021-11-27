using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandingState : State
{
    Text _stateText;
    CommandFSM _stateMachine;

    public CommandingState(CommandFSM stateMachine, Text stateText)
    {
        _stateMachine = stateMachine;
        _stateText = stateText;
    }

    // Start is called before the first frame update
    public override void Enter()
    {

        _stateText.text = "Commanding...";
        Debug.Log(_stateMachine.CurrentState);

    }

    public override void Exit()
    {
        base.Exit();



    }

    public override void Update()
    {
        base.Update();

    }
}
