using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : MonoBehaviour
{
  [SerializeField]
  Transform bearTarget, bearDamageTarget, bearStartPos;
  [SerializeField] float movSpeed = 3f;
  [SerializeField] GameObject hive;
  Animator animator;
  [SerializeField] GameObject[] HealthBars;

  [SerializeField] bool startAttacking = false;
  [SerializeField] bool didDamage = false;
  [SerializeField] bool firstAttack = true;
  [SerializeField] bool hasRetreated = false;
  [SerializeField] bool currentlyMoving = false;

  [SerializeField] int[] health = { 1, 2, 2, 3, 3, 3, 4, 4 };

  TimeToBearAttack timeToBearAttack;

  private int currentHealth = 1;
  private GameStates gameState;
  bool bearCanRetreat;

  void Start()
  {
    animator = GetComponent<Animator>();
    gameState = FindObjectOfType<GameStates>().GetComponent<GameStates>();
    timeToBearAttack = FindObjectOfType<TimeToBearAttack>();
    // Deactivate Health Bars
    foreach (GameObject bar in HealthBars)
    {
      bar.SetActive(false);
    }
  }


  private void MoveToHive()
  {
    if (gameState.STATE == GameStates.GameState.BearMovingToHive && !currentlyMoving)
    {
      //transform.position = Vector3.MoveTowards(transform.position, BearTarget.position, movSpeed * Time.deltaTime);
      animator.SetTrigger("StartWalk");
      currentlyMoving = true;
    }
  }

  private void WhenAtHive()
  {
    currentlyMoving = false;
    gameState.STATE = GameStates.GameState.BearAtHive;
    StartCoroutine(WaitForBeeAttack());
  }

  private IEnumerator WaitForBeeAttack()
  {
    //Wait to check if there is defense bees
    yield return new WaitForSeconds(3f);

    if (gameState.STATE == GameStates.GameState.BearAtHive)
    {
      gameState.STATE = GameStates.GameState.DamagePhase;
    }

    yield return new WaitForSeconds(2f);
  }

  private void Damage()
  {
    if (gameState.STATE == GameStates.GameState.DamagePhase)
    {
      // Damage the Hive
      animator.SetTrigger("StartAttack");
      gameState.STATE = GameStates.GameState.DidDamage;
      hive.GetComponent<Hive>().TakeDamage(currentHealth);
    }
  }

  public void BearFinishedAttacking()
  {
    //transform.position = Vector3.MoveTowards(transform.position, BearDamageTarget.position, movSpeed * Time.deltaTime);
    Vector3.MoveTowards(transform.position, bearStartPos.position, movSpeed * Time.deltaTime);
    gameState.STATE = GameStates.GameState.BearRetreat;
    animator.SetTrigger("Retreat");
    bearCanRetreat = true;
  }

  private void Retreat()
  {
    //Then Go back -> Retreat
    if (gameState.STATE == GameStates.GameState.BearRetreat && bearCanRetreat)
    {
      bearCanRetreat = false;
      animator.SetTrigger("Retreat");
    }
  }

  public void DoneRetreating()
  {
    gameState.STATE = GameStates.GameState.ResetTimer;
    bearCanRetreat = false;
    hasRetreated = true;
    animator.SetTrigger("FinishedRetreat");
  }

  // Update is called once per frame
  void Update()
  {

    // At Start, Check for Start Attack
    if (gameState.STATE == GameStates.GameState.BearStartAttack)
    {
      StartAttack();
    }

    MoveToHive();
    Damage();
    Retreat();

    // Retreat if no health
    if (gameState.STATE != GameStates.GameState.Idle && currentHealth <= 0 && !hasRetreated)
    {
      gameState.STATE = GameStates.GameState.BearRetreat;
      hasRetreated = true;
    }

    if (gameState.STATE == GameStates.GameState.Idle && hasRetreated) hasRetreated = false;

    // If has retreated
    if (gameState.STATE == GameStates.GameState.BearHasRetreated)
    {
      gameState.STATE = GameStates.GameState.ResetTimer;
      return;
    }


  }


  //Set attacking to true and let bear approach Hive
  private void StartAttack()
  {
    if (gameState.STATE == GameStates.GameState.BearStartAttack)
    {
      gameState.STATE = GameStates.GameState.BearMovingToHive;
      SetHealth();
    }
  }



  //Set Health, 
  // First time -> 1
  // Next Times Random from Array
  private void SetHealth()
  {
    if (firstAttack)
    {
      currentHealth = 1;
      HealthBars[0].SetActive(true);
      firstAttack = false;
    }
    else
    {
      if (timeToBearAttack.attackIntervals.Length - 1 <= timeToBearAttack.GetAttackIntervalIndex())
      {
        currentHealth = health[timeToBearAttack.attackIntervals.Length - 1];
      }
      else
      {
        currentHealth = health[timeToBearAttack.GetAttackIntervalIndex()];
      }
    }

    for (int i = 0; i < currentHealth; i++)
    {
      HealthBars[i].SetActive(true);
    }
  }

  // This method is called from GuardBeeController and the Player
  public void TakeDamage(int _damage)
  {
    if (currentHealth > 0)
    {
      HealthBars[currentHealth - 1].SetActive(false);
    }

    currentHealth -= _damage;
  }

  public int GetHealth()
  {
    return currentHealth;
  }
}
