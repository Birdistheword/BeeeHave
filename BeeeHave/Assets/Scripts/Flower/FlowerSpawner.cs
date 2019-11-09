using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FlowerSpawner : MonoBehaviour
{
  [SerializeField] int xRange = 100;
  [SerializeField] int yRange = 100;

  [SerializeField] int xspawnDistance;
  [SerializeField] int zspawnDistance;

  [SerializeField] GameObject flowerSpawnPointPrefab;
  [SerializeField] GameObject[,] flowerSpawnPoints;
  [SerializeField] List<FlowerSpawnPoint> flowerPointList;
  [SerializeField] GameObject flowerPrefab;
  [SerializeField]
  GameObject flowerSpawnPoint;

  int flowerCount;
  bool flowersMaxed = false;

  public void SpawnFlower(GameObject flowerPrefab)
  {
    int i = Random.Range(-1, flowerPointList.Count);
    if (i < 0)
    {
      return;
    }
    Instantiate(flowerPrefab, flowerPointList[i].transform.position, Quaternion.identity);
    flowerCount++;
    if (flowerCount == xRange * yRange)
    {
      print("max Amount of flowers reached");
      flowersMaxed = true;
    }
    flowerPointList.Remove(flowerPointList[i]);
  }

  public bool FlowersMaxed()
  {
    return flowersMaxed;
  }

  public int GetFlowerCount()
  {
    return flowerCount;
  }

  private void Start()
  {
    flowerSpawnPoints = new GameObject[xRange, yRange];
    SpawnFlowerPoints();
    for (int i = 0; i < 3; i++)
    {
      SpawnFlower(flowerPrefab);
    }
  }

  private void SpawnFlowerPoints()
  {
    for (int i = 0; i < xRange; i++)
    {
      for (int j = 0; j < yRange; j++)
      {
        flowerSpawnPoints[i, j] = flowerSpawnPointPrefab;
        flowerSpawnPoint = Instantiate(flowerSpawnPoints[i, j], new Vector3(i * xspawnDistance, 0, j * zspawnDistance), Quaternion.identity);
        flowerPointList.Add(flowerSpawnPoint.GetComponent<FlowerSpawnPoint>());
      }
    }
  }
}