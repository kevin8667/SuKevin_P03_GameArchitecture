using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFSM : StateMachineMB
{
    [SerializeField]
    BattleSceneController _controller;

    public SetUpState SetUp { get; private set; }
    public PlayState PlayState { get; private set; }

    public PauseState PauseState { get; private set; }

    private void Awake()
    {
        SetUp = new SetUpState(this, _controller.SceneStateText);
        PlayState = new PlayState(this, _controller.SceneStateText);
        PauseState = new PauseState(this, _controller.SceneStateText, _controller.PauseMenu);
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(SetUp);
    }

    public void PauseGame()
    {
        ChangeState(PauseState);
    }

    public void ResumeGame()
    {
        ChangeState(PlayState);
    }

}
