using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenuAttribute(fileName = "New Magical Data", menuName = "Ability Data/Magical")]
public class MagicallAbility : MagicData , IMagic
{
    public static event Action<GameObject, GameObject, float> MagicalAbilityCalculation;
    public static event Action<GameObject, List<GameObject>, float> AOEMagicalAbilityCalculation;

    public BattleSceneController Controller
    {
        get;
        set;
    }

    public GameObject Character
    {
        get;
        set;
    }

    public string Name
    {
        get;
        set;
    }

    public AbilityLoader.AbilityType AbilityType
    {
        get;
        set;
    }

    public float AbilityPower
    {
        get;
        set;
    }

    public int AbilityCost
    {
        get;
        set;
    }

    public void Init(GameObject _gameObject, BattleSceneController _battleSceneController, string _abilityName, AbilityLoader.AbilityType _abilityType, float _power, int _cost)
    {

        Character = _gameObject;
        Controller = _battleSceneController.GetComponent<BattleSceneController>();
        Name = _abilityName;
        AbilityType = _abilityType;
        AbilityPower = _power;
        AbilityCost = _cost;
    }

    public void Execute(List<GameObject> target, int index)
    {
        Controller.AbilityPanel.SetActive(true);
        
        Controller.AbilityText.text = Character.name + "'s " + Name + "!";

        Character.GetComponent<controller>().Attack();

        if (AbilityType == AbilityLoader.AbilityType.SingleMagicalAttack)
        {
            MagicalAbilityCalculation?.Invoke(Character, target[index], AbilityPower);

            Character.GetComponent<Attr>().MP -= AbilityCost;

        }
        else if(AbilityType == AbilityLoader.AbilityType.AOEMagicalAttack)
        {
            AOEMagicalAbilityCalculation?.Invoke(Character,target, AbilityPower);

            Character.GetComponent<Attr>().MP -= AbilityCost;
        }
        

    }

}
