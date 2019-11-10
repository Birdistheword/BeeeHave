using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenManager : MonoBehaviour
{
  [SerializeField] int pollen;

  [SerializeField] int pollenEfficiencyStatValue = 2;
  [SerializeField] int pollenEfficiencyApplication;

  [SerializeField] int pollenCarryStatValue = 2;
  [SerializeField] int pollenCarryStatApplication;

  [SerializeField] int maxAmountOfPollen;
  [SerializeField] int startMaxAmountOfPollen;

  StatManager statManager;

  private void Start()
  {
    statManager = GetComponent<StatManager>();
  }

  private void Update()
  {
    pollenCarryStatApplication = pollenCarryStatValue * statManager.GetCarryStatLevel();
    print(statManager.GetCarryStatLevel());
    maxAmountOfPollen = pollenCarryStatApplication + startMaxAmountOfPollen;
    pollenEfficiencyApplication = pollenEfficiencyStatValue * statManager.GetEfficiencyStatLevel();
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
    pollen += pollenAmount + pollenEfficiencyApplication;
  }

  public void RemovePollen(int pollenAmount)
  {
    pollen -= pollenAmount;
  }
}
