using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfClass : MonoBehaviour
{


    private BaseCharacter Wolf = new BaseCharacter();

    public BaseCharacter WOLF
    {
        get { return Wolf; }
        set { Wolf = value; }
    }

    // Use this for initialization
    void Awake()
    {
        Wolf = new BaseCharacter() { NAME = "Wolf", HP = 3, MAXHP = 3, ATK = 1, DEF = 1, EXP = 0, MAXEXP = 100, ACTED = false };
    }

    // Update is called once per frame
    void DisplayCharacterStats()
    {

        Debug.Log("Name: " + Wolf.NAME);
        Debug.Log("HP: " + Wolf.HP);
        Debug.Log("MAX HP: " + Wolf.MAXHP);
        Debug.Log("ATK: " + Wolf.ATK);
        Debug.Log("DEF: " + Wolf.DEF);
        Debug.Log("Experience: " + Wolf.EXP);
        Debug.Log("Experience Max: " + Wolf.MAXEXP);
        Debug.Log("Acted: " + Wolf.ACTED);
    }
}
