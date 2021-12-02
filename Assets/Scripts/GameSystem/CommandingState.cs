using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandingState : State
{
    Text _stateText;

    CommandFSM _stateMachine;

    GameObject[] _commandButtons;

    EnemyManager _enemyManager;

    BattleSceneController _battleSceneController;

    public CommandingState(CommandFSM stateMachine, Text stateText, GameObject[] commandButtons, EnemyManager enemyManager, BattleSceneController battleSceneController)
    {
        _stateMachine = stateMachine;
        _stateText = stateText;
        _commandButtons = commandButtons;
        _enemyManager = enemyManager;
        _battleSceneController = battleSceneController;
    }

    // Start is called before the first frame update
    public override void Enter()
    {

        _stateText.text = "Commanding...";

        _battleSceneController.AbilityPanel.SetActive(false);

        //enemy makes desicion when enters the commanding state
        _enemyManager.DecisionMaker();

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
