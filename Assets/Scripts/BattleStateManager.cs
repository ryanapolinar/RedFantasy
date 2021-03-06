﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateManager : MonoBehaviour {
    public GameObject enemy;
    public GameObject Player;

    public GameObject menu;
    public GameObject AtkMenu;

    public enum BattleStates
    {
        Start,
        PlayerTurn,
        PlayerAttackSelection,
        PlayerAction,
        EnemyTurn,
        EnemyAction,
        Victory,
        Defeat
    }

    public static BattleStates currentState = BattleStates.Start;

    // Use this for initialization
    void Start () {
		currentState = BattleStates.Start;
        AtkMenu.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(currentState);
        if (enemy.GetComponent<Enemy>().hp == 0)
        {
            currentState = BattleStates.Victory;
        }

        if (Player.GetComponent<Player>().hp == 0)
        {
            currentState = BattleStates.Defeat;
        }

		switch(currentState)
        {
            case (BattleStates.Start):
                currentState = BattleStates.PlayerTurn;
                break;
            case (BattleStates.PlayerTurn):
                menu.SetActive(true);
                break;
            case (BattleStates.PlayerAttackSelection):
                //menu.SetActive(false);
                AtkMenu.SetActive(true); //activate AtkMenu
                break;
            case (BattleStates.PlayerAction):
                AtkMenu.SetActive(false);
                menu.SetActive(false); //set AtkMenu to inactive 
                break;
            case (BattleStates.EnemyTurn):
                currentState = BattleStates.EnemyAction;
                break;
            case (BattleStates.EnemyAction):
                Enemy.myTurn = true;
                break;
            case (BattleStates.Victory):
                Debug.Log("VICTORY");
                break;
            case (BattleStates.Defeat):
                Debug.Log("DEFEAT");
                break;
        }
	}
}