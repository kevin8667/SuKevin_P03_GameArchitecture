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

    string Name
    {
        get;
        set;
    }

    AbilityLoader.AbilityType AbilityType
    {
        get;
        set;
    }

    float AbilityPower
    {
        get;
        set;
    }

    int AbilityCost
    {
        get;
        set;
    }


    void Execute(List<GameObject> target, int index);


}
