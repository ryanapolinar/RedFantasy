using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {
        if (BattleStateManager.currentState == BattleStateManager.BattleStates.PlayerTurn)
        {
            if (GUI.Button(new Rect(Screen.width - 450, Screen.height - 50, 100, 30), "Attack"))
            {
                Debug.Log("ATTACK");
                PlayerAttack.attacking = true;
                BattleStateManager.currentState = BattleStateManager.BattleStates.PlayerAction;
            }
        }
    }
}
