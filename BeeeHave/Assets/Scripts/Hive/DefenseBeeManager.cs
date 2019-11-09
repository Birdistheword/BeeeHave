using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBeeManager : MonoBehaviour
{
    [SerializeField] GameObject GuardbeePrefab;
    [SerializeField] Transform[] SpawnPoints;

    [HideInInspector]
    private int amountOfCurrentBees = 0;
    private int SpCounter = 0, children;
    private GameStates GS;

    void Start()
    {

        GS = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<GameStates>();

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            SpawnGuardBee();
        }

        if(GS.STATE == GameStates.GameState.BearAtHive && amountOfCurrentBees > 0)
        {
            //Changes behavious of guard bees
            GS.STATE = GameStates.GameState.ThereIsGuardBees;
            print(amountOfCurrentBees);
            print("There is guard bees");
        }

    }

    public void SpawnGuardBee()
    {
        if(amountOfCurrentBees < 5)
        {
            GameObject Bee = Instantiate(GuardbeePrefab, SpawnPoints[SpCounter].position, Quaternion.identity);
            SpCounter++;
            amountOfCurrentBees++;
        }
        
    }

    public void BeeDied()
    {
        amountOfCurrentBees--;
        print("Amount of current bees: " + amountOfCurrentBees);
        SpCounter--;
    }
}
