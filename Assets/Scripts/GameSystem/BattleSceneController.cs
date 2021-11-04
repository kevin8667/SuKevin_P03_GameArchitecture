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
    private GameObject _pauseMenu;

    [SerializeField]
    private GameObject _endMenu;

    public Text SceneStateText => _sceneStateText;

    public Text TurnStateText => _turnStateText;

    public GameObject PauseMenu => _pauseMenu;

    public GameObject EndMenu => _endMenu;

    public Text WinLoseStateText => _winLoseStateText;
}
