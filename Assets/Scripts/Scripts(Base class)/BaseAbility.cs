using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAbility {

    private string name; //ability name
    private int id; //ability ID
    private int ad; //ability dmg

    public string NAME {
        get { return name; }
        set { name = value; }
    }

    public int ID {
        get { return id; }
        set { id = value; }
    }

    public int AD {
        get { return ad; }
        set { ad = value; }
    }

}
