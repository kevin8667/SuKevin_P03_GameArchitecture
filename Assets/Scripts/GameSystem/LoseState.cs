using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseState : State
{
    Text _stateText;
    ConditionFSM _stateMachine;
    GameObject _endMenu;

    public LoseState(ConditionFSM stateMachine, Text stateText, GameObject endMenu)
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
        
        _stateText.text = "You Lose!";

        
    }

    public override void Exit()
    {
        base.Exit();

        _endMenu.SetActive(false);


    }

    public override void Update()
    {
        base.Update();

    }

}
