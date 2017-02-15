using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMenu : MonoBehaviour {

    //should not be string but ability classes 
    private delegate IEnumerator playerAttack();
    private bool choseAction = false;
    private List<playerAttack> playerAttacks;
    private IEnumerator turnRoutine = null;
    public static bool myTurn = false;
    private float timePassed = 0f;
    private Vector3 originalPosition;
    public GameObject Enemy;
    private bool debug_printAction = true;
    public float movementDuration = 3f;
    public int normalDamage = 1; //should reference player stats if there is one
    public int heavyDamage = 2;
    private playerAttack selected;

    // Use this for initialization
    void Start () {
        originalPosition = transform.position;
        playerAttacks = new List<playerAttack> { Attack, StrongAttack };

	}
	
	// Update is called once per frame
	void Update () {//player has to make selection during the update while looping in the selection state
        //if (myTurn && turnRoutine == null)
        //{
            //turnRoutine = playerAttacks[];





            //StartCoroutine(turnRoutine);
        //}
	}

    void Selector()
    {
        int index = 0;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            index++;
            if (index == playerAttacks.Count)
            {
                index = playerAttacks.Count;
                selected = playerAttacks[index];
            }
            else if (index > playerAttacks.Count)
            {
                index = 0;
                selected = playerAttacks[index];
            }
            else
            {
                selected = playerAttacks[index];
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            index--;
            if (index == 0)
            {
                //set to 0
                selected = playerAttacks[index];
            }
            else if (index < 0)
            {
                //set to max
                index = playerAttacks.Count;
                selected = playerAttacks[index];
            }
            else
            {
                selected = playerAttacks[index];
            }
        }
    }


    public IEnumerator Attack() {
        Debug.Log("Player Attack");

        StartCoroutine(abstractMeleeAttackEnemy(movementDuration, normalDamage));

        yield return new WaitForSeconds(movementDuration * 2); // * 2 because it's there and back
        yield return 0; // Just to make sure we don't endTurn() too quickly
        endTurn();

    }


    public IEnumerator StrongAttack() {
        Debug.Log("Player Strong Attack");
        printAction("Normal Attack");

        StartCoroutine(abstractMeleeAttackEnemy(movementDuration, heavyDamage));

        yield return new WaitForSeconds(movementDuration * 2); // * 2 because it's there and back
        yield return 0; // Just to make sure we don't endTurn() too quickly
        endTurn();

    }

    private IEnumerator abstractMeleeAttackEnemy(float moveDuration, int damage)
    {
        bool attacking = true;
        bool attacked = false;
        timePassed = 0;

        while (attacking)
        {
            //print(timePassed + " | " + moveDuration + " | " + attacking + " | " + attacked);

            if (attacked)
            {
                transform.position = Vector3.Lerp(Enemy.transform.position, originalPosition, timePassed / moveDuration);
            }
            else
            {
                transform.position = Vector3.Lerp(originalPosition, Enemy.transform.position, timePassed / moveDuration);
            }

            if (timePassed > moveDuration)
            {
                if (!attacked)
                {
                    Enemy.GetComponent<Enemy>().hp -= 2; 
                }
                else
                {
                    attacking = false;
                    yield break;
                }

                timePassed = 0f;
            }
            yield return 0;
            timePassed += Time.deltaTime;
        }
    }


    private void endTurn()
    {
        BattleStateManager.currentState = BattleStateManager.BattleStates.EnemyTurn;
        turnRoutine = null;
        timePassed = 0;
    }

    private void printAction(string statement)
    {
        if (debug_printAction)
            print(statement);
    }
}
