using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorControl : MonoBehaviour {
	private Vector3 position;
	private bool justPressed;
	private bool selected;
	private bool onAttack;
	private bool onSkill;
	private bool onItem;
	private bool onDefend;
	private int frames;

	public Text actionText;

	// Initializes position, frames and text variables
	void Start () {
		position = this.transform.position;
		justPressed = false;
		frames = Time.frameCount;
		actionText.text = "Player is choosing an action";


	}

	//Increments frames
	//Changes action text based on chosen command
	void FixedUpdate () {
		Movement ();
		frames++;
		selected = Input.GetKey ("z");
		if (selected) {
			if (onAttack) {
				actionText.text = "Player attacks!";
			} else if (onSkill) {
				actionText.text = "Player uses a skill!";
			} else if (onItem) {
				actionText.text = "Player uses an item!";
			} else if (onDefend) {
				actionText.text = "Player defends!";
			}
		}
	}

	//Sets corresponding boolean for command to true and all others to false on collision
	void OnTriggerStay2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("attack")) {
			onAttack = true;
			onSkill = false;
			onItem = false;
			onDefend = false;
		} else if (other.gameObject.CompareTag ("skill")) {
			onAttack = false;
			onSkill = true;
			onItem = false;
			onDefend = false;
		}
		else if (other.gameObject.CompareTag ("item")){
			onAttack = false;
			onSkill = false;
			onItem = true;
			onDefend = false;
		}
		else if (other.gameObject.CompareTag ("defend")){
			onAttack = false;
			onSkill = false;
			onItem = false;
			onDefend = true;
		}
	}

	//Shifts selector up/down depending on input
	void Movement() {
		
		if (Input.GetKey ("w") && !justPressed) {
			ShiftUp ();
			justPressed = true;
		} else if (Input.GetKey ("s") && !justPressed) {
			ShiftDown ();
			justPressed = true;
		} else {
			Wait();
		}
	}
		
	void ShiftUp(){
			if (position.y < 24) {
				position.y += 12;
			} else if (position.y == 24) {
				position.y = -12;
			}
		this.transform.position = position;
		Wait ();
	}

	void ShiftDown(){
		if (position.y > -12) {
			position.y -= 12;
		} else if (position.y == -12) {
			position.y = 24;
		}
		this.transform.position = position;
		Wait ();
	}

	//Changes justPressed back to false after 20 frames have passed
	void Wait(){
			if(frames > 20){
				justPressed = false;
				frames = 0;
			}
	}
}
