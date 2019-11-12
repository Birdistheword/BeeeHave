using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBeeManager : MonoBehaviour
{
  [SerializeField] GameObject GuardbeePrefab, BearPrefab;
  [SerializeField] Transform[] SpawnPoints;
  private List<GameObject> AllGuardBees = new List<GameObject>();

  //[HideInInspector]
  [SerializeField] private int amountOfCurrentBees = 0;
  private int SpCounter = 0, children;
  private GameStates GS;
  private BearController BC;

  void Start()
  {
    GS = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<GameStates>();
    BC = GameObject.FindGameObjectWithTag("Bear").GetComponent<BearController>();
  }

  private void Update()
  {
    if (Debug.isDebugBuild)
    {
      if (Input.GetKeyDown(KeyCode.L))
      {
        SpawnGuardBee();
      }
    }

    if (GS.STATE == GameStates.GameState.BearAtHive && amountOfCurrentBees > 0)
    {
      //Changes behavious of guard bees
      GS.STATE = GameStates.GameState.ThereIsGuardBees;
    }

    HandleGuardBee();

  }

  public void SpawnGuardBee()
  {
    if (amountOfCurrentBees < 5)
    {
      GameObject Bee = Instantiate(GuardbeePrefab, SpawnPoints[SpCounter].position, Quaternion.identity);

      AllGuardBees.Add(Bee);


      if (Bee != null)
      {
        SpCounter++;
        amountOfCurrentBees++;
      }
    }
  }

  private void HandleGuardBee()
  {
    if (GS.STATE == GameStates.GameState.ThereIsGuardBees)
    {
      GS.STATE = GameStates.GameState.GuardBeesAttacking;
    }

    if (GS.STATE == GameStates.GameState.GuardBeesAttacking)
    {
      int bearHealth = BC.GetHealth();


      // Let the bees attack the bear
      for (int i = bearHealth; i > 0; i--)
      {
        // If there is more bees than the bear has health, then choose them form the right and update accordingly

        if (AllGuardBees.Count > 0)
        {
          AllGuardBees[AllGuardBees.Count - 1].transform.position = Vector3.MoveTowards(transform.position, BearPrefab.transform.position, 1f);
          BC.TakeDamage(1); // Fix magic number
          Destroy(AllGuardBees[AllGuardBees.Count - 1]);
          AllGuardBees.Remove(AllGuardBees[AllGuardBees.Count - 1]);
          SpCounter--;
          amountOfCurrentBees--;
        }

      }

      GS.STATE = GameStates.GameState.DamagePhase;
    }
  }



  /* public void BeeDied()
   {
       if (amountOfCurrentBees > 0)
           amountOfCurrentBees = amountOfCurrentBees - 1;
       print("Amount of current bees: " + amountOfCurrentBees);
       if (SpCounter > 0) SpCounter = SpCounter - 1;
       print("SpCounter: " + SpCounter);

   }*/

  public int GetBeeNumber()
  {
    return amountOfCurrentBees;
  }
}
