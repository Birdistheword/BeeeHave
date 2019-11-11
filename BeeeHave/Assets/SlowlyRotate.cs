using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowlyRotate : MonoBehaviour
{
  [SerializeField] float rotationSpeed = 0.5f;
  [SerializeField] float maxAltitudeChange = 2f;

  void Update()
  {
    transform.Rotate(0, rotationSpeed, 0);
  }
}
