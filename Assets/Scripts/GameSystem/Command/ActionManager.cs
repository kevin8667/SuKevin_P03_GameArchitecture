using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    [SerializeField] CharacterManager _chrManager;
    [SerializeField] BattleSceneController _controller;

    List<GameObject> _chrList;



    private List<ICommand> _commands;

    private int _currentOrder;



    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        _chrList = new List<GameObject>(_chrManager.GetComponent<CharacterManager>()._characterList);
        DetermineCommandSequence();
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

        for (int i = 0; i < _chrList.Count; i++)
        {
            _commands.Add(new Attack(_chrList[i], _controller));
        }
    }

    private IEnumerator ExecuteCommandSequence(float commandDuration)
    {
        foreach (ICommand command in _commands)
        {
            command.Execute();
            yield return new WaitForSeconds(commandDuration);
        }
    }



}
