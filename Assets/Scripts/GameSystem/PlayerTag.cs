using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerTag : MonoBehaviour, IPlayer
{
    public void Execute(string _name)
    {
        Debug.Log(_name);
    }


}
