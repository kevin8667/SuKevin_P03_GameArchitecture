using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDecider : MonoBehaviour
{
    public List<PhysicAbility> _physicalAbilityList = new List<PhysicAbility>();
    public List<MagicallAbility> _magicalAbilityList = new List<MagicallAbility>();

    // Start is called before the first frame update
    void Start()
    {
        _physicalAbilityList = GetComponent<AbilityLoader>()._physicalAbilityList;
        _magicalAbilityList = GetComponent<AbilityLoader>()._magicalAbilityList;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
