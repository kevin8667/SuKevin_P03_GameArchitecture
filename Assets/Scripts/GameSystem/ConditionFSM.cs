using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionFSM : StateMachineMB
{
    [SerializeField]
    BattleSceneController _controller;

    public WinState Win{ get; private set; }

    public LoseState Lose { get; private set; }

    public FightingState Fighting { get; private set; }

    private void Awake()
    {
        Win = new WinState(this, _controller.WinLoseStateText, _controller.EndMenu);
        Lose = new LoseState(this, _controller.WinLoseStateText, _controller.EndMenu);
        Fighting = new FightingState(this);

    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(Fighting);
    }

    public void WinTheGame()
    {
        ChangeState(Win);
    }

    public void LoseTheGame()
    {
        ChangeState(Lose);
    }

    public void PlayTheGame()
    {
        ChangeState(Fighting);
    }

}
