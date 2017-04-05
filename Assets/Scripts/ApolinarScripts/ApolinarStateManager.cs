using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApolinarStateManager : MonoBehaviour {

    public enum BattleStates
    {
        //Start,
        PlayerTurn,
        PlayerAction,
        EnemyAction,
        Victory,
        Defeat
    }

    //Keeps track of the current state and whether the game is over
    public static BattleStates currentState;
    public static bool PlayerActionRunning;
    public static bool gameOver;

    //Battle Units
    public Red Red;
    public Wolf Wolf;
    //Battle Menu to communicate player's inputs
    public BattleMenu PlayerMenuScript;
    public GameObject PlayerMenu, InfoBox, PartyBox, WolfHealthText;   //for going active/inactive

    // Use this for initialization
    void Start () {
        currentState = BattleStates.PlayerTurn;
        PlayerMenuScript.confirmed = false;
        PlayerActionRunning = false;
        gameOver = false;

        Red.DisplayHealth();
        Wolf.DisplayHealth();
    }
	
	// Update is called once per frame
	void Update () {
        switch (currentState)
        {
            case BattleStates.PlayerTurn:

                //Get the player's input and confirm their selection
                PlayerMenuScript.SelectionMove();
                if (PlayerMenu.activeSelf)
                    PlayerMenuScript.ConfirmSelection();

                //Switch state to PlayerAction when confirmed
                if (PlayerMenuScript.confirmed)
                {
                    PlayerMenuScript.confirmed = false;
                    //Perform player's action based on selection
                    if (!gameOver)
                    {
                        currentState = BattleStates.PlayerAction;
                    }
                }
                break;

            case BattleStates.PlayerAction:
                //Disable the menu until the player's next turn
                PlayerMenu.SetActive(false);
                InfoBox.SetActive(false);
                
                //Display animation based on player's selection

                if (!PlayerActionRunning)
                {
                    PlayerActionRunning = true;
                    switch (PlayerMenuScript.selection)
                    {
                        case 0:
                            PartyBox.SetActive(false);
                            StartCoroutine(Red.AttackAction(Wolf, 1f));
                            Invoke("CheckEndGame", 1.1f);
                            break;
                        case 1:
                            StartCoroutine(Red.HealAction(1f));
                            break;
                        case 2:
                            PartyBox.SetActive(false);
                            Debug.Log("Red waits!");
                            if (!gameOver)
                            {
                                PlayerActionRunning = false;
                                currentState = BattleStates.EnemyAction;
                            }
                            break;
                    }
                }
                break;

            case BattleStates.EnemyAction:
                Debug.Log("EnemyAction state.");
                if (Wolf.HP > 0)
                {
                    //Generate a move for the enemy
                    int WolfMove = Wolf.GenerateMove();
                    //Display the action based on the move
                    switch (WolfMove)
                    {
                        case 0:
                            StartCoroutine(Wolf.AttackAction(Red, 2f));
                            Invoke("CheckEndGame", 2.1f);
                            break;
                        case 1:
                            StartCoroutine(Wolf.DefendAnimation(Red, 2f));
                            break;
                    }

                    if (!gameOver)
                    {
                        Invoke("ActivatePlayerMenu", 2.2f);
                        currentState = BattleStates.PlayerTurn;
                    }

                }
                break;

            case BattleStates.Victory:
                Invoke("VictoryTransition", 2);
                break;

            case BattleStates.Defeat:
                Invoke("DefeatTransition", 2);
                break;
   
        }
	}

    void CheckEndGame()
    {
        if (Red.HP <= 0)
        {
            //Red loses
            Red.HP = 0;
            currentState = BattleStates.Defeat;
            gameOver = true;
        }
        else if (Wolf.HP <= 0)
        {
            //Red wins
            Wolf.HP = 0;
            currentState = BattleStates.Victory;
            gameOver = true;
        }
    }

    void VictoryTransition()
    {
        Debug.Log("Red wins! Victory!");
        SceneManager.LoadScene("EndGame");
    }

    void DefeatTransition()
    {
        Debug.Log("You lose. Defeat.");
        SceneManager.LoadScene("EndGame");
    }

    void ActivatePlayerMenu()
    {
        InfoBox.SetActive(true);
        PartyBox.SetActive(true);
        PlayerMenu.SetActive(true);
        PlayerMenuScript.DrawSelectedCommand();
    }

}