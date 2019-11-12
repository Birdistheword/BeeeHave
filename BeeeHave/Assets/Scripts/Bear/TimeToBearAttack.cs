using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeToBearAttack : MonoBehaviour
{
  [SerializeField] GameObject bearPrefab;
  [SerializeField] Image img;


  public float[] attackIntervals = { 60, 50, 45, 35, 30, 25, 20, 10 };
  [SerializeField] int attackIntervalIndex = 0;

  [SerializeField] float counter = 0f;
  [SerializeField] float timeTillAttack;
  [SerializeField] bool countingTime = true;
  [SerializeField] float beatRepelentEffectivness = 30f;

  public bool sentAttack = false;
  private GameStates gameState;

  void Start()
  {
    timeTillAttack = attackIntervals[attackIntervalIndex++];
    gameState = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<GameStates>();
  }

  public int GetAttackIntervalIndex()
  {
    return attackIntervalIndex;
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
    if (attackIntervalIndex >= attackIntervals.Length)
    {
      timeTillAttack = attackIntervals[attackIntervals.Length - 1];
    }
    else
    {
      timeTillAttack = attackIntervals[attackIntervalIndex++];
    }
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
}
