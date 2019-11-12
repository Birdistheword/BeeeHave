using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeToBearAttack : MonoBehaviour
{
  [SerializeField] GameObject bearPrefab;
  [SerializeField] Image img;


  float[] attackIntervals = { 60f, 60f, 60f, 50f, 50f, 40f, 40f, 35f };

  [SerializeField] float counter = 0f;
  [SerializeField] float timeTillAttack;
  [SerializeField] bool countingTime = true;
  [SerializeField] float beatRepelentEffectivness = 30f;

  [SerializeField] Timers[] timers;

  [SerializeField] Dictionary<DifficultyLevels, float[]> difficultyTable;

  public bool sentAttack = false;
  private GameStates gameState;

  void Start()
  {
    timeTillAttack = attackIntervals[0];
    gameState = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<GameStates>();
  }

  void Update()
  {

    if (gameState.STATE == GameStates.GameState.Idle)
    {
      counter += Time.deltaTime;
      img.fillAmount = (timeTillAttack - counter) / timeTillAttack;
    }

    if (gameState.STATE == GameStates.GameState.Idle && counter >= timeTillAttack && !sentAttack)
    {
      gameState.STATE = GameStates.GameState.BearStartAttack;
    }

    if (gameState.STATE == GameStates.GameState.ResetTimer)
    {
      gameState.STATE = GameStates.GameState.Idle;
      counter = 0f;
      ChooseNewAttackInterval();
      img.fillAmount = 1;
    }
  }

  private void ChooseNewAttackInterval()
  {
    timeTillAttack = attackIntervals[Random.Range(1, attackIntervals.Length)];
  }

  public bool CanBuyBearRepelent()
  {
    if (gameState.STATE == GameStates.GameState.Idle)
    {
      BearRepelent();
      return true;
    }
    else
    {
      return false;
    }
  }

  public void BearRepelent()
  {
    counter -= beatRepelentEffectivness;
  }

  [System.Serializable]
  class Timers
  {
    DifficultyLevels difficultyLevels;
  }
}
