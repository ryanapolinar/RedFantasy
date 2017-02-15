using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartMenu : MonoBehaviour {
	private Vector3 position;
	private int max;
	private string[] commands;
	private int command_index;

	public GameObject instructions;
	public GameObject startMenu;
	public GameObject cursor;
	public float move_val = 2f;


	// Use this for initialization
	void Start () {
		position = cursor.transform.position;
		max = 2;
		command_index = 0;
		commands = new string[3]{ "play", "instructions", "quit" };
		instructions.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
		string command = commands [command_index];
		if (Input.GetKeyDown("z")) {
			if (command == "play") {
				SceneManager.LoadScene ("RichardBut", LoadSceneMode.Single);
			} else if (command == "quit") {
				Application.Quit ();
			} else if (command == "instructions") {
				instructions.SetActive (true);
				startMenu.SetActive (false);
				cursor.SetActive (false);
			}
		}
		else if (Input.anyKeyDown) {
			instructions.SetActive (false);
			startMenu.SetActive (true);
			cursor.SetActive (true);
		}
	}

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
			cursor.transform.position = position;
			command_index -= 1;
		} else {
			position.y -= (move_val * 2);
			cursor.transform.position = position;
			command_index = 2;
		}
	}

	void ShiftDown(){
		if (command_index < max) {
			position.y -= move_val;
			cursor.transform.position = position;
			command_index += 1;
		} else {
			position.y += (move_val * max);
			cursor.transform.position = position;
			command_index = 0;
		}
	}
}
