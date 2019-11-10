﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSimple : MonoBehaviour
{
  [SerializeField] Rigidbody rb;
  [SerializeField] float moveSpeed, dashSpeed, dashTime, rotationSpeed, dashCooldown = 2f;
  private Vector3 Move;

  private float hSpeed, vSpeed, dashCd = 0f;
  private bool dashing = false;

  [SerializeField] float speedStatValue = 2f;
  [SerializeField] float speedStatApplication;

  StatManager statManager;

  private void Start()
  {
    statManager = GetComponent<StatManager>();
  }

  private void Update()
  {
    speedStatApplication = statManager.GetSpeedStatLevel() * speedStatValue;
    Move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));



    if (Input.GetKeyDown("space"))
    {
      Dash();
    }

    // Count down dash cooldowntimer
    dashCd -= Time.deltaTime;
  }


  void LateUpdate()
  {
    MoveBee();
  }

  private void MoveBee()
  {
    if (!dashing)
    {
      //Get the Values from Input
      Move.x = Move.x * moveSpeed * Time.fixedDeltaTime * speedStatApplication;
      Move.z = Move.z * moveSpeed * Time.fixedDeltaTime * speedStatApplication;


      Vector3 Movement = new Vector3(Move.x, 0f, Move.z);

      //Fix for isometric Movement -> Rotate by 45 Degrees!
      if (Movement != Vector3.zero)
      {
        Movement = Quaternion.Euler(0f, 45f, 0f) * Move;
        //Debug.Log("Fixing rotation!");
      }

      rb.velocity = Movement;

      // Rotate Forwards
      if (Movement != Vector3.zero)
      {
        transform.forward = Vector3.Lerp(transform.forward, Movement, rotationSpeed * Time.fixedDeltaTime);
        //Debug.Log("Rotating");
      }

    }
  }


  private void Dash()
  {
    if (dashCd <= 0f)
    {
      //Stop Movement
      dashing = true;

      //Get move direction / velocity
      Vector3 normalizedSpeed = rb.velocity.normalized;

      // Increase velocity by dashSpeed
      rb.velocity = normalizedSpeed * dashSpeed;

      //Stop dash after dashTime runs out
      StartCoroutine(StopDash());

      dashCd = dashCooldown;
    }


  }

  private IEnumerator StopDash()
  {
    yield return new WaitForSeconds(dashTime);

    // Reset velocity -> since this happens in Update it will pick the right velocity immediatly in the same frame
    // On FixedUpdate right?
    rb.velocity = Vector3.zero;
    dashing = false;
  }


}
