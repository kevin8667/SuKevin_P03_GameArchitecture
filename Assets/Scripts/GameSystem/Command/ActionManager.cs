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

    public List<int> _targetIndex;

    AbilityLoader _abilityLoader;

    public List<ICommand> _commands;

    CursorManager _cursorManager;

    CommandFSM _commandFSM;

    private void Awake()
    {
        _targetIndex = new List<int>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _chrManager = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();
        _cursorManager = GameObject.Find("CursorManager").GetComponent<CursorManager>();
        _commandFSM = GameObject.Find("CommandFSM").GetComponent<CommandFSM>();

        _enemyList = new List<GameObject>(_chrManager._enemyChrList);
        _playerList = new List<GameObject>(_chrManager._playerChrList);

        _commands = new List<ICommand>();

        _targetIndex = new List<int> { 0, 0, 0, 0, 0 };

        _chrManager._currentCharacter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            foreach (int i in _targetIndex)
            {
                Debug.Log(_targetIndex[i]);
            }
        }
      
    }

    private void DetermineCommandSequence()
    {
        for (int i = 0; i < _commands.Count; i++)
        {
            _commands.Sort(delegate (ICommand a, ICommand b)
            {
                return (b.Character.GetComponent<Attr>().SPD).CompareTo(a.Character.GetComponent<Attr>().SPD);
            });

        }

    }

    private IEnumerator ExecuteCommandSequence(float commandDuration)
    {
        Debug.Log(_commands.Count);
        DetermineCommandSequence();
        for (int i = 0; i < _commands.Count; i++)
        {
            if(_commands[i].Character.GetComponent<IPlayer>() != null)
            {
                _commands[i].Execute(_enemyList, _targetIndex[i]);

                
            }else if(_commands[i].Character.GetComponent<IEnemy>() != null)
            {

                _commands[i].Execute(_playerList, _targetIndex[i]);
            }
            
            yield return new WaitForSeconds(commandDuration);

            if (i == _commands.Count -1 )
            {
                _commands.Clear();

                _targetIndex = new List<int> { 0, 0, 0, 0, 0 };

                _commandFSM.ChangeState(_commandFSM.Commanding);
            }
        }
    }

    public void ExecuteCommands()
    {
        _chrManager._currentCharacter = 0;
        StartCoroutine(ExecuteCommandSequence(1));
        
    }

    public void AddPhysicalCommand(int abilityIndex)
    {
        _playerList = new List<GameObject>(_chrManager._playerChrList);

        if (_chrManager._currentCharacter < _playerList.Count)
        {
            _commands.Add(_playerList[_chrManager._currentCharacter].GetComponent<AbilityLoader>()._physicalAbilityList[abilityIndex]);

            _chrManager._currentCharacter += 1;
        }

        
    }

    public void AddPhysicalCommand(int index, int abilityIndex)
    {
        _enemyList = new List<GameObject>(_chrManager._enemyChrList);
        _commands.Add(_enemyList[index].GetComponent<AbilityLoader>()._physicalAbilityList[0]);

    }

    public void AddMagicalCommand(int abilityIndex)
    {
        _playerList = new List<GameObject>(_chrManager._playerChrList);

        if (_chrManager._currentCharacter < _playerList.Count)
        {
            _commands.Add(_playerList[_chrManager._currentCharacter].GetComponent<AbilityLoader>()._magicalAbilityList[abilityIndex]);

            _chrManager._currentCharacter += 1;
        }

    }

    public void AddMagicalCommand(int index, int abilityIndex)
    {
        _enemyList = new List<GameObject>(_chrManager._enemyChrList);
        _commands.Add(_enemyList[index].GetComponent<AbilityLoader>()._magicalAbilityList[abilityIndex]);
    }

    public void EnemyTageting(int enemyIndex, GameObject attacker)
    {
       for(int i =0; i < _chrManager._characterList.Count; i++)
        {
            if(attacker.name == _chrManager._characterList[i].name)
            {
                _targetIndex[i] = enemyIndex;
            }
        }
        

    }

    public void CancelCommand(int index)
    {
        _commands.Remove(_commands[index]);
        if(_chrManager._currentCharacter != 0)
        {
            _chrManager._currentCharacter -= 1;
        }
        
    }

    public void ResetTarget()
    {
        _targetIndex.Clear();
    }

}
