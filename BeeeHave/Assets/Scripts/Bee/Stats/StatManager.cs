using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
  [SerializeField] int speedStatLevel = 1;
  [SerializeField] int efficiencyStatLevel = 1;
  [SerializeField] int PollenCarryStatLevel = 1;

  public void AddSpeedStat()
  {
    speedStatLevel++;
  }

  public void AddEfficiencyStat()
  {
    efficiencyStatLevel++;
  }

  public void AddCarryStat()
  {
    PollenCarryStatLevel++;
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
