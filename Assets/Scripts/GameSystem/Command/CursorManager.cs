using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public enum CursorState
    {
        NonActive,
        Basic,
        Ability,
        EnemyTargeting
        
    }

    public CursorState _currentCursorState;

    [SerializeField] GameObject _basicCursor, _abilityCursor, _targetingCursor;

    [SerializeField] GameObject _characterCursor;

    [SerializeField] AudioClip _cursorSound;

    [SerializeField] AudioClip _cancelSound;

    [SerializeField] AudioClip _decideSound;

    List<GameObject> _menuItems;

    private CharacterManager _characterManager;

    MenuManager _menuManager;

    ActionManager _actionManager;

    CommandFSM _commandFSM;

    [SerializeField] GameObject _namePanel;

    [SerializeField] Text _targetName;

    [SerializeField] Text[] _abilityNames;

    ConditionFSM _conditionFSM;

    SceneFSM _sceneFSM;

    bool _isMenuLoaded, _isBasicAttack;

    int _menuIndex, _currentCommandNumber;

    // Start is called before the first frame update
    void Start()
    {
        _basicCursor.SetActive(false);
        _abilityCursor.SetActive(false);
        _targetingCursor.SetActive(false);
        _characterCursor.SetActive(false);

        _namePanel.SetActive(false);
        

        _currentCursorState = CursorState.Basic;

        _menuItems = new List<GameObject>();

        _menuIndex = 0;
        _currentCommandNumber = 0;

        _isMenuLoaded = false;
        _isBasicAttack = false;


        _characterManager = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();
        _menuManager = GameObject.Find("MenuManager").GetComponent<MenuManager>();
        _actionManager = GameObject.Find("ActionManager").GetComponent<ActionManager>();

        _commandFSM = GameObject.Find("CommandFSM").GetComponent<CommandFSM>();
        _conditionFSM = GameObject.Find("ConditionFSM").GetComponent<ConditionFSM>();
        _sceneFSM = GameObject.Find("SceneFSM").GetComponent<SceneFSM>();

        if (_commandFSM.CurrentState == _commandFSM.Commanding)
        {
            _characterCursor.SetActive(true);
            _characterCursor.transform.position = _characterManager._playerChrList[_characterManager._currentCharacter].transform.position + new Vector3(0, 1f, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (_conditionFSM.CurrentState == _conditionFSM.Fighting && _sceneFSM.CurrentState != _sceneFSM.PauseState)
        {
            if (_commandFSM.CurrentState == _commandFSM.Commanding && _currentCursorState == CursorState.Basic)
            {
                if (_isMenuLoaded == false)
                {
                    ResetMenu(_basicCursor);
                    _isMenuLoaded = true;
                }

                _characterCursor.SetActive(true);
                _characterCursor.transform.position = _characterManager._playerChrList[_characterManager._currentCharacter].transform.position + new Vector3(0, 1f, 0);

                _basicCursor.SetActive(true);
                _targetingCursor.SetActive(false);
                _abilityCursor.SetActive(false);

                MovingCursor(_basicCursor);
                Decide();
                Cancel();
            }
            else if (_commandFSM.CurrentState == _commandFSM.Commanding && _currentCursorState == CursorState.Ability)
            {
                if (_isMenuLoaded == false)
                {
                    ResetMenu(_abilityCursor);
                    _isMenuLoaded = true;

                }

                _basicCursor.SetActive(false);
                _abilityCursor.SetActive(true);
                _targetingCursor.SetActive(false);

                MovingCursor(_abilityCursor);
                Decide();
                Cancel();
            }
            else if (_commandFSM.CurrentState == _commandFSM.Commanding && _currentCursorState == CursorState.EnemyTargeting)
            {
                if (_isMenuLoaded == false)
                {
                    ResetMenu(_targetingCursor);
                    _isMenuLoaded = true;
                }

                _basicCursor.SetActive(false);
                _abilityCursor.SetActive(false);
                _targetingCursor.SetActive(true);

                MovingCursor(_targetingCursor);
                Decide();
                Cancel();
            }
        }
       
    }


    //Reload manu everytime the menu changes
    public void ResetMenu(GameObject cursor)
    {
        _menuItems.Clear();
        if (_currentCursorState == CursorState.Basic || _currentCursorState == CursorState.EnemyTargeting && _isMenuLoaded == false)
        {
            
            if(_currentCursorState == CursorState.EnemyTargeting)
            {
                foreach (GameObject item in _characterManager._enemyChrList)
                {
                    _menuIndex = 0;
                    _menuItems.Add(item);
                    _targetName.text = _menuItems[_menuIndex].name;
                    cursor.transform.position = new Vector3(cursor.transform.position.x, _menuItems[_menuIndex].transform.position.y, cursor.transform.position.z);

                }
            }else if (_currentCursorState == CursorState.Basic)
            {
                foreach (GameObject item in GameObject.FindGameObjectsWithTag("BasicCommand"))
                {
                    _menuIndex = 0;
                    _menuItems.Add(item);
                    cursor.transform.position = new Vector3(cursor.transform.position.x, _menuItems[_menuIndex].transform.position.y, cursor.transform.position.z);
                }
            }
        }else if(_currentCursorState == CursorState.Ability && _isMenuLoaded == false)
        {
            foreach (GameObject item in GameObject.FindGameObjectsWithTag("AbilityCommand"))
            {
                _menuIndex = 0;
                _menuItems.Add(item);
                for(int i = 0; i < _menuItems.Count; i++)
                {
                    
                    if (_characterManager._playerChrList[_characterManager._currentCharacter].GetComponent<AbilityLoader>()._physicalAbilityList.Count != 0)
                    {
                        
                        _abilityNames[i].GetComponent<Text>().text = _characterManager._playerChrList[_characterManager._currentCharacter].GetComponent <AbilityLoader>()._physicalAbilityList[i+1].Name;


                    }else if (_characterManager._playerChrList[_characterManager._currentCharacter].GetComponent<AbilityLoader>()._magicalAbilityList.Count != 0)
                    {
                        _abilityNames[i].GetComponent<Text>().text = _characterManager._playerChrList[_characterManager._currentCharacter].GetComponent<AbilityLoader>()._magicalAbilityList[i+1].Name;
                    }
                    
                }
                cursor.transform.position = new Vector3(cursor.transform.position.x, _menuItems[_menuIndex].transform.position.y, cursor.transform.position.z);
            }
        }

    }

 
    //moving cursor
    private void MovingCursor(GameObject cursor)
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            
            if(_cursorSound != null)
            {
                AudioHelper.PlayClip2D(_cursorSound, 1f);
            }
            if (_menuIndex <= _menuItems.Count - 1 && _menuIndex != 0)
            {
                _menuIndex -= 1;

                cursor.transform.position = new Vector3(cursor.transform.position.x, _menuItems[_menuIndex].transform.position.y, cursor.transform.position.z);

                _targetName.text = _menuItems[_menuIndex].name;
            }
            else if (_menuIndex == 0)
            {
                _menuIndex = _menuItems.Count - 1;

                cursor.transform.position = new Vector3(cursor.transform.position.x, _menuItems[_menuIndex].transform.position.y, cursor.transform.position.z);

                _targetName.text = _menuItems[_menuIndex].name;
            }

            Debug.Log(_menuItems[_menuIndex].name);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_cursorSound != null)
            {
                AudioHelper.PlayClip2D(_cursorSound, 1f);
            }
            if (_menuIndex < _menuItems.Count - 1)
            {
                _menuIndex += 1;

                cursor.transform.position = new Vector3(cursor.transform.position.x, _menuItems[_menuIndex].transform.position.y, cursor.transform.position.z);

                _targetName.text = _menuItems[_menuIndex].name;
            }
            else if (_menuIndex == _menuItems.Count - 1)
            {
                _menuIndex = 0;

                cursor.transform.position = new Vector3(cursor.transform.position.x, _menuItems[_menuIndex].transform.position.y, cursor.transform.position.z);

                _targetName.text = _menuItems[_menuIndex].name;
            }

            Debug.Log(_menuItems[_menuIndex].name);
        }
    }


    //make decision and pass it out
    private void Decide()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (_currentCursorState == CursorState.Basic || _currentCursorState == CursorState.Ability || _currentCursorState == CursorState.EnemyTargeting && _isMenuLoaded == true)
            {
                if (_decideSound != null)
                {
                    AudioHelper.PlayClip2D(_decideSound, 1f);
                }
                if (_currentCursorState == CursorState.Basic)
                {
                    if(_menuItems[_menuIndex].name == "Attack")
                    {
                        _currentCursorState = CursorState.EnemyTargeting;

                        if(_characterManager._playerChrList[_characterManager._currentCharacter].GetComponent<AbilityLoader>()._magicalAbilityList.Count == 0)
                        {
                            _actionManager.AddPhysicalCommand(0);

                        }else if(_characterManager._playerChrList[_characterManager._currentCharacter].GetComponent<AbilityLoader>()._physicalAbilityList.Count == 0)
                        {
                            _actionManager.AddMagicalCommand(0);

                        }
                        
                        _currentCommandNumber += 1;

                        _isMenuLoaded = false;

                        _isBasicAttack = true;

                        _namePanel.SetActive(true);
                    }
                    else if(_menuItems[_menuIndex].name == "Ability")
                    {
                        _currentCursorState = CursorState.Ability;

                        _menuManager.EnableAbilityMenu();

                        _isMenuLoaded = false;

                        Debug.Log(_currentCursorState);
                    }

                }
                else if (_currentCursorState == CursorState.Ability)
                {
                    _currentCursorState = CursorState.EnemyTargeting;

                    if(_characterManager._playerChrList[_characterManager._currentCharacter].GetComponent<AbilityLoader>()._magicalAbilityList.Count == 0)
                    {
                        if (_menuIndex + 1 != _menuItems.Count)
                        {
                            _actionManager.AddPhysicalCommand(_menuIndex + 1);

                            Debug.Log(_menuIndex);

                        }
                        else if (_menuIndex  == _menuItems.Count - 1)
                        {
                            _actionManager.AddPhysicalCommand(_menuIndex +1 );

                            Debug.Log(_menuIndex);

                        }
                    }else if(_characterManager._playerChrList[_characterManager._currentCharacter].GetComponent<AbilityLoader>()._physicalAbilityList.Count == 0)
                    {
                        if (_menuIndex + 1 != _menuItems.Count)
                        {
                            _actionManager.AddMagicalCommand(_menuIndex + 1 );

                            Debug.Log(_menuIndex);

                        }
                        else if (_menuIndex == _menuItems.Count - 1)
                        {
                            _actionManager.AddMagicalCommand(_menuIndex + 1 );

                            Debug.Log(_menuIndex);
                        }
                    }
                    
                    _menuManager.DisableAbilityMenu();

                    _currentCommandNumber += 1;

                    _namePanel.SetActive(true);

                    _isMenuLoaded = false;

                }
                else if(_currentCursorState == CursorState.EnemyTargeting)
                {
                    _currentCursorState = CursorState.Basic;
                    
                    _actionManager.EnemyTageting(_menuIndex, _characterManager._playerChrList[_characterManager._currentCharacter-1]);


                    if (_characterManager._currentCharacter < _characterManager._playerChrList.Count)
                    {

                        _characterCursor.transform.position = _characterManager._playerChrList[_characterManager._currentCharacter].transform.position + new Vector3(0, 1f, 0);


                    }else if(_characterManager._currentCharacter == _characterManager._playerChrList.Count)
                    {
                        _characterCursor.SetActive(false);
                    }
                    

                    _namePanel.SetActive(false);
                    
                    Debug.Log(_menuIndex);
                    
                    if (_characterManager._currentCharacter == _characterManager._playerChrList.Count)
                    {
                        _currentCommandNumber = 0;

                        _namePanel.SetActive(false);

                        _basicCursor.SetActive(false);

                        _targetingCursor.SetActive(false);

                        _abilityCursor.SetActive(false);

                        _characterCursor.SetActive(false);

                        _commandFSM.Execute();

                        _actionManager.ExecuteCommands();

                    }

                    _isMenuLoaded = false;
                }

            }
        }

    }

    //cacel the current selection
    private void Cancel()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            
            if (_currentCursorState == CursorState.EnemyTargeting|| _currentCursorState == CursorState.Ability && _isMenuLoaded == true)
            {
                if (_cancelSound != null)
                {
                    AudioHelper.PlayClip2D(_cancelSound, 1f);
                }
                if (_currentCursorState == CursorState.EnemyTargeting && _isBasicAttack)
                {
                    _namePanel.SetActive(false);
                    _currentCursorState = CursorState.Basic;
                    if(_currentCommandNumber != 0)
                    {
                        _actionManager.CancelCommand((_characterManager._enemyChrList.Count - 1 + _currentCommandNumber));
                        _currentCommandNumber -= 1;
                        
                    }

                    _isBasicAttack = false;
                    _isMenuLoaded = false;

                }else if (_currentCursorState == CursorState.EnemyTargeting && _isBasicAttack == false && _isMenuLoaded == true)
                {
                    _namePanel.SetActive(false);
                    _currentCursorState = CursorState.Ability;
                    _menuManager.EnableAbilityMenu();
                    if (_currentCommandNumber != 0)
                    {
                        _actionManager.CancelCommand((_characterManager._enemyChrList.Count -1 + _currentCommandNumber));
                        _currentCommandNumber -= 1;

                    }

                    _isMenuLoaded = false;
                }
                else if (_currentCursorState == CursorState.Ability && _isMenuLoaded == true)
                {
                    _currentCursorState = CursorState.Basic;
                    _menuManager.DisableAbilityMenu();
                    _isMenuLoaded = false;
                }

            }
        }

    }

}

