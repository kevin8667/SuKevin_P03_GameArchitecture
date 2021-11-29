using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandFSM : StateMachineMB
{
    [SerializeField]
    BattleSceneController _controller;

    [SerializeField] GameObject[] _commandButtons;

    public CommandingState Commanding{ get; private set; }

    public ExecutionState Executing{ get; private set; }

    private void Awake()
    {
        Commanding = new CommandingState(this, _controller.CommandStateText, _commandButtons);
        Executing = new ExecutionState(this, _controller.CommandStateText);
    }

    void Start()
    {
        ChangeState(Commanding);
    }

    protected override void Update()
    {
        base.Update();

    }

    public void Execute()
    {
        if (CurrentState == Commanding)
        {
            ChangeState(Executing);

            
        }
        
    }
}
