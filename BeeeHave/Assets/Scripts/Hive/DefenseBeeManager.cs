using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseBeeManager : MonoBehaviour
{
    [SerializeField] GameObject GuardbeePrefab;
    [SerializeField] Transform[] SpawnPoints;

    [HideInInspector]
    public int amountOfCurrentBees = 0;
    private int SpCounter = 0, children;

    void Start()
    {
        
           
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            SpawnGuardBee();
        }
    }

    public void SpawnGuardBee()
    {
        GameObject Bee = Instantiate(GuardbeePrefab, SpawnPoints[SpCounter].position, Quaternion.identity);
        SpCounter++;
    }
}
