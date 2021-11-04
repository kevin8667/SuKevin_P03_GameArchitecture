using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseState : State
{
    Text _stateText;
    SceneFSM _stateMachine;
    GameObject _pauseMenu;

    public PauseState(SceneFSM stateMachine, Text stateText, GameObject pauseMenu)
    {
        _stateMachine = stateMachine;
        _stateText = stateText;
        _pauseMenu = pauseMenu;
    }


    public override void Enter()
    {
        base.Enter();

        _stateText.text = "PauseState";

        _pauseMenu.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        _pauseMenu.SetActive(false);

        _stateMachine.ChangeState(_stateMachine.PlayState);

    }

    public override void Update()
    {
        //base.Update();

    }
}
