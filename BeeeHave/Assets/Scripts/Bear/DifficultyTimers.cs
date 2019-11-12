using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyTimers", menuName = "BeeeHave/DifficultyTimers", order = 0)]
public class DifficultyTimers : ScriptableObject
{
  [SerializeField] TimerDifficulyClass[] timers;

  public Dictionary<DifficultyLevels, int[]> timerTable = null;
  public Dictionary<DifficultyLevels, int[]> bearHealthTable = null;

  public void BuildTables()
  {
    if (timerTable == null && bearHealthTable == null) { return; }
    foreach (TimerDifficulyClass timer in timers)
    {
      int[] temporaryArray = null;
      int i = 0;
      foreach (int timerPerLevel in timer.timerPerLevel)
      {
        temporaryArray = new int[timer.timerPerLevel.Length];
        temporaryArray[i++] = timerPerLevel;
        timerTable[timer.difficultyLevels] = temporaryArray;
        Debug.Log("timertable - " + timerTable[timer.difficultyLevels]);
      }
      i = 0;
      temporaryArray = null;
      foreach (int bearHealth in timer.bearHealth)
      {
        temporaryArray = new int[timer.bearHealth.Length];
        temporaryArray[i++] = bearHealth;
        bearHealthTable[timer.difficultyLevels] = temporaryArray;
      }
    }
  }

  [System.Serializable]
  class TimerDifficulyClass
  {
    public DifficultyLevels difficultyLevels;
    public int[] timerPerLevel;
    public int[] bearHealth;
  }
}