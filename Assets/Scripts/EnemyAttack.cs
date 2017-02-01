using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    public GameObject Player;
    public float movementDuration = 3f;
    public static bool attacking = false;

    private bool attacked = false;
    private float timePassed = 0f;
    private Vector3 originalPosition;

    // Use this for initialization
    void Start()
    {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            timePassed += Time.deltaTime;

            if (attacked)
            {
                transform.position = Vector3.Lerp(Player.transform.position, originalPosition, timePassed / movementDuration);
            }
            else
            {
                transform.position = Vector3.Lerp(originalPosition, Player.transform.position, timePassed / movementDuration);
            }

            if (timePassed > movementDuration)
            {
                if (attacked)
                {
                    attacking = false;
                }
                else
                {
                    Player.GetComponent<Player>().hp -= 1;
                }

                attacked = !attacked;
                timePassed = 0f;

                if (!attacking)
                {
                    BattleStateManager.currentState = BattleStateManager.BattleStates.PlayerTurn;
                }
            }
        }
    }
}
