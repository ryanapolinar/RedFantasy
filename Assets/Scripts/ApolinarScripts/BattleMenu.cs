using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMenu : MonoBehaviour {

    public int selection = 0;
    public bool confirmed = false;
    private string[] commands = new string[4] { "attack", "skills", "items", "defend" };

    public Text AttackText, SkillsText, ItemsText, DefendText;

    // Use this for initialization
    void Start () {
        DrawSelectedCommand();
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void SelectionMove()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            if (selection == 0)
            {
                selection = commands.Length - 1;
            }
            else
            {
                selection--;
            }
            DrawSelectedCommand();
            //Debug.Log("selection: " + selection + " " + commands[selection]);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            if (selection == commands.Length - 1)
            {
                selection = 0;
            }
            else
            {
                selection++;
            }
            DrawSelectedCommand();
            //Debug.Log("selection: " + selection + " " + commands[selection]);
        }
    }

    public void ConfirmSelection()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            confirmed = true;
        }
    }

    void DrawSelectedCommand()
    {
        AttackText.color = Color.white;
        SkillsText.color = Color.white;
        ItemsText.color = Color.white;
        DefendText.color = Color.white;
        switch (selection)
        {
            case 0:
                AttackText.color = Color.red;
                break;
            case 1:
                SkillsText.color = Color.red;
                break;
            case 2:
                ItemsText.color = Color.red;
                break;
            case 3:
                DefendText.color = Color.red;
                break;
        }
    }
}
