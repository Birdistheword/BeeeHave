using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementSimple : MonoBehaviour
{
  [SerializeField] Rigidbody rb;
  [SerializeField] float moveSpeed, startSpeed, dashSpeed, dashTime, rotationSpeed, dashCooldown = 2f;

  [SerializeField] AudioSource dashSFX;
  [SerializeField] AudioSource moveSFX;

  [SerializeField] Image image;

  private Vector3 Move;

  private float hSpeed, vSpeed;
  [SerializeField] float dashCd = 0f;
  private bool dashing = false;


  StatManager statManager;

  private void Start()
  {
    statManager = GetComponent<StatManager>();
    startSpeed = moveSpeed;
  }

  private void Update()
  {
    Move = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

    if (!dashing)
    {
      image.fillAmount = (dashCooldown - dashCd) / dashCooldown;
    }


    if (Input.GetKeyDown(KeyCode.LeftShift))
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
      if (moveSFX) moveSFX.Play();

      //Get the Values from Input
      Move.x = Move.x * moveSpeed * Time.fixedDeltaTime;
      Move.z = Move.z * moveSpeed * Time.fixedDeltaTime;


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

      dashSFX.Play();
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

  public void AddSpeed(float addedSpeed)
  {
    moveSpeed += addedSpeed;
  }

  public void ResetSpeed()
  {
    moveSpeed = startSpeed;
  }

  private void OnCollisionEnter(Collision collision)
  {
    if (dashing)
    {
      if (collision.gameObject.tag == "Bear")
      {
        // WE STUNG THE BEAR
        collision.gameObject.GetComponent<BearController>().TakeDamage(4);

        // Reset the bee
        ResetSpeed();
      }
    }
  }
}
