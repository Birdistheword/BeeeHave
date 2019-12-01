using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Moto.Control
{
  public class BikeController2D : MonoBehaviour
  {
    [SerializeField] WheelJoint2D backWheel;
    [SerializeField] WheelJoint2D frontWheel;

    [SerializeField] float rotationSpeed = 15f;
    [SerializeField] float speed = 1500f;

    [SerializeField] float maxSpeed;

    [SerializeField] Rigidbody2D rigidBody;

    [SerializeField] bool usingFrontWheel;
    [SerializeField] bool usingBackWheel;

    JointMotor2D motor;

    float rotation = 0f;
    float movement = 0f;


    void Update()
    {
      movement = Input.GetAxisRaw("Vertical") * speed;
      rotation = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
      ProcessMovement();
      ProcessRotation();
    }

    private void ProcessRotation()
    {
      rigidBody.AddTorque(-rotation * rotationSpeed * Time.fixedDeltaTime);
    }

    private void ProcessMovement()
    {
      WheelRotation(frontWheel, usingFrontWheel);
      WheelRotation(backWheel, usingBackWheel);
    }

    private void WheelRotation(WheelJoint2D wheel, bool usingWheel)
    {
      if (movement == 0)
      {
        wheel.useMotor = false;
      }
      else if (usingWheel)
      {
        wheel.useMotor = true;

        motor = new JointMotor2D() { motorSpeed = movement, maxMotorTorque = maxSpeed };
        wheel.motor = motor;
      }
    }
  }
}
