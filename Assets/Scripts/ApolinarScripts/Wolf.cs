using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wolf : MonoBehaviour {

    public int HP = 5;
    public int ATK = 1;

    //Fields for attacking animation/movement
    public GameObject Red;
    public bool attacking;
    public bool attacked;
    public float timePassed;
    public float movementSpeed = 12;
    private Vector3 originalPosition;

    AudioSource wolfAttack;

    public Text WolfHPText;

	// Use this for initialization
	void Start () {

        attacking = false;
        attacked = false;
        timePassed = 0f;
        originalPosition = transform.position;

        wolfAttack = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        AttackAnimation();
    }

    public int Attack()
    {
        int damageIncrease = Random.Range(0, 2);
        return ATK + damageIncrease;
    }

    public IEnumerator AttackAction(Red red, float delayTime)
    {
        attacking = true;

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

    void AttackAnimation()
    {
        if (attacking)
        {
            if (!attacked && transform.position.x > Red.transform.position.x + 1.5)
            {
                //Move Wolf towards Red
                if (this.GetComponent<Animator>().GetBool("wolf-idle"))
                {
                    this.GetComponent<Animator>().SetBool("wolf-idle", false);
                    this.GetComponent<Animator>().SetTrigger("wolf-move");
                }
                if (transform.position.z > -2)
                {
                    transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
                }
                transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
            }
            if (!attacked && transform.position.x <= Red.transform.position.x + 1.5 && !attacked)
            {
                //If Wolf is at Red, play attack animation
                this.GetComponent<Animator>().SetTrigger("wolf-attack");
                wolfAttack.Play();
                Red.GetComponent<Animator>().SetTrigger("red-hit");
                attacked = true;
            }

            //Pass time while attack animation plays
            if (attacked) { timePassed += Time.deltaTime; }

            if (attacked && transform.position.x < originalPosition.x && timePassed >= 0.48)
            {
                //Move Wolf back to original position

                this.GetComponent<Animator>().SetTrigger("wolf-move");
                transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
                if (transform.position.z <= 0)
                {
                    transform.Translate(Vector3.forward * (movementSpeed/2) * Time.deltaTime);
                }
            }
            if (attacked && transform.position.x >= originalPosition.x)
            {
                //Wolf is back in original position, stop moving it.

                transform.position = originalPosition;
                this.GetComponent<Animator>().SetBool("wolf-idle", true);
                attacking = false;
                attacked = false;
                timePassed = 0f;
            }
        }
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
            case 8:
            case 9:
                result = 0;
                break;
        }
        return result;
    }
}
