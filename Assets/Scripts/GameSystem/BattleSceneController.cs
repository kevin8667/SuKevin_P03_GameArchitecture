using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleSceneController : MonoBehaviour
{

    [SerializeField]
    private Text _sceneStateText;

    [SerializeField]
    private Text _turnStateText;

    [SerializeField]
    private Text _winLoseStateText;

    [SerializeField]
    private Text _commandStateText;

    [SerializeField]
    private Text _abilityText;

    [SerializeField]
    private GameObject _pauseMenu;

    [SerializeField]
    private GameObject _endMenu;

    [SerializeField]
    private GameObject _abilityPanel;

    public Text SceneStateText => _sceneStateText;

    public Text TurnStateText => _turnStateText;

    public Text WinLoseStateText => _winLoseStateText;

    public Text CommandStateText => _commandStateText;

    public Text AbilityText => _abilityText;

    public GameObject PauseMenu => _pauseMenu;

    public GameObject EndMenu => _endMenu;

    public GameObject AbilityPanel => _abilityPanel;

    
}
