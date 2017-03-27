using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Red : MonoBehaviour {

    public int HP;
    public int ATK;

    //Fields for attacking animation/movement
    public GameObject Wolf;
    public bool attacking = false;
    public bool attacked = false;
    public float timePassed = 0f;
    public float movementSpeed = 20;
    private Vector3 originalPosition;

    public Text RedHPText;

	// Use this for initialization
	void Start () {
        HP = 5;
        ATK = 1;

        originalPosition = transform.position;		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log("red-attack playing: " + this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("red-attack"));
        AttackAnimation();
	}

    public int Attack()
    {
        int damageIncrease = Random.Range(0, 2);
        return ATK + damageIncrease;
    }

    public IEnumerator AttackAction(Wolf wolf, float delayTime)
    {
        //Start attack animation (lerp). Do damage calculations after Red reaches the wolf.
        attacking = true;

        yield return new WaitForSeconds(delayTime);
        int RedDamage = Attack();
        Debug.Log("Red attacks Wolf for " + RedDamage + " damage!");

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
        if (attacking)
        {
            if (!attacked && transform.position.x < Wolf.transform.position.x - 1.5)
            {
                //Move Red towards Wolf
                transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
            }
            if (!attacked && transform.position.x >= Wolf.transform.position.x - 1.5 && !attacked)
            {
                //If Red is at Wolf, play attack animation
                this.GetComponent<Animator>().SetTrigger("red_attack");
                attacked = true;
            }
            if (attacked && transform.position.x > originalPosition.x && !this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("red-attack"))
            {
                //Move Red back to original position
                transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
            }
            if (attacked && transform.position.x <= originalPosition.x)
            {
                //Red is back in original position, stop moving her.
                Debug.Log("BACK AT ORIGINAL POSITION.");
                transform.position = originalPosition;
                attacking = false;
                attacked = false;
            }
        }

    }

    public void DisplayHealth()
    {
        RedHPText.text = "Red HP: " + HP;
    }
}
