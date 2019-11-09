using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField] CharacterController characterController;

  [SerializeField] float speed;
  [SerializeField] float normalSpeed;
  [SerializeField] float accelerationMultiplier = 1;
  [SerializeField] float maxAccelerationMultiplier = 2;
  [SerializeField] float timeUpdater = 0.8f;

  [SerializeField] float movementRestriction;

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
    {
      accelerationMultiplier = 1;
    }
    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
    {
      print("keydown");
      accelerationMultiplier += Time.deltaTime * timeUpdater;
      accelerationMultiplier = Mathf.Clamp(accelerationMultiplier, 1, maxAccelerationMultiplier);
    }
    speed = normalSpeed;

    float x = Input.GetAxis("Horizontal");
    float z = Input.GetAxis("Vertical");

    Vector3 moveForward = transform.right * x;
    Vector3 moveRight = transform.forward * z;

    if ((Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") > 0) || (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") < 0) || (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Vertical") < 0) || (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Vertical") > 0))
    {
      x /= movementRestriction;
      z /= movementRestriction;
      speed /= movementRestriction;
    }
    else
    {
      speed = normalSpeed;
    }

    characterController.Move(moveForward * Time.deltaTime * speed * accelerationMultiplier);
    characterController.Move(moveRight * Time.deltaTime * speed * accelerationMultiplier);
  }
}
