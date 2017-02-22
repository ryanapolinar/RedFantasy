using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedClass : MonoBehaviour {


    private BaseCharacter Red = new BaseCharacter();

    public BaseCharacter RED {
        get { return Red; }
        set { Red = value; }
    }

	// Use this for initialization
	void Awake ()
    {
        Red = new BaseCharacter() {NAME = "Red", HP = 3, MAXHP = 3, ATK = 1, DEF = 1, EXP = 0, MAXEXP = 100, ACTED = false};
	}

    // Update is called once per frame
    void DisplayCharacterStats()
    {

        Debug.Log("Name: " + Red.NAME);
        Debug.Log("HP: " + Red.HP);
        Debug.Log("MAX HP: " + Red.MAXHP);
        Debug.Log("ATK: " + Red.ATK);
        Debug.Log("DEF: " + Red.DEF);
        Debug.Log("Experience: " + Red.EXP);
        Debug.Log("Experience Max: " + Red.MAXEXP);
        Debug.Log("Acted: " + Red.ACTED);
    }
}
