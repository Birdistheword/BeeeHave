using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatProgression", menuName = "Stats/NewStatProgression", order = 0)]
public class StatProgression : ScriptableObject
{
  [SerializeField] ProgressionStat[] progressionStats = null;

  Dictionary<BeeStat, int[]> statTable = null;
  Dictionary<string, int> statLevel;

  public int GetStatValue(BeeStat stat, int statLevel)
  {
    BuildLookUp();
    int[] levels = statTable[stat];
    if (levels.Length < statLevel)
    {
      return 0;
    }
    return levels[statLevel - 1];
  }

  private void BuildLookUp()
  {
    if (statTable != null) { return; }

    statTable = new Dictionary<BeeStat, int[]>();

    foreach (ProgressionStat progressionStat in progressionStats)
    {
      statTable[progressionStat.stat] = progressionStat.levels;
    }
  }

  [System.Serializable]
  class ProgressionStat
  {
    public BeeStat stat;
    public int[] levels;
  }
}
