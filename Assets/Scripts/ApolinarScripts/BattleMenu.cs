using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMenu : MonoBehaviour {

    public int selection = 0;
    public bool confirmed = false;
    private string[] commandInfo = new string[3] { "Physical knife strike.", "Uses healing magic.", "Do nothing."};

    public Text AttackText, HealText, WaitText, CommandInfoText;
    public GameObject AttackPointer, HealPointer, WaitPointer;

    AudioSource[] sounds;

    // Use this for initialization
    void Start () {
        sounds = GetComponents<AudioSource>();
        DrawSelectedCommand();
    }
	
	// Update is called once per frame
	void Update () {
    }

    public void SelectionMove()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            sounds[0].Play();
            if (selection == 0)
            {
                selection = commandInfo.Length - 1;
            }
            else
            {
                selection--;
            }
            DrawSelectedCommand();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            sounds[0].Play();
            if (selection == commandInfo.Length - 1)
            {
                selection = 0;
            }
            else
            {
                selection++;
            }
            DrawSelectedCommand();
            //Debug.Log("selection: " + selection + " " + commandInfo[selection]);
        }

    }

    public void ConfirmSelection()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            confirmed = true;
        }
    }

    public void DrawSelectedCommand()
    {
        AttackText.color = Color.white;
        HealText.color = Color.white;
        WaitText.color = Color.white;

        AttackPointer.SetActive(false);
        HealPointer.SetActive(false);
        WaitPointer.SetActive(false);

        switch (selection)
        {
            case 0:
                AttackPointer.SetActive(true);
                AttackText.color = Color.red;
                break;
            case 1:
                HealPointer.SetActive(true);
                HealText.color = Color.red;
                break;
            case 2:
                WaitPointer.SetActive(true);
                WaitText.color = Color.red;
                break;
        }

        CommandInfoText.text = commandInfo[selection];
    }
}
