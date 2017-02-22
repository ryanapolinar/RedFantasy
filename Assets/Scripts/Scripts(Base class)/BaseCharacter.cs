using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter {

    string chara;
    private int hp;
    private int maxhp;
    private int atk; //base dmg
    private int def; //base def
    private int exp;
    private int maxExp;
    private bool acted;

    public string NAME
    {
        get { return chara; }
        set { chara = value; }
    }

    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }

    public int MAXHP
    {
        get { return maxhp; }
        set { maxhp = value; }
    }

    public int ATK
    {
        get { return atk; }
        set { atk = value; }
    }

    public int DEF
    {
        get { return def; }
        set { def = value; }
    }

    public int EXP
    {
        get { return exp; }
        set { exp = value; }
    }

    public int MAXEXP
    {
        get { return maxExp; }
        set { maxExp = value; }
    }

    public bool ACTED
    {
        get { return ACTED; }
        set { ACTED = value; }
    }

}
