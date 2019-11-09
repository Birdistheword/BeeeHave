using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBeeController : MonoBehaviour
{
    [SerializeField] GameObject BearPrefab;
    private bool isAttacking = false,stoppedMoving = false;
    private Vector3 startPos;
    [SerializeField] int damage = 1;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {

        // TO DO : MAke bee fly

        /*if (!isAttacking)
        {
            Vector3 circle = startPos + new Vector3(Random.insideUnitCircle.x, 0f, Random.insideUnitCircle.y);
            transform.position = Vector3.MoveTowards(transform.position,circle, 3f);
        }*/
    }

    public void HandleBearAttack()
    {
        isAttacking = true;
        // Animate to attack

        transform.position = Vector3.MoveTowards(transform.position, BearPrefab.transform.position, 3f);

        // Deal Damage
        BearPrefab.GetComponent<BearController>().TakeDamage(damage);

        // Die and clear counter on BeehiveManager
    }
}
