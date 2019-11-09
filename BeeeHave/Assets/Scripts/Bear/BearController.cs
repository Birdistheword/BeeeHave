using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : MonoBehaviour
{
    [SerializeField]
    Transform BearTarget;
    [SerializeField]float movSpeed = 3f;


    [HideInInspector]
    public bool startAttacking = false, isAttacking = false;
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
        if (startAttacking) StartAttack();

        if(isAttacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, BearTarget.position, movSpeed * Time.deltaTime);

            if(Vector3.Distance(BearTarget.position, transform.position) <= 1f)
            {
                if(currentHealth <= 0)
                {

                }
            }
        }

       
    }

    private void StartAttack()
    {
        SetHealth();
        startAttacking = false;
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
