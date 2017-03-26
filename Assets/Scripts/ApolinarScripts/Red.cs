using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Red : MonoBehaviour {

    public int HP;
    public int ATK;

    public Text RedHPText;

	// Use this for initialization
	void Start () {
        HP = 5;
        ATK = 1;
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public int Attack()
    {
        int damageIncrease = Random.Range(0, 2);
        return ATK + damageIncrease;
    }

    public IEnumerator AttackAction(Wolf wolf, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        int RedDamage = Attack();
        Debug.Log("Red attacks Wolf for " + RedDamage + " damage!");

        //Start attack animation (lerp). Do damage calculations after Red reaches the wolf.

        //Damage calculations
        wolf.HP -= RedDamage;
        if (wolf.HP <= 0)
        {
            wolf.HP = 0;
            ApolinarStateManager.currentState = ApolinarStateManager.BattleStates.Victory;
        }

        if (!ApolinarStateManager.gameOver)
        {
            ApolinarStateManager.PlayerActionRunning = false;
            ApolinarStateManager.currentState = ApolinarStateManager.BattleStates.EnemyAction;
        }

        wolf.DisplayHealth();
    }

    void AttackAnimation()
    {

    }

    public void DisplayHealth()
    {
        RedHPText.text = "Red HP: " + HP;
    }
}
