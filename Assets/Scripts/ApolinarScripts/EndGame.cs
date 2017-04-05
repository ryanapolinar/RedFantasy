using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

    public GameObject WinGrandma, LoseBasket;

    public Text EndGameText;

	// Use this for initialization
	void Start () {
        WinGrandma.SetActive(false);
        LoseBasket.SetActive(false);
        if (ApolinarStateManager.currentState == ApolinarStateManager.BattleStates.Victory)
        {
            WinGrandma.SetActive(true);
            EndGameText.text = "Victory!";
        } else if (ApolinarStateManager.currentState == ApolinarStateManager.BattleStates.Defeat)
        {
            LoseBasket.SetActive(true);
            EndGameText.text = "You lost.";
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScreen");
        }
    }
}
