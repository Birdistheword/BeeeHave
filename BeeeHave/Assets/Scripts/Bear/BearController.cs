using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : MonoBehaviour
{
    [SerializeField]
    Transform BearTarget;
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
        GS = GameObject.FindObjectOfType<GameStates>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GS.STATE == GameStates.GameState.BearStartAttack) StartAttack();

        if(isAttacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, BearTarget.position, movSpeed * Time.deltaTime);

            if(Vector3.Distance(BearTarget.position, transform.position) <= 1f)
            {
                GS.STATE = GameStates.GameState.BearAtHive;
            }
        }

        if (GS.STATE == GameStates.GameState.DamagePhase && didDamage == false)
        {
            // Damage the Hive
            didDamage = true;
            Hive.GetComponent<Hive>().health -= currentHealth;
            
        }



    }

    private void StartAttack()
    {
        GS.STATE = GameStates.GameState.BearMovingToHive;
        SetHealth();
        isAttacking = true;
        Debug.Log(currentHealth);

    }

    private void SetHealth ()
    {
        if (firstAttack)
        {
            currentHealth = 1;
            firstAttack = false;
        }
           
        else
            currentHealth = HealthPool[Random.Range(0, HealthPool.Length)];
    }

    public void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
    }

}
