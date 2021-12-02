using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject _abilityMenu;


    private void Start()
    {
        _abilityMenu.SetActive(false);
    }

    public void EnableAbilityMenu()
    {
        _abilityMenu.SetActive(true);
    }

    public void DisableAbilityMenu()
    {
        _abilityMenu.SetActive(false);
    }


}
