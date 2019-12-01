using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moto.Control
{
  public class BikeController3D : MonoBehaviour
  {
    [SerializeField] float maxMotorSpeed = 1500;
    [SerializeField] WheelCollider backWheel;
    [SerializeField] WheelCollider frontWheel;

    [SerializeField] float motorMovement;
    [Range(-1, 1)] [SerializeField] float horizontal;

    private void Update()
    {
      horizontal = Input.GetAxisRaw("Horizontal");
      motorMovement = horizontal * maxMotorSpeed;
    }

    private void FixedUpdate()
    {
      backWheel.motorTorque = motorMovement;
      frontWheel.motorTorque = motorMovement;
    }
  }
}
