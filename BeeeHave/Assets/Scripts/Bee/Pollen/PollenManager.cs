using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenManager : MonoBehaviour
{
  [SerializeField] int pollen;

  [SerializeField] int maxAmountOfPollen;
  [SerializeField] int startMaxAmountOfPollen;

  [SerializeField] int pollenEfficiency = 0;

  StatManager statManager;

  private void Start()
  {
    statManager = GetComponent<StatManager>();
  }

  private void Update()
  {
    if (pollen > maxAmountOfPollen)
    {
      pollen = maxAmountOfPollen;
    }
    if (pollen < 0)
    {
      pollen = 0;
    }
  }

  public int GetPollenCount()
  {
    return pollen;
  }

  public void AddPollen(int pollenAmount)
  {
    pollen += pollenAmount /*+ pollenEfficiencyApplication*/;
  }

  public void RemovePollen(int pollenAmount)
  {
    pollen -= pollenAmount;
  }

  public void IncreaseMaxAmountOfPollen(int pollenAmountIncreaser)
  {
    maxAmountOfPollen += pollenAmountIncreaser;
  }

  public int GetMaxPollenAmount()
  {
    return maxAmountOfPollen;
  }

  public void IncreasePollenEfficiency(int pollenEffciencyAmountIncrease)
  {
    pollenEfficiency += pollenEffciencyAmountIncrease;
  }
}
