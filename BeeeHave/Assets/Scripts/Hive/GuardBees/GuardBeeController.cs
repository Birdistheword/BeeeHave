using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBeeController : MonoBehaviour
{
    [SerializeField] GameObject BearPrefab, BeeManager;
    private bool isAttacking = false;
    private Vector3 startPos;
    [SerializeField] int damage = 1;

    private GameStates GS;

    /*private void Start()
    {
        startPos = transform.position;
        GS = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<GameStates>();

        BearPrefab = GameObject.FindGameObjectWithTag("Bear");
    }

    private void Update()
    {
        if(GS.STATE == GameStates.GameState.ThereIsGuardBees)
        {
            MoveToBear();
        }

        AttackBear();

        if(GS.STATE != GameStates.GameState.ThereIsGuardBees )
        {
            transform.position = startPos;
        }

    }

    public void MoveToBear()
    {
        
        // Animate to attack
        transform.position = Vector3.MoveTowards(transform.position, BearPrefab.transform.position, 1f);

        if (Vector3.Distance(transform.position, BearPrefab.transform.position) <= 0.4f)
        {
            GS.STATE = GameStates.GameState.GuardBeesAttacking;
        }

    }

    private void AttackBear()
    {
        if (GS.STATE == GameStates.GameState.GuardBeesAttacking)
        {
            GS.STATE = GameStates.GameState.DamagePhase;
            BearPrefab.GetComponent<BearController>().TakeDamage(damage);
            BeeManager.GetComponent<DefenseBeeManager>().BeeDied();
            print("Bee died event");
            Destroy(gameObject);
        }
        
    }
    */

}
