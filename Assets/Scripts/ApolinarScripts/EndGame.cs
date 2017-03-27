using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

    public Text EndGameText;

	// Use this for initialization
	void Start () {
        if (ApolinarStateManager.currentState == ApolinarStateManager.BattleStates.Victory)
        {
            EndGameText.text = "Victory!";
        } else if (ApolinarStateManager.currentState == ApolinarStateManager.BattleStates.Defeat)
        {
            EndGameText.text = "Defeat!";
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("ApolinarGame");
        }
    }
}
