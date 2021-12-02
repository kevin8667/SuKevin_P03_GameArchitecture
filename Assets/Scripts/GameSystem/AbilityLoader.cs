using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityLoader : MonoBehaviour
{

    public enum AbilityType
    {
        SinglePhysicalAttack,
        AOEPhysicalAttack,
        SingleMagicalAttack,
        AOEMagicalAttack,
        SinglHeal,
        AOEHeal
    }

    [SerializeField] BattleSceneController _controller;
    [SerializeField] string[] _physicalAbilityNames;
    [SerializeField] string[] _magicalAbilityNames;
    [SerializeField] AbilityType[] _physicalAbilityTypes;
    [SerializeField] AbilityType[] _magicalAbilityTypes;
    [SerializeField] float[] _physicalAbilityPower;
    [SerializeField] float[] _MagicalAbilityPower;
    [SerializeField] int[] _physicalAbilityCost;
    [SerializeField] int[] _MagicalAbilityCost;


    public List<PhysicAbility> _physicalAbilityList = new List<PhysicAbility>();
    public List<MagicallAbility> _magicalAbilityList = new List<MagicallAbility>();


    private void Start()
    {
        _controller = GameObject.Find("BattleSceneController").GetComponent<BattleSceneController>();

        if(_physicalAbilityList.Count != 0)
        {
            for (int i = 0; i < _physicalAbilityList.Count; i++)
            {
                //_physicalAbilityList[i] = new PhysicAbility(this.gameObject, _controller);
                _physicalAbilityList[i] = ScriptableObject.CreateInstance<PhysicAbility>();
                _physicalAbilityList[i].Init(gameObject, _controller, _physicalAbilityNames[i], _physicalAbilityTypes[i], _physicalAbilityPower[i], _physicalAbilityCost[i]);
            }
        }
        
        if(_magicalAbilityList.Count != 0)
        {
            for (int i = 0; i < _magicalAbilityList.Count; i++)
            {
                //_magicalAbilityList[i] = new MagicallAbility(this.gameObject, _controller);
                _magicalAbilityList[i] = ScriptableObject.CreateInstance<MagicallAbility>();
                _magicalAbilityList[i].Init(gameObject, _controller, _magicalAbilityNames[i], _magicalAbilityTypes[i], _MagicalAbilityPower[i], _MagicalAbilityCost[i]);
            }
        }
        
    }
}
