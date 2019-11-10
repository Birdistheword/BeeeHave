using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBeeManager : MonoBehaviour
{
    [SerializeField] GameObject GuardbeePrefab, BearPrefab;
    [SerializeField] Transform[] SpawnPoints;
    private GameObject[] AllGuardBees;

    //[HideInInspector]
    [SerializeField] private int amountOfCurrentBees = 0;
    private int SpCounter = 0, children;
    private GameStates GS;
    private BearController BC;

    void Start()
    {

        GS = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<GameStates>();
        BC = BearPrefab.GetComponent<BearController>();


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

        HandleGuardBee();

    }

    public void SpawnGuardBee()
    {
        if (amountOfCurrentBees < 5)
        {
            GameObject Bee = Instantiate(GuardbeePrefab, SpawnPoints[SpCounter].position, Quaternion.identity);
            AllGuardBees[SpCounter] = Bee;

            if (Bee != null)
            {
            SpCounter++;
            amountOfCurrentBees++;
            }
        }
    }

    private void HandleGuardBee()
    {
        if(GS.STATE == GameStates.GameState.ThereIsGuardBees)
        {
            GS.STATE = GameStates.GameState.GuardBeesAttacking;
        }

        if(GS.STATE == GameStates.GameState.GuardBeesAttacking)
        {
            int bearHealth = BC.GetHealth();

            for (int i = 0; i <= bearHealth; i++)
            {
                AllGuardBees[i].transform.position = Vector3.MoveTowards(transform.position, BearPrefab.transform.position, 1f);

                if(Vector3.Distance(AllGuardBees[i].transform.position, BearPrefab.transform.position) <= 1f)
                {
                    BC.TakeDamage(1); // Fix magic number
                    Destroy(AllGuardBees[i]);
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
