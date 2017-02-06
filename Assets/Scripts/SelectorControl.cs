using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorControl : MonoBehaviour {
	private Vector3 position;
	//private bool justPressed;
	private string[] commands = new string[4]{"attack", "skill", "item", "defend"};
	private int command_index = 0;
	public Text actionText;
	public float move_val;

	// Initializes position, frames and text variables
	void Start () {
		position = transform.position;
		actionText.text = "Player is choosing an action";
		move_val = 4.5f;


	}

	//Increments frames
	//Changes action text based on chosen command
	void Update () {
		Movement ();
		string command = commands [command_index];
		if (Input.GetKeyDown("z")) {
			if (command == "attack") {
				actionText.text = "Player attacks!";
				//insert attack function
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
		if (Input.GetKeyDown ("up")) {
			ShiftUp ();
		} else if (Input.GetKeyDown ("down")) {
			ShiftDown ();
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
