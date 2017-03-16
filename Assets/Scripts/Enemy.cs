using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float hp = 3;
	public float movementDuration = 3f;
	public int chargeDamage = 999;
	public int normalDamage = 1;
	public int strongDamage = 2;
	public static bool myTurn = false;
	public GameObject Player;

	private bool choseAction = false;
	private bool debug_printAction = true;
	private delegate IEnumerator enemyAction();
	private float timePassed = 0f;
	private Color defaultColor;
	private IEnumerator turnRoutine = null;
	private List<enemyAction> enemyActions;
	private Renderer meshRend;
	private Vector3 originalPosition;




	// Use this for initialization
	void Start () {
		originalPosition = transform.position;
		meshRend = GetComponent<Renderer>();
		defaultColor = meshRend.material.color;

		enemyActions = new List<enemyAction> { Attack, StrongAttack, ChargeAttack };
	}
	
	// Update is called once per frame
	void Update () {
		if (myTurn && turnRoutine == null) {

			turnRoutine = enemyActions[Random.Range(0, enemyActions.Count)]();

			StartCoroutine(turnRoutine);
		}
	}

	public IEnumerator Attack() {
		printAction("Normal Attack");

		StartCoroutine(abstractMeleeAttackPlayer(movementDuration, normalDamage));

		yield return new WaitForSeconds(movementDuration * 2); // * 2 because it's there and back
		yield return 0; // Just to make sure we don't endTurn() too quickly
		endTurn();

	}

	public IEnumerator StrongAttack() {
		printAction("Strong Attack");

		float duration = movementDuration / 5f;

		StartCoroutine(abstractMeleeAttackPlayer(duration, strongDamage));

		yield return new WaitForSeconds(duration * 2); // * 2 because it's there and back
		yield return 0; // Just to make sure we don't endTurn() too quickly
		endTurn();

	}

	public IEnumerator ChargeAttack() {
		printAction("Charge Attack");

		bool charging = false;
		bool attacked = false;


		while (!attacked) {
			if (!charging) {

				meshRend.material.color = new Color(255, 0, 128);
				myTurn = false;
				charging = true;
				BattleStateManager.currentState = BattleStateManager.BattleStates.PlayerTurn;

			} else if (myTurn) { // myTurn again
				float duration = movementDuration / 5f;
				StartCoroutine(abstractMeleeAttackPlayer(duration, chargeDamage));
				yield return new WaitForSeconds(duration);
				yield return 0;
				break;
			}
			yield return 0;
		}
		meshRend.material.color = defaultColor;
		endTurn();
	}



	//Stuff
	private IEnumerator abstractMeleeAttackPlayer(float moveDuration, int damage) {
		bool attacking = true;
		bool attacked = false;
		timePassed = 0;

		while (attacking) {
			//print(timePassed + " | " + moveDuration + " | " + attacking + " | " + attacked);

			if (attacked) {
				transform.position = Vector3.Lerp(Player.transform.position, originalPosition, timePassed / moveDuration);
			}
			else {
				transform.position = Vector3.Lerp(originalPosition, Player.transform.position, timePassed / moveDuration);
			}

			if (timePassed > moveDuration) {
				if (!attacked) {
					Player.GetComponent<Player>().hp -= damage;
					attacked = true;
				}
				else {
					attacking = false;
					yield break;
				}

				timePassed = 0f;
			}
			yield return 0;
			timePassed += Time.deltaTime;
		}
	}



	private void endTurn() {
		myTurn = false;
		BattleStateManager.currentState = BattleStateManager.BattleStates.PlayerTurn;
		turnRoutine = null;
		timePassed = 0;
	}

	private void printAction(string statement) {
		if (debug_printAction)
			print(statement);
	}

}
