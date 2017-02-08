using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateManager : MonoBehaviour {
    public GameObject Enemy;
    public GameObject Player;

    public GameObject menu;

    public enum BattleStates
    {
        Start,
        PlayerTurn,
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
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(currentState);
        if (Enemy.GetComponent<Enemy>().hp == 0)
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
            case (BattleStates.PlayerAction):
                menu.SetActive(false);
                break;
            case (BattleStates.EnemyTurn):
                currentState = BattleStates.EnemyAction;
                break;
            case (BattleStates.EnemyAction):
                EnemyAttack.attacking = true;
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