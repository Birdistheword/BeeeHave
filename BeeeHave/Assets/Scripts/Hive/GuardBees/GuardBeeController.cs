using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBeeController : MonoBehaviour
{
    [SerializeField] GameObject BearPrefab;
    private bool isAttacking = false;
    private Vector3 startPos;
    [SerializeField] int damage = 1;

    private GameStates GS;

    private void Start()
    {
        startPos = transform.position;
        GS = GameObject.FindObjectOfType<GameStates>();
    }

    private void Update()
    {
        if(GS.STATE == GameStates.GameState.BearAtHive)
        {
            HandleBearAttack();
        }

    }

    public void HandleBearAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            // Animate to attack

            transform.position = Vector3.MoveTowards(transform.position, BearPrefab.transform.position, 3f);

            // Deal Damage
            

            // Die and clear counter on BeehiveManager
        }
    }

    private IEnumerator WaitAndDamage()
    {
        yield return new WaitForSeconds(1.5f);
        BearPrefab.GetComponent<BearController>().TakeDamage(damage);
        GS.STATE = GameStates.GameState.DamagePhase;
        Destroy(gameObject);
    }
}
