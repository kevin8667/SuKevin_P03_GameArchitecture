using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    ActionManager _actionManager;

    CharacterManager _characterManager;

    List<GameObject> _playChrList;

    private void Awake()
    {
        _actionManager = GameObject.Find("ActionManager").GetComponent<ActionManager>();
        _characterManager = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();

        _playChrList = _characterManager._playerChrList;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecisionMaker()
    {
        _playChrList = new List<GameObject>(_characterManager._playerChrList);

        for (int i = 0; i < _characterManager._enemyChrList.Count; i++)
        {
            if (_characterManager._enemyChrList[i].GetComponent<AbilityLoader>()._physicalAbilityList.Count != 0)
            {
                _actionManager.AddPhysicalCommand(i, Random.Range(0, _characterManager._enemyChrList[i].GetComponent<AbilityLoader>()._physicalAbilityList.Count));

                _actionManager.EnemyTageting(Random.Range(0, _playChrList.Count), _characterManager._enemyChrList[i]);

            }
            else if (_characterManager._enemyChrList[i].GetComponent<AbilityLoader>()._magicalAbilityList.Count != 0)
            {
                _actionManager.AddMagicalCommand(i, Random.Range(0, _characterManager._enemyChrList[i].GetComponent<AbilityLoader>()._magicalAbilityList.Count));

                _actionManager.EnemyTageting(Random.Range(0, _playChrList.Count), _characterManager._enemyChrList[i]);

            }
        }
       
    }
}
