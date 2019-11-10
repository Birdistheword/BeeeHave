using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
  [SerializeField] Pollen pollenPrefab;

  [SerializeField] float pollenSpawnHeight = 2;

  bool pollenIsSpawned;

  float timeSinceSpawnedPollen = 0;
  [SerializeField] float pollenRespawnTimer;
  Animator animator;

  private void Start()
  {
    pollenIsSpawned = false;
    animator = GetComponentInChildren<Animator>();
  }

  private void Update()
  {
    if (pollenIsSpawned)
    {
      timeSinceSpawnedPollen = 0;
    }
    else
    {
      timeSinceSpawnedPollen += Time.deltaTime;
      if (timeSinceSpawnedPollen >= pollenRespawnTimer && !pollenIsSpawned)
      {
        animator.SetTrigger("flowerOpening");
        StartCoroutine(SpawnPollen());
      }
    }
  }

  private IEnumerator SpawnPollen()
  {
    yield return new WaitForSeconds(1);
    print("Spawning pollen");
    Instantiate(pollenPrefab, new Vector3(transform.position.x, transform.position.y + pollenSpawnHeight, transform.position.z), Quaternion.identity, transform);
  }

  public void SetPollenIsSpawned(bool isPollenSpawned)
  {
    pollenIsSpawned = isPollenSpawned;
    if (!pollenIsSpawned)
    {
      animator.SetTrigger("flowerClosing");
    }
  }

}
