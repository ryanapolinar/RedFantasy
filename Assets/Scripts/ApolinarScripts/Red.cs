using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Red : MonoBehaviour {

    public int MAXHP = 5;
    public int HP;
    public int ATK;

    //Fields for attacking animation/movement
    public GameObject Wolf;
    public bool attacking;
    public bool attacked;
    public float timePassed;
    public float movementSpeed = 21;
    private Vector3 originalPosition;

    AudioSource[] sounds; 
    public GameObject healEffect;

    public Text RedHPText;

	// Use this for initialization
	void Start () {
        HP = MAXHP;
        ATK = 1;

        attacking = false;
        attacked = false;
        timePassed = 0f;
        originalPosition = transform.position;

        sounds = GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        AttackAnimation();
	}

    public int Attack()
    {
        int damageIncrease = Random.Range(0, 3);
        return ATK + damageIncrease;
    }

    public int Heal()
    {
        int heal = Random.Range(3, 5);
        return heal;
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

    public IEnumerator HealAction(float delayTime)
    {
        healEffect.SetActive(true);
        sounds[1].Play();
        yield return new WaitForSeconds(delayTime);

        int restoredHealth = Heal();
        if (HP + restoredHealth > MAXHP)
            restoredHealth = MAXHP - HP;

        HP += restoredHealth;
        healEffect.SetActive(false);

        Debug.Log("Red heals for " + restoredHealth + " health!");

        if (!ApolinarStateManager.gameOver)
        {
            ApolinarStateManager.PlayerActionRunning = false;
            ApolinarStateManager.currentState = ApolinarStateManager.BattleStates.EnemyAction;
        }

        GetComponent<SpriteRenderer>().color = Color.white;
        DisplayHealth();
    }

    void AttackAnimation()
    {
        if (attacking)
        {
            if (!attacked && transform.position.x < Wolf.transform.position.x - 1.5)
            {
                //Move Red towards Wolf
                if (this.GetComponent<Animator>().GetBool("red-idle"))
                {
                    this.GetComponent<Animator>().SetBool("red-idle", false);
                    this.GetComponent<Animator>().SetTrigger("red-move");
                }
                transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
            }
            if (!attacked && transform.position.x >= Wolf.transform.position.x - 1.5 && !attacked)
            {
                //If Red is at Wolf, play attack animation
                this.GetComponent<Animator>().SetTrigger("red-attack");
                sounds[0].Play();
                Wolf.GetComponent<Animator>().SetBool("wolf-hit", true);
                Wolf.GetComponent<Animator>().SetBool("wolf-idle", false);
                

                attacked = true;
            }

            //Pass time while attack animation plays
            if (attacked) { timePassed += Time.deltaTime; }

            if (attacked && transform.position.x > originalPosition.x && timePassed >= 0.48)
            {
                //Move Red back to original position
                this.GetComponent<Animator>().SetTrigger("red-move");
                Wolf.GetComponent<Animator>().SetBool("wolf-hit", false);
                Wolf.GetComponent<Animator>().SetBool("wolf-idle", true);
                transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
            }
            if (attacked && transform.position.x <= originalPosition.x)
            {
                //Red is back in original position, stop moving her.

                transform.position = originalPosition;
                this.GetComponent<Animator>().SetBool("red-idle", true);
                attacking = false;
                attacked = false;
                timePassed = 0f;
            }
        }

    }

    public void DisplayHealth()
    {
        RedHPText.text = "Red HP: " + HP + " / " + MAXHP;
    }

}
