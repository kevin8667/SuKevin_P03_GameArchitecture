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
    private Text _enemyTexts;

    [SerializeField]
    private Text[] _playerChrNameText;

    [SerializeField]
    private Text[] _playerChrHPText;

    [SerializeField]
    private Text[] _playerChrMPText;

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

    public Text EnemyTexts => _enemyTexts;

    public Text[] PlayerChrNameText => _playerChrNameText;

    public Text[] PlayerChrHPText => _playerChrHPText;

    public Text[] PlayerChrMPText => _playerChrMPText;


    public GameObject PauseMenu => _pauseMenu;

    public GameObject EndMenu => _endMenu;

    public GameObject AbilityPanel => _abilityPanel;



}
