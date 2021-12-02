using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightingState : State
{
    Text _stateText;
    ConditionFSM _stateMachine;
    public FightingState(ConditionFSM stateMachine)
    {
        _stateMachine = stateMachine;

    }


    public override void Enter()
    {
       
        Time.timeScale = 1;
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
