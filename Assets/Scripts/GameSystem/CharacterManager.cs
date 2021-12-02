using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public List<GameObject> _characterList = new List<GameObject>();
    public List<GameObject> _playerChrList = new List<GameObject>();
    public List<GameObject> _enemyChrList = new List<GameObject>();

    public int _currentCharacter;

    private List<int> _SPDList = new List<int>();

    BattleSceneController _battleSceneController;

    IPlayer _Iplayer;
    IEnemy _IEnemy;

    bool _isDead;

    ConditionFSM _conditionFSM;


    private void Awake()
    {
        _currentCharacter = 0;

        GameObject[] _allCharacter = GameObject.FindGameObjectsWithTag("Character");

        _battleSceneController = GameObject.Find("BattleSceneController").GetComponent<BattleSceneController>();

        _conditionFSM = GameObject.Find("ConditionFSM").GetComponent<ConditionFSM>();

        _isDead = false;

        foreach (GameObject character in _allCharacter)
        {
            _characterList.Add(character);
        }

        for (int i = 0; i < _characterList.Count; i++)
        {
            _SPDList.Add(_characterList[i].GetComponent<Attr>().SPD);


        }

        
        SortOrder(_characterList);

        ListEditor(_characterList);




    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {  
            _SPDList.Sort();
            _SPDList.Reverse();
            for (int i = 0; i < _SPDList.Count; i++)
            {
                Debug.Log(_SPDList[i]);
            }

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _enemyChrList[0].GetComponent<Attr>().HP = 0;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _playerChrList[0].GetComponent<Attr>().HP = 0;

        }

        if(_playerChrList.Count == 0)
        {
            StartCoroutine(ExecuteLoseSequence(1));
        }

        if(_enemyChrList.Count == 0)
        {

            StartCoroutine(ExecuteWinSequence(1));

        }

        RemoveDead();


        

        
    }

    public void SortOrder(List<GameObject> _characterList)
    {
        if (_characterList.Count > 0)
        {
            _characterList.Sort(delegate (GameObject a, GameObject b)
            {
                return (b.GetComponent<Attr>().SPD).CompareTo(a.GetComponent<Attr>().SPD);
            });

        }
    }

    public void PrintOrder(List<GameObject> _characterList)
    {
        for (int i = 0; i < _characterList.Count; i++)
        {
            if (_characterList[i].GetComponent<IPlayer>() != null)
            {
                _Iplayer = _characterList[i].GetComponent<IPlayer>();
                _Iplayer.Execute(_characterList[i].name);
            }
            else if (_characterList[i].GetComponent<IPlayer>() == null && _characterList[i].GetComponent<IEnemy>() != null)
            {
                _IEnemy = _characterList[i].GetComponent<IEnemy>();
                _IEnemy.Execute(_characterList[i].name);

            }
        }
    }
    

    public void ListEditor(List<GameObject> _characterList)
    {
        for (int i = 0; i < _characterList.Count; i++)
        {
            if (_characterList[i].GetComponent<IPlayer>() != null)
            {
                _Iplayer = _characterList[i].GetComponent<IPlayer>();

                _playerChrList.Add(_characterList[i]);


            }
            else if (_characterList[i].GetComponent<IPlayer>() == null && _characterList[i].GetComponent<IEnemy>() != null)
            {
                _IEnemy = _characterList[i].GetComponent<IEnemy>();

                _enemyChrList.Add(_characterList[i]);

            }
        }
    }

    public void RemoveDead()
    {
            
        foreach (GameObject chr in _characterList)
        {
            
            if (chr.GetComponent<Attr>().HP <= 0)
            {
                _isDead = true;
                if (_isDead == true)
                {
                    if (chr.GetComponent<IPlayer>() != null)
                    {
                        chr.GetComponent<controller>().Die();
                        _playerChrList.Remove(chr);
                        _isDead = false;

                    }
                    else if (chr.GetComponent<IEnemy>() != null)
                    {
                        chr.GetComponent<controller>().Die();
                        _enemyChrList.Remove(chr);
                        _isDead = false;
                    }
                }
                _characterList.Remove(chr);
                break;
                
            }
        }

    }

    private IEnumerator ExecuteWinSequence(float commandDuration)
    {
         yield return new WaitForSeconds(commandDuration);
        _conditionFSM.WinTheGame();

    }

    private IEnumerator ExecuteLoseSequence(float commandDuration)
    {
        yield return new WaitForSeconds(commandDuration);
        _conditionFSM.LoseTheGame();

    }

}

