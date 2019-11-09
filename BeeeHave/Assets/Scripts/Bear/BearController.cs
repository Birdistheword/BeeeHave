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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startAttacking) StartAttack();

        if(isAttacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, BearTarget.position, movSpeed * Time.deltaTime);
            Debug.Log("IsAttacking");
        }
    }

    private void StartAttack()
    {
        Debug.Log("StartAttacking");
        startAttacking = false;
        isAttacking = true;

    }
}
