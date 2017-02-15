using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSelector : MonoBehaviour {
    public GameObject test; // will change
    public GameObject menu; // will change

    private Vector3 position;
    private string[] commands = new string[2] { "resume", "main"};
    private int command_index = 0;
    public float move_val = 4.5f;

    // Use this for initialization
    void Start () {
        position = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        Movement();
        string command = commands[command_index];
        if (Input.GetKeyDown("z"))
        {
            if (command == "resume")
            {
                Time.timeScale = 1.0f;
                test.GetComponent<PauseMenu>().isShowing = false;
                menu.SetActive(test.GetComponent<PauseMenu>().isShowing);
            }
            else if (command == "main")
            {
                SceneManager.LoadScene("Main Menu");
            }
        }
    }

    //Shifts selector up/down depending on input
    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ShiftUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ShiftDown();
        }
    }

    void ShiftUp()
    {
        if (command_index > 0)
        {
            position.y += move_val;
            transform.position = position;
            command_index -= 1;
        }
        else
        {
            position.y -= (move_val);
            transform.position = position;
            command_index = 3;
        }
    }

    void ShiftDown()
    {
        if (command_index < 1)
        {
            position.y -= move_val;
            transform.position = position;
            command_index += 1;
        }
        else
        {
            position.y += (move_val);
            transform.position = position;
            command_index = 0;
        }
    }
}
