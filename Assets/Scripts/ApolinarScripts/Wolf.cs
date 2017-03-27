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
        //0-6: Attack the player    (80% chance to attack)
        //7-9: Defend               (20% chance to defend/wait)   
        int result = Random.Range(0, 10);
        switch (result)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
                result = 0;
                break;
            case 8:
            case 9:
                result = 1;
                break;
        }
        return result;
    }
}
