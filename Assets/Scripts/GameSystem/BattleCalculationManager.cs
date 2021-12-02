using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class BattleCalculationManager : MonoBehaviour
{

    int _attackerATK, _attackerSTR , _attackerINT, _targetDEF, _targetVIT,  _targetWIS;

    [SerializeField] GameObject _damageEffect;
    [SerializeField] GameObject _damgageText;

    GameObject _newParticles, _newDamageText;

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


    private void MagicCalculation(GameObject attacker, GameObject target, float power)
    {
        _attackerATK = attacker.GetComponent<Attr>().ATK;
        _targetDEF = target.GetComponent<Attr>().DEF;
        _attackerINT = attacker.GetComponent<Attr>().INT;
        _targetWIS = target.GetComponent<Attr>().WIS;

        _damageAmount = DamageCalculation(power, _attackerATK, _targetDEF, _attackerINT, _targetWIS);
        target.GetComponent<Attr>().HP -= _damageAmount;

        _newParticles = Instantiate(_damageEffect, target.transform.position, Quaternion.identity);
        if (_newParticles)
        {
            Destroy(_newParticles, 1f);
        }

        _newDamageText = Instantiate(_damgageText, target.transform.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f)), Quaternion.identity);
        if (_newDamageText)
        {
            _newDamageText.GetComponent<TextMeshPro>().text = _damageAmount.ToString();
            Destroy(_newDamageText, 0.6f);
        }

        Debug.Log(attacker.name + "attacked " + target.name);
        Debug.Log(target.name + "'s HP: " + target.GetComponent<Attr>().HP);
    }

    private void AOEMagicCalculation(GameObject attacker , List<GameObject> targetList, float power)
    {
        _attackerATK = attacker.GetComponent<Attr>().ATK;
        _attackerINT = attacker.GetComponent<Attr>().INT;

        foreach (GameObject target in targetList)
        {
            _targetDEF = target.GetComponent<Attr>().DEF;
            _targetWIS = target.GetComponent<Attr>().WIS;


            _damageAmount = DamageCalculation(power, _attackerATK, _targetDEF, _attackerINT, _targetWIS);
            target.GetComponent<Attr>().HP -= _damageAmount;

            _newParticles = Instantiate(_damageEffect, target.transform.position, Quaternion.identity);
            if (_newParticles)
            {
                
                Destroy(_newParticles, 1f);
            }

            _newDamageText = Instantiate(_damgageText, target.transform.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f)), Quaternion.identity);
            if (_newDamageText)
            {
                _newDamageText.GetComponent<TextMeshPro>().text = _damageAmount.ToString();
                Destroy(_newDamageText, 0.6f);
            }

            Debug.Log(attacker.name + "attacked " + target.name);
            Debug.Log(target.name + "'s HP: " + target.GetComponent<Attr>().HP);
        }
        
    }

    private void PhysicalCalculation(GameObject attacker, GameObject target, float power)
    {
        _attackerATK = attacker.GetComponent<Attr>().ATK;
        _attackerSTR = attacker.GetComponent<Attr>().STR;

        _targetDEF = target.GetComponent<Attr>().DEF;
        _targetVIT = target.GetComponent<Attr>().VIT;

        _damageAmount = DamageCalculation(power, _attackerATK, _targetDEF, _attackerSTR, _targetVIT);
        target.GetComponent<Attr>().HP -= _damageAmount;

        _newParticles = Instantiate(_damageEffect, target.transform.position, Quaternion.identity);
        if (_newParticles)
        {
            Destroy(_newParticles, 1f);
        }

        _newDamageText = Instantiate(_damgageText, target.transform.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f)), Quaternion.identity);
        if (_newDamageText)
        {
            _newDamageText.GetComponent<TextMeshPro>().text = _damageAmount.ToString();
            Destroy(_newDamageText, 0.6f);
        }

        Debug.Log(attacker.name + "attacked " + target.name);
        Debug.Log(target.name + "'s HP: " + target.GetComponent<Attr>().HP);

    }

    private void AOEPhysicalCalculation(GameObject attacker, List<GameObject> targetList, float power)
    {
        _attackerATK = attacker.GetComponent<Attr>().ATK;
        _attackerSTR = attacker.GetComponent<Attr>().STR;

        foreach (GameObject target in targetList)
        {
            _targetDEF = target.GetComponent<Attr>().DEF;
            _targetVIT = target.GetComponent<Attr>().VIT;

            _damageAmount = DamageCalculation(power, _attackerATK, _targetDEF, _attackerSTR, _targetVIT);
            target.GetComponent<Attr>().HP -= _damageAmount;

            _newParticles = Instantiate(_damageEffect, target.transform.position, Quaternion.identity);
            if (_newParticles)
            {
                Destroy(_newParticles, 1f);
            }

            _newDamageText = Instantiate(_damgageText, target.transform.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f)), Quaternion.identity);
            if (_newDamageText)
            {
                _newDamageText.GetComponent<TextMeshPro>().text = _damageAmount.ToString();
                Destroy(_newDamageText, 0.6f);
            }

            Debug.Log(attacker.name + "attacked " + target.name);
            Debug.Log(target.name + "'s HP: " + target.GetComponent<Attr>().HP);
        }
    }


    public int DamageCalculation(float power,int attackerATK ,int targetDEF, int attackerAttr, int targetAttr)
    {
        return (int)Math.Round(((attackerATK + (attackerAttr * 0.3)) * power - (targetDEF + targetAttr * 0.2)) * UnityEngine.Random.Range(0.3f, 0.7f));
    }

}
