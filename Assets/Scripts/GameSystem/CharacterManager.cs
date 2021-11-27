using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public List<GameObject> _characterList = new List<GameObject>();
    public List<GameObject> _playerChrList = new List<GameObject>();
    public List<GameObject> _enemyChrList = new List<GameObject>();


    private List<int> _SPDList = new List<int>();

    IPlayer _Iplayer;
    IEnemy _IEnemy;


    private void Awake()
    {
        GameObject[] _allCharacter = GameObject.FindGameObjectsWithTag("Character");
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
            SortOrder(_characterList);
            PrintOrder(_characterList);
        }

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

}

