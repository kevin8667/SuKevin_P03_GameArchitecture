using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandingState : State
{
    Text _stateText;
    CommandFSM _stateMachine;
    GameObject[] _commandButtons;


    public CommandingState(CommandFSM stateMachine, Text stateText, GameObject[] commandButtons)
    {
        _stateMachine = stateMachine;
        _stateText = stateText;
        _commandButtons = commandButtons;
    }

    // Start is called before the first frame update
    public override void Enter()
    {

        _stateText.text = "Commanding...";

        foreach (GameObject button in _commandButtons)
        {
            button.SetActive(true);
        }

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
