using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ICommand
{
    BattleSceneController Controller
    {
        get;
        set;
    }

    GameObject Character
    {
        get;
        set;
    }

    void Execute();


}
