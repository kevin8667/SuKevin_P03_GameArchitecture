using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : ICommand
{

    public GameObject Character
    {
        get;
        set;
    }

    public BattleSceneController Controller
    {
        get;
        set;
    }


    public Magic(GameObject _gameObject, BattleSceneController _battleSceneController)
    {

        Character = _gameObject;
        Controller = _battleSceneController.GetComponent<BattleSceneController>();
    }


    public void Execute()
    {
        Controller.AbilityPanel.SetActive(true);
        Controller.AbilityText.text = Character.name + "'s Magic!";
    }

}
