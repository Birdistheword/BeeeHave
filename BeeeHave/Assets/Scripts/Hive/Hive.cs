using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hive : MonoBehaviour
{
    bool playerisIn;
    public int health = 5, HBcounter = 0;
    private GameStates GS;
    [SerializeField]
    GameObject[] HealthBars;



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
            SceneManager.LoadScene("Main Menu");
        }

       
    }

    public void TakeDamage(int _dmg)
    {
        if( HBcounter < 5 )
        {
            HealthBars[HBcounter].SetActive(false);
            HBcounter++;

        }
        health -= _dmg;
        print("Took" + _dmg + "damage");
    }
}
