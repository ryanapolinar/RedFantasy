using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAbility : MonoBehaviour {

    private List<BaseAbility> abilities;
    private BaseAbility atk_0 = new BaseAbility(); //DEFAULT ATK
    private BaseAbility atk_1 = new BaseAbility(); //ACTUAL ABILITY 
    public BaseCharacter enemy;

    public List<BaseAbility> BaseAbility{
        get { return abilities; }
        set { abilities = value; }
    }

    // Use this for initialization
    void Awake()
    {
        enemy = GetComponent<WolfClass>().WOLF;

        abilities = new List<BaseAbility>();
        atk_0 = new BaseAbility() { NAME = "Attack", ID = 0, AD = enemy.ATK };
        atk_1 = new BaseAbility() { NAME = "Strong Attack", ID = 1, AD = (enemy.ATK * 2) }; //value can be replaced by function for calculation
        abilities.Add(atk_0);
        abilities.Add(atk_1);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
