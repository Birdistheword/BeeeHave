using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hive : MonoBehaviour
{
  bool playerisIn;
    public int health = 10;
    private GameStates GS;

    

  private void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      playerisIn = true;
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.tag == "Player")
    {
      playerisIn = false;
    }
  }
    private void Start()
    {
        GS = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<GameStates>();
    }

    // IF health is 0 or below, toggle lose condition Game State
    private void Update()
    {
        if(health <= 0)
        {
            GS.STATE = GameStates.GameState.LoseCondition;
        }
    }

    public void TakeDamage(int _dmg)
    {
        health -= _dmg;
        print("Took" + _dmg + "damage");
    }
}
