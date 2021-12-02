using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinState : State
{
    Text _stateText;
    ConditionFSM _stateMachine;
    GameObject _endMenu;

    public WinState(ConditionFSM stateMachine, Text stateText, GameObject endMenu)
    {
        _stateMachine = stateMachine;
        _stateText = stateText;
        _endMenu = endMenu;
    }

    // Start is called before the first frame update
    public override void Enter()
    {
        base.Enter();
        
        _endMenu.SetActive(true);

        _stateText.text = "You Win!";

        Time.timeScale = 0;


    }

    public override void Exit()
    {
        base.Exit();

        _endMenu.SetActive(false);

        Time.timeScale = 1;
    }

    public override void Update()
    {
        base.Update();

    }



}
