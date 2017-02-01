using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPUI : MonoBehaviour {
    public GameObject enemy ;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "Enemy HP: " + enemy.GetComponent<Enemy>().hp;
    }
}
