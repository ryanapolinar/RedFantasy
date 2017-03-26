using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wolf : MonoBehaviour {

    public int HP = 5;
    public int ATK = 1;

    public Text WolfHPText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int Attack()
    {
        int damageIncrease = Random.Range(0, 2);
        return ATK + damageIncrease;
    }

    public IEnumerator AttackAnimation(Red red, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        int WolfDamage = Attack();
        Debug.Log("Wolf attacks Red for " + WolfDamage + " damage!");
        red.HP -= WolfDamage;
        if (red.HP <= 0)
        {
            red.HP = 0;
            ApolinarStateManager.currentState = ApolinarStateManager.BattleStates.Defeat;
        }
        red.DisplayHealth();
    }

    public IEnumerator DefendAnimation(Red red, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        Debug.Log("Wolf defends.");
    }

    public void DisplayHealth()
    {
        WolfHPText.text = "Wolf HP: " + HP;
    }

    public int GenerateMove()
    {
        //Generates a random move for the Wolf.
        //0: Attack the player
        //1: Defend
        return Random.Range(0, 2);
    }
}
