using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    public GameObject Enemy;
    public float movementDuration = 3f;
    public static bool attacking = false;

    private bool attacked = false;
    private float timePassed = 0f;
    private Vector3 originalPosition;

    // Use this for initialization
    void Start () {
        originalPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
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
