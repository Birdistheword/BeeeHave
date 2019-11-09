using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{
  bool playerisIn;

  private void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      playerisIn = true;
    }
  }



  private void OnTriggerExit(Collider other)
  {
    if (other.tag == "Player")
    {
      playerisIn = false;
    }
  }
}
