using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hive : MonoBehaviour
{
  bool playerisIn;
  [SerializeField] int health = 5;
  [SerializeField] int healthBarCounter = 0;
  private GameStates GS;
  [SerializeField] GameObject[] HealthBars;



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
    if (health <= 0)
    {
      GS.STATE = GameStates.GameState.LoseCondition;
      SceneManager.LoadScene(0);
    }
  }

  public void TakeDamage(int damage)
  {
    if (healthBarCounter < 5 && damage > 0)
    {
      HealthBars[healthBarCounter].SetActive(false);
      print(HealthBars[healthBarCounter]);
      healthBarCounter++;
    }
    health -= damage;
    print("Took " + damage + " damage");
  }
}
