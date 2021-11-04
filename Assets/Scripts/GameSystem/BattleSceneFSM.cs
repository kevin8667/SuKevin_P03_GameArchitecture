using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneFSM : StateMachineMB
{
    [SerializeField]
    BattleSceneController _controller;

    [SerializeField]
    SceneFSM _sceneFSM;

    private bool _isLoaded;

    public PlayerTurnState PlayerTurn { get; private set; }

    public EnemyTurnState EnemyTurn { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        _isLoaded = false;
        PlayerTurn = new PlayerTurnState(this, _controller.TurnStateText);
        EnemyTurn = new EnemyTurnState(this, _controller.TurnStateText);
    }

    private void Start()
    {
        
    }

    protected override void Update()
    {
        base.Update();

        if(_isLoaded == false && _sceneFSM.CurrentState == _sceneFSM.PlayState)
        {
            ChangeState(PlayerTurn);
            _isLoaded = true;
        }
    }

    public void EndPlayerTurn()
    {
        if(CurrentState == PlayerTurn)
        {
            ChangeState(EnemyTurn);
        }
        

    }

}
