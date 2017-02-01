using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPUI : MonoBehaviour {
    public GameObject player;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "Player HP: " + player.GetComponent<Player>().hp;
    }
}
