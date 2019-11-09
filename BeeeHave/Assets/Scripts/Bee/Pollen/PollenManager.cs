using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenManager : MonoBehaviour
{
  [SerializeField] int pollen;

  public int GetPollenCount()
  {
    return pollen;
  }

  public void AddPollen(int pollenAmount)
  {
    pollen += pollenAmount;
  }
}
