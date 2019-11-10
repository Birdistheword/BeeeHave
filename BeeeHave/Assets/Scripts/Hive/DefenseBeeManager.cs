using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBeeManager : MonoBehaviour
{
  [SerializeField] GameObject GuardbeePrefab;
  [SerializeField] Transform[] SpawnPoints;

  //[HideInInspector]
  [SerializeField] private int amountOfCurrentBees = 0;
  private int SpCounter = 0, children;
  private GameStates GS;

  void Start()
  {

    GS = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<GameStates>();

  }

  private void Update()
  {

        print("Amount of bees:" + amountOfCurrentBees);
        print("SpCpounter:" + SpCounter);
    if (Input.GetKeyDown(KeyCode.L))
    {
      SpawnGuardBee();
    }

    if (GS.STATE == GameStates.GameState.BearAtHive && amountOfCurrentBees > 0)
    {
      //Changes behavious of guard bees
      GS.STATE = GameStates.GameState.ThereIsGuardBees;
      print("There is guard bees");
    }

  }

  public void SpawnGuardBee()
  {
    if (amountOfCurrentBees < 5)
    {
      GameObject Bee = Instantiate(GuardbeePrefab, SpawnPoints[SpCounter].position, Quaternion.identity);
      if (Bee != null)
      {
        SpCounter++;
        amountOfCurrentBees++;
      }

    }

  }

  public int GetBeeNumber()
  {
    return amountOfCurrentBees;
  }

  public void BeeDied()
  {
    if (amountOfCurrentBees > 0)
      amountOfCurrentBees = amountOfCurrentBees - 1;
    print("Amount of current bees: " + amountOfCurrentBees);
    if (SpCounter > 0) SpCounter = SpCounter - 1;
    print("SpCounter: " + SpCounter);
  }
}
