using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : MonoBehaviour
{
    [SerializeField]
    Transform BearTarget, BearDamageTarget, BearStartPos;
    [SerializeField]float movSpeed = 3f;
    [SerializeField] GameObject Hive;


    [HideInInspector]
    public bool startAttacking = false, isAttacking = false, didDamage = false;
    private bool firstAttack = true;

    private int[] HealthPool = { 1, 1, 2, 2, 2, 3, 3, 3, 4 };
    private int currentHealth;
    private GameStates GS;

    void Start()
    {
        GS = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<GameStates>();
    }




    private void MoveToHive()
    {
        if (GS.STATE == GameStates.GameState.BearMovingToHive)
            transform.position = Vector3.MoveTowards(transform.position, BearTarget.position, movSpeed * Time.deltaTime);
    }

    private void WhenAtHive()
    {
        if (GS.STATE == GameStates.GameState.BearMovingToHive && Vector3.Distance(BearTarget.position, transform.position) <= 1f)
        {
            GS.STATE = GameStates.GameState.BearAtHive;
            print("Bear at hive");

            


            StartCoroutine(WaitForBeeAttack());
        }
    }

    private void Damage()
    {
        // When Bear is at Hive, Check if it has 0 HP and if it does, RETREAT
        if (GS.STATE == GameStates.GameState.BearAtHive && currentHealth <= 0)
        {
            GS.STATE = GameStates.GameState.BearRetreat;
        }

        // Otherwhise, Damage the Hive with current health

        else if (GS.STATE == GameStates.GameState.DamagePhase)
        {
            // Damage the Hive
            GS.STATE = GameStates.GameState.DidDamage;
            print("did damage");
            Hive.GetComponent<Hive>().TakeDamage(currentHealth);

            // Need Attack Animation
            

        }
        else if (GS.STATE == GameStates.GameState.DidDamage)
        {
            transform.position = Vector3.MoveTowards(transform.position, BearDamageTarget.position, movSpeed * Time.deltaTime);
        }
    }

    private void Retreat()
    {
        //Then Go back -> Retreat
        if (GS.STATE == GameStates.GameState.DidDamage&& Vector3.Distance(transform.position, BearDamageTarget.position) <= 1f)
        {
            transform.position = BearStartPos.position;
            GS.STATE = GameStates.GameState.BearRetreat;
            print("BearRetreat");
            
        }

        if (GS.STATE == GameStates.GameState.BearRetreat && Vector3.Distance(transform.position, BearStartPos.position) <= 1f)
        {
            GS.STATE = GameStates.GameState.ResetTimer;
            print("ResetTimer");
        }
    }

    // Update is called once per frame
    void Update()
    {

        // At Start, Check for Start Attack
        if (GS.STATE == GameStates.GameState.BearStartAttack)
        {
            print("Start attack");
            StartAttack();
        }

        MoveToHive();
        WhenAtHive();
        Damage();
        Retreat();       

        
    }


    //Set attacking to true and let bear approach Hive
    private void StartAttack()
    {
        if(GS.STATE == GameStates.GameState.BearStartAttack)
        {
            GS.STATE = GameStates.GameState.BearMovingToHive;
            SetHealth();
            isAttacking = true;
        }
    }

    private IEnumerator WaitForBeeAttack()
    {
        yield return new WaitForSeconds(2f);
        GS.STATE = GameStates.GameState.DamagePhase;
        print("Damage phase");
    }

    //Set Health, 
    // First time -> 1
    // Next Times Random from Array
    private void SetHealth ()
    {
        if (firstAttack)
        {
            currentHealth = 1;
            firstAttack = false;
        }
           
        else
            currentHealth = HealthPool[Random.Range(0, HealthPool.Length)];

        print("CurrenBearHealth:" + currentHealth);
    }

    // This method is called from GuardBeeController and the Player
    public void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
    }

}
