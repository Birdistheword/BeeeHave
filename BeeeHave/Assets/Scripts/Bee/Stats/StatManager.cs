using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
  Dictionary<BeeStat, int> statLevel = new Dictionary<BeeStat, int>()
  {
  {BeeStat.speedStat , 1},
  {BeeStat.efficiencyStat , 1},
  {BeeStat.pollenCarryStat , 1 }
  };

  [SerializeField] int speedStatLevel;
  [SerializeField] int efficiencyStatLevel;
  [SerializeField] int PollenCarryStatLevel;

  [SerializeField] int pollenEfficiencyStatValue = 2;

  [SerializeField] int pollenCarryStatValue = 2;

  [SerializeField] int speedStatValue = 1;

  [SerializeField] StatProgression statProgression;

  PlayerMovementSimple playerMovementSimple;
  PollenManager pollenManager;

  private void Update()
  {
    speedStatLevel = statLevel[BeeStat.speedStat];
    efficiencyStatLevel = statLevel[BeeStat.efficiencyStat];
    PollenCarryStatLevel = statLevel[BeeStat.pollenCarryStat];
  }

  private void Start()
  {
    playerMovementSimple = GetComponent<PlayerMovementSimple>();
    pollenManager = GetComponent<PollenManager>();
  }

  public void AddSpeedStat()
  {
    speedStatLevel++;
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

  public void AddStat(BeeStat stat)
  {
    statLevel[stat]++;
    IncreaseStat(stat);
  }

  public void IncreaseStat(BeeStat stat)
  {
    if (stat == BeeStat.speedStat)
    {
      playerMovementSimple.AddSpeed(speedStatValue);
    }
    if (stat == BeeStat.efficiencyStat)
    {
      pollenManager.IncreasePollenEfficiency(pollenEfficiencyStatValue);
    }
    if (stat == BeeStat.pollenCarryStat)
    {
      pollenManager.IncreaseMaxAmountOfPollen(pollenCarryStatValue);
    }
  }

  public int GetStatLevel(BeeStat stat)
  {
    return statLevel[stat];
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

  public void ResetStats()
  {
    speedStatLevel = 1;
    efficiencyStatLevel = 1;
    PollenCarryStatLevel = 1;
  }
}
