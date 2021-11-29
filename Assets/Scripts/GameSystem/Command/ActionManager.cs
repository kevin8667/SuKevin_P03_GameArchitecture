using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    [SerializeField] CharacterManager _chrManager;
    [SerializeField] BattleSceneController _controller;

    List<GameObject> _enemyList;
    List<GameObject> _playerList;

    List<GameObject> _targetList;

    List<int> _targetIndex;

    AbilityLoader _abilityLoader;

    private List<ICommand> _commands;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        _chrManager = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();
        _enemyList = new List<GameObject>(_chrManager._enemyChrList);
        _playerList = new List<GameObject>(_chrManager._playerChrList);
        _targetIndex = new List<int>();
        _commands = new List<ICommand>();
        _chrManager._currentCharacter = 0;

        Debug.Log(_targetIndex);

        //DetermineCommandSequence();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ExecuteCommandSequence(1));
        }
    }

    private void DetermineCommandSequence()
    {
        _commands = new List<ICommand>();
        
    }

    private IEnumerator ExecuteCommandSequence(float commandDuration)
    {
        for(int i = 0; i < _commands.Count; i++)
        {
            _commands[i].Execute(_enemyList, _targetIndex[i]);
            yield return new WaitForSeconds(commandDuration);
            if (i == _commands.Count - 1)
            {
                _commands.Clear();
                _targetIndex.Clear();
            }
        }
    }

    public void ExecuteCommands()
    {
        _chrManager._currentCharacter = 0;
        StartCoroutine(ExecuteCommandSequence(1));
        
    }

    public void AddPhysicalCommand(string characterType)
    {
        if(characterType == "Player")
        {
            if (_chrManager._currentCharacter < _playerList.Count)
            {
                Debug.Log(_chrManager._currentCharacter);
                _commands.Add(_playerList[_chrManager._currentCharacter].GetComponent<AbilityLoader>()._physicalAbilityList[0]);
                _chrManager._currentCharacter += 1;
            }else if(_chrManager._currentCharacter == _playerList.Count)
            {
                _chrManager._currentCharacter = 0;
                Debug.Log(_chrManager._currentCharacter);
                _commands.Add(_playerList[_chrManager._currentCharacter].GetComponent<AbilityLoader>()._physicalAbilityList[0]);
                
            }
            
        }else if(characterType == "Enemy")
        {
            _commands.Add(_enemyList[0].GetComponent<AbilityLoader>()._physicalAbilityList[0]);
        }
        
    }

    public void AddMagicalCommand(string characterType)
    {
        
        if (characterType == "Player")
        {
            
            if (_chrManager._currentCharacter < _playerList.Count)
            {
                Debug.Log(_chrManager._currentCharacter);
                _commands.Add(_playerList[_chrManager._currentCharacter].GetComponent<AbilityLoader>()._magicalAbilityList[0]);
                _chrManager._currentCharacter += 1;
            }
            else if (_chrManager._currentCharacter == _playerList.Count)
            {
                _chrManager._currentCharacter = 0;
                Debug.Log(_chrManager._currentCharacter);
                _commands.Add(_playerList[_chrManager._currentCharacter].GetComponent<AbilityLoader>()._magicalAbilityList[0]);
            }

        }else if (characterType == "Enemy")
        {
            _commands.Add(_enemyList[0].GetComponent<AbilityLoader>()._magicalAbilityList[0]);
        }
    }

    public void EnemyTageting(int enemyIndex)
    {
        _targetIndex.Add(enemyIndex);
        Debug.Log(_targetIndex);
    }
    

}
