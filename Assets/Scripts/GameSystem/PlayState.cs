using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayState : State
{
    Text _stateText;
    SceneFSM _stateMachine;
    public PlayState(SceneFSM stateMachine, Text stateText)
    {
        _stateMachine = stateMachine;
        _stateText = stateText;
    }


    public override void Enter()
    { 
        _stateText.text = "PlayState";
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        //base.Update();
    }
}
