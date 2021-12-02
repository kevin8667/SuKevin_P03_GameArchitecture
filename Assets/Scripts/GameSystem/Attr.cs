using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attr : MonoBehaviour
{
    [SerializeField] Text _nameText;
    [SerializeField] Text _hpText;
    [SerializeField] Text _mpText;

    public int HP;
    public int MP;
    public int ATK;
    public int DEF;
    public int VIT;
    public int STR;
    public int INT;
    public int WIS;
    public int DEX;
    public int SPD;

    private void Start()
    {
        if(_nameText != null)
        {
            _nameText.text = gameObject.name;
        }

        if(_hpText != null)
        {

            _hpText.text = HP.ToString();
        }

        if(_mpText != null)
        {
            _mpText.text = MP.ToString();
        }
        

    }

    private void Update()
    {
        if (_nameText != null)
        {
            _nameText.text = gameObject.name;
        }

        if (_hpText != null)
        {

            _hpText.text = HP.ToString();
        }

        if (_mpText != null)
        {
            _mpText.text = MP.ToString();
        }
    }


}
