using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenManager : MonoBehaviour
{
  [SerializeField] float pollen;

  public float GetPollenCount()
  {
    return pollen;
  }

  public void AddPollen(float pollenAmount)
  {
    pollen += pollenAmount;
  }
}
