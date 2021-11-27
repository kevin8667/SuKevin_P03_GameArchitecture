using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTag : MonoBehaviour, IEnemy
{
    public void Execute(string _name)
    {
        Debug.Log(_name);
    }
}
