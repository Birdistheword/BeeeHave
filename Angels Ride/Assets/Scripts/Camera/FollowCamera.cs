using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moto.Camera
{
  public class FollowCamera : MonoBehaviour
  {
    [SerializeField] Transform targetTransform;
    [SerializeField] Vector3 offset;
    [SerializeField] Quaternion cameraRotation;

    private void LateUpdate()
    {
      transform.position = new Vector3(targetTransform.position.x, targetTransform.position.y, transform.position.z) + offset;
      // transform.rotation = cameraRotation;
    }
  }
}