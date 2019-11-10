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
    private bool firstAttack = true, hasRetreated = false;

    private int[] HealthPool = { 1, 2, 2, 2, 3, 3, 3, 4 };
    private int currentHealth = 1;
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

    private IEnumerator WaitForBeeAttack()
    {
        //Wait to check if there is defense bees
        yield return new WaitForSeconds(3f);

        if (GS.STATE == GameStates.GameState.BearAtHive)
        {
            GS.STATE = GameStates.GameState.DamagePhase;
            print("Damage phase");
        }

        yield return new WaitForSeconds(2f);

        
    }

    private void Damage()
    {
        if (GS.STATE == GameStates.GameState.DamagePhase)
        {
            // Damage the Hive
            GS.STATE = GameStates.GameState.DidDamage;
            print("did damage");
            Hive.GetComponent<Hive>().TakeDamage(currentHealth);
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
            return;
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

        // Retreat if no health
        if (GS.STATE != GameStates.GameState.Idle && currentHealth <= 0 && !hasRetreated)
        { 
            GS.STATE = GameStates.GameState.BearRetreat;
            transform.position = BearStartPos.position;
            print("BearRetreat because of Damage");
            hasRetreated = true;
        }

        if (GS.STATE == GameStates.GameState.Idle && hasRetreated) hasRetreated = false;


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

    public int GetHealth()
    {
        return currentHealth;
    }

}
