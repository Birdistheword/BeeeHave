using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : MonoBehaviour
{
    [SerializeField]
    Transform BearTarget;
    [SerializeField]float movSpeed = 3f;
<<<<<<< HEAD
    [SerializeField] GameObject Hive;


    [HideInInspector]
    public bool startAttacking = false, isAttacking = false, didDamage = false;
    private bool firstAttack = true;

    private int[] HealthPool = { 1, 1, 2, 2, 2, 3, 3, 3, 4 };
    private int currentHealth;
    private GameStates GS;
=======
    [HideInInspector]
    public bool startAttacking = false, isAttacking = false;
>>>>>>> b5d1eb37b54ca3fa7783d607ccb0b935549698ae

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GS.STATE == GameStates.GameState.BearStartAttack) StartAttack();

        if(isAttacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, BearTarget.position, movSpeed * Time.deltaTime);
<<<<<<< HEAD

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



=======
            Debug.Log("IsAttacking");
        }
>>>>>>> b5d1eb37b54ca3fa7783d607ccb0b935549698ae
    }

    private void StartAttack()
    {
<<<<<<< HEAD
        GS.STATE = GameStates.GameState.BearMovingToHive;
        SetHealth();
=======
        Debug.Log("StartAttacking");
        startAttacking = false;
>>>>>>> b5d1eb37b54ca3fa7783d607ccb0b935549698ae
        isAttacking = true;

    }
}
