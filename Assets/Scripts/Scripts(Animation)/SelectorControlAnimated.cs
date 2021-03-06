﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorControlAnimated : MonoBehaviour {
	private Vector3 position;
	private string[] commands = new string[4]{"attack", "skill", "item", "defend"};
	private string[] descriptions = new string[4]{"Performs a basic knife attack", "Use a skill that costs MP", "Use an item", "Take less damage next turn"};
	private int command_index = 0;
	public Text actionText;
	public Text descriptionText;
	public float move_val = 4.5f;

    public GameObject player;

    // Initializes position, text and movement spacing variables
    void Start() {
		position = transform.position;
		actionText.text = "Player is choosing an action";
	}
		
	//Changes action text based on chosen command
	void Update() {
		Movement();
		string command = commands [command_index];
		descriptionText.text = descriptions [command_index];
		if (Input.GetKeyDown("z")) {
			if (command == "attack") {
				actionText.text = "Player attacks!";
                //BattleStateManager.currentState = BattleStateManager.BattleStates.PlayerAttackSelection; 
                player.GetComponent<PlayerAttackAnimated>().attacking = true;
                BattleStateManager.currentState = BattleStateManager.BattleStates.PlayerAction;
            } else if (command == "skill") {
				actionText.text = "Player uses a skill!";
				//insert skill function
			} else if (command == "item") {
				actionText.text = "Player uses an item!";
				//insert item function
			} else if (command == "defend") {
				actionText.text = "Player defends!";
				//insert defend function
			}
		}
	}



	//Shifts selector up/down depending on input
	void Movement() {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			ShiftUp();
		} else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            ShiftDown();
		} 
	}
		
	void ShiftUp(){
		if (command_index > 0) {
			position.y += move_val;
			transform.position = position;
			command_index -= 1;
		} else {
			position.y -= (move_val * 3);
			transform.position = position;
			command_index = 3;
		}
	}

	void ShiftDown(){
		if (command_index < 3) {
			position.y -= move_val;
			transform.position = position;
			command_index += 1;
		} else {
			position.y += (move_val * 3);
			transform.position = position;
			command_index = 0;
		}
	}

}
