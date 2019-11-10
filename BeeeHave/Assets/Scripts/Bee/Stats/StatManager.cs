using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
  [SerializeField] int speedStatLevel = 1;
  [SerializeField] int efficiencyStatLevel = 1;
  [SerializeField] int PollenCarryStatLevel = 1;

  [SerializeField] int pollenEfficiencyStatValue = 2;

  [SerializeField] int pollenCarryStatValue = 2;

  PlayerMovementSimple playerMovementSimple;
  PollenManager pollenManager;

  private void Start()
  {
    playerMovementSimple.GetComponent<PlayerMovementSimple>();
    pollenManager.GetComponent<PollenManager>();
  }

  public void AddSpeedStat()
  {
    speedStatLevel++;
    //playerMovementSimple.
  }

  public void AddEfficiencyStat()
  {
    efficiencyStatLevel++;
    pollenManager.IncreasePollenEfficiency(pollenEfficiencyStatValue);
  }

  public void AddCarryStat()
  {
    PollenCarryStatLevel++;
    pollenManager.IncreaseMaxAmountOfPollen(pollenCarryStatValue);
  }

  public int GetSpeedStatLevel()
  {
    return speedStatLevel;
  }

  public int GetEfficiencyStatLevel()
  {
    return efficiencyStatLevel;
  }

  public int GetCarryStatLevel()
  {
    return PollenCarryStatLevel;
  }
}
