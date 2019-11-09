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
  GameObject flowerSpawnPoint;

  public void SpawnFlower(GameObject flowerPrefab)
  {
    int i = Random.Range(0, flowerPointList.Count);
    int j = Random.Range(0, flowerPointList.Count);
    if (i <= 0)
    {
      print("no more places to spawn a flower");
      return;
    }
    Instantiate(flowerPrefab, flowerPointList[i].transform.position, Quaternion.identity);
    flowerPointList.Remove(flowerPointList[i]);
  }

  private void Start()
  {
    flowerSpawnPoints = new GameObject[xRange, yRange];
    SpawnFlowerPoints();
  }

  private void SpawnFlowerPoints()
  {
    for (int i = 0; i < xRange; i++)
    {
      for (int j = 0; j < yRange; j++)
      {
        flowerSpawnPoints[i, j] = flowerSpawnPointPrefab;
        flowerSpawnPoint = Instantiate(flowerSpawnPoints[i, j], new Vector3(i * xspawnDistance, 0, j * zspawnDistance), Quaternion.identity);
        print("adding " + flowerSpawnPoint);
        flowerPointList.Add(flowerSpawnPoint.GetComponent<FlowerSpawnPoint>());
      }
    }
  }
}