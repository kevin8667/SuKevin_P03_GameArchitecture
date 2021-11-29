using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleCalculationManager : MonoBehaviour
{

    int _attackerATK, _attackerSTR , _attackerINT, _targetDEF, _targetVIT,  _targetWIS;
    int _damageAmount;

    private void OnEnable()
    {
        MagicallAbility.MagicalAbilityCalculation += MagicCalculation;
        MagicallAbility.AOEMagicalAbilityCalculation += AOEMagicCalculation;
        PhysicAbility.PhysicalAbilityCalculation += PhysicalCalculation;
        PhysicAbility.AOEPhysicalAbilityCalculation += AOEPhysicalCalculation;

    }

    private void OnDisable()
    {
        MagicallAbility.MagicalAbilityCalculation -= MagicCalculation;
        MagicallAbility.AOEMagicalAbilityCalculation -= AOEMagicCalculation;
        PhysicAbility.PhysicalAbilityCalculation -= PhysicalCalculation;
        PhysicAbility.AOEPhysicalAbilityCalculation -= AOEPhysicalCalculation;
    }


    private void MagicCalculation(GameObject attacker, GameObject target)
    {
        _attackerATK = attacker.GetComponent<Attr>().ATK;
        _targetDEF = target.GetComponent<Attr>().DEF;
        _attackerINT = attacker.GetComponent<Attr>().INT;
        _targetWIS = target.GetComponent<Attr>().WIS;
        target.GetComponent<Attr>().HP -= DamageCalculation(_attackerATK, _targetDEF, _attackerINT, _targetWIS);
        Debug.Log(target.GetComponent<Attr>().HP);
    }

    private void AOEMagicCalculation(GameObject attacker , List<GameObject> targetList)
    {
        _attackerATK = attacker.GetComponent<Attr>().ATK;
        _attackerINT = attacker.GetComponent<Attr>().INT;
        foreach (GameObject target in targetList)
        {
            _targetDEF = target.GetComponent<Attr>().DEF;
            _targetWIS = target.GetComponent<Attr>().WIS;
            target.GetComponent<Attr>().HP -= DamageCalculation(_attackerATK, _targetDEF, _attackerINT, _targetWIS);
            Debug.Log(target.GetComponent<Attr>().HP);
        }
        
    }

    private void PhysicalCalculation(GameObject attacker, GameObject target)
    {
        _attackerATK = attacker.GetComponent<Attr>().ATK;
        _attackerSTR = attacker.GetComponent<Attr>().STR;
        _targetDEF = target.GetComponent<Attr>().DEF;
        _targetVIT = target.GetComponent<Attr>().VIT;
        target.GetComponent<Attr>().HP -= DamageCalculation(_attackerATK, _targetDEF, _attackerSTR, _targetVIT);
        Debug.Log(target.GetComponent<Attr>().HP);

    }

    private void AOEPhysicalCalculation( GameObject attacker, List<GameObject> targetList)
    {
        _attackerATK = attacker.GetComponent<Attr>().ATK;
        _attackerSTR = attacker.GetComponent<Attr>().STR;
        foreach (GameObject target in targetList)
        {
            _targetDEF = target.GetComponent<Attr>().DEF;
            _targetVIT = target.GetComponent<Attr>().VIT;
            target.GetComponent<Attr>().HP -= DamageCalculation(_attackerATK, _targetDEF, _attackerSTR, _targetVIT);
            Debug.Log(target.GetComponent<Attr>().HP);
        }
    }


    public int DamageCalculation(int attackerATK ,int targetDEF, int attackerAttr, int targetAttr)
    {
        return (int)Math.Round((attackerATK * attackerAttr * 0.3) - (targetDEF + (targetAttr * 0.2)));
    }

}
