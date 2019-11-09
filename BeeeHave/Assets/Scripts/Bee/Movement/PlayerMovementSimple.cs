using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSimple : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float moveSpeed, dashSpeed, dashTime, rotationSpeed;

    private float hSpeed, vSpeed;
    private bool dashing = false;

    private void Update()
    {
        hSpeed = Input.GetAxis("Horizontal");
        vSpeed = Input.GetAxis("Vertical");

        if(Input.GetKeyDown("space"))
        {
            Dash();
        }
        
    }



    void FixedUpdate()
    {
        MoveBee();
    }



    private void MoveBee ()
    {
        if (!dashing)
        {
            Debug.Log("Moving");
            float hMove, vMove;

            hMove = hSpeed * moveSpeed * Time.fixedDeltaTime * 100f;
            vMove = vSpeed * moveSpeed * Time.fixedDeltaTime * 100f;

            Vector3 Movement = new Vector3(hMove, 0f, vMove);

            rb.velocity = Movement;

            if (Movement != Vector3.zero)
                transform.forward = Vector3.Lerp(transform.forward, Movement, rotationSpeed * Time.fixedDeltaTime);
        }
    }


    private void Dash()
    {
        dashing = true;

        Vector3 normalizedSpeed = rb.velocity.normalized;

        rb.velocity = normalizedSpeed * dashSpeed;
        StartCoroutine(StopDash());
    }

    private IEnumerator StopDash()
    {
        yield return new WaitForSeconds(dashTime);

        rb.velocity = Vector3.zero;
        dashing = false;
    }

}
