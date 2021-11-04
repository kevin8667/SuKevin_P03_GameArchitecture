using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionFSM : StateMachineMB
{
    [SerializeField]
    BattleSceneController _controller;

    public WinState Win{ get; private set; }

    public LoseState Lose { get; private set; }

    private void Awake()
    {
        Win = new WinState(this, _controller.WinLoseStateText, _controller.EndMenu);
        Lose = new LoseState(this, _controller.WinLoseStateText, _controller.EndMenu);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void WinTheGame()
    {
        ChangeState(Win);
    }

    public void LoseTheGame()
    {
        ChangeState(Lose);
    }

}
