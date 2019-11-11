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

  [HideInInspector]
  public bool startAttacking = false, isAttacking = false, didDamage = false;
  private bool firstAttack = true, hasRetreated = false, currentlyMoving = false;

  private int[] healthPool = { 1, 2, 2, 2, 3, 3, 3, 4 };
  private int currentHealth = 1;
  private GameStates gameState;
  bool bearCanRetreat;


  void Start()
  {
    animator = GetComponent<Animator>();
    gameState = FindObjectOfType<GameStates>().GetComponent<GameStates>();

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
      print("bear's HP " + currentHealth);
      hive.GetComponent<Hive>().TakeDamage(currentHealth);
    }
    /*else if (GS.STATE == GameStates.GameState.DidDamage && anim.GetCurrentAnimatorStateInfo(0).IsName("BearAttack"))
    {

    }*/
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
      isAttacking = true;
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
      currentHealth = healthPool[Random.Range(0, healthPool.Length)];
    }

    for (int i = 0; i < currentHealth; i++)
    {
      HealthBars[i].SetActive(true);
    }

    print("CurrenBearHealth: " + currentHealth);
  }

  // This method is called from GuardBeeController and the Player
  public void TakeDamage(int _damage)
  {
    if (currentHealth > 0)
    {
      HealthBars[currentHealth - 1].SetActive(false);
    }

    currentHealth -= _damage;
    print("Bear took " + _damage + " damage");
  }

  public int GetHealth()
  {
    return currentHealth;
  }
}
