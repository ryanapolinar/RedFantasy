using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAbility : MonoBehaviour {

    private List<BaseAbility> abilities;
    private BaseAbility atk_0 = new BaseAbility(); //DEFAULT ATK
    private BaseAbility atk_1 = new BaseAbility(); //ACTUAL ATK
    public BaseCharacter player; 

    public List<BaseAbility> BaseAbility {
        get { return abilities; }
        set { abilities = value; }
    }

	// Use this for initialization
	void Awake () {
        player = GetComponent<RedClass>().RED;
        
        abilities = new List<BaseAbility>();
        atk_0 = new BaseAbility() { NAME = "Attack", ID = 0, AD = player.ATK};
        atk_1 = new BaseAbility() { NAME = "Strong Attack", ID = 1, AD = (player.ATK * 2)};
        abilities.Add(atk_0);
        abilities.Add(atk_1);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
