using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public bool attacking = false;
    public float hp = 3;
	public float movementDuration = 3f;
	public GameObject Enemy;

	private bool attacked = false;
	private float timePassed = 0f;
	private Vector3 originalPosition;

	// Use this for initialization
	void Start () {
		originalPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		playerAttack ();
	}

	void playerAttack() {
		if (attacking)
		{
			timePassed += Time.deltaTime;

			if (attacked)
			{
				transform.position = Vector3.Lerp(Enemy.transform.position, originalPosition, timePassed / movementDuration);
			} else
			{
				transform.position = Vector3.Lerp(originalPosition, Enemy.transform.position, timePassed / movementDuration);
			}

			if (timePassed > movementDuration)
			{
				if (attacked)
				{
					attacking = false;
				} else
				{
					Enemy.GetComponent<Enemy>().hp -= 1;
				}

				attacked = !attacked;
				timePassed = 0f;

				if (!attacking)
				{
					BattleStateManager.currentState = BattleStateManager.BattleStates.EnemyTurn;
				}
			}
		}
	}
}
