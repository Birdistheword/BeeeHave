using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : MonoBehaviour
{
  [SerializeField]
  Transform BearTarget, BearDamageTarget, BearStartPos;
  [SerializeField] float movSpeed = 3f;
  [SerializeField] GameObject Hive;
  Animator animator;
  [SerializeField] GameObject[] HealthBars;

  [HideInInspector]
  public bool startAttacking = false, isAttacking = false, didDamage = false;
  private bool firstAttack = true, hasRetreated = false, currentlyMoving = false;

  private int[] HealthPool = { 1, 2, 2, 2, 3, 3, 3, 4 };
  private int currentHealth = 1;
  private GameStates GS;
  bool bearCanRetreat;


  void Start()
  {
    animator = GetComponent<Animator>();
    GS = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<GameStates>();

    // Deactivate Health Bars
    foreach (GameObject bar in HealthBars)
    {
      bar.SetActive(false);
    }
  }


  private void MoveToHive()
  {
    if (GS.STATE == GameStates.GameState.BearMovingToHive && !currentlyMoving)
    {
      //transform.position = Vector3.MoveTowards(transform.position, BearTarget.position, movSpeed * Time.deltaTime);
      print("STARTING WALKING ANIMATION AND BEAR ATTACK");
      animator.SetTrigger("StartWalk");
      currentlyMoving = true;
    }

  }

  private void WhenAtHive()
  {

    GS.STATE = GameStates.GameState.BearAtHive;
    print("Bear at hive");

    StartCoroutine(WaitForBeeAttack());
    currentlyMoving = false;

  }



  private IEnumerator WaitForBeeAttack()
  {
    //Wait to check if there is defense bees
    yield return new WaitForSeconds(3f);

    if (GS.STATE == GameStates.GameState.BearAtHive)
    {
      GS.STATE = GameStates.GameState.DamagePhase;
      print("Damage phase");
    }

    yield return new WaitForSeconds(2f);

  }

  private void Damage()
  {
    if (GS.STATE == GameStates.GameState.DamagePhase)
    {
      // Damage the Hive
      animator.SetTrigger("StartAttack");
      GS.STATE = GameStates.GameState.DidDamage;
      print("did damage");
      Hive.GetComponent<Hive>().TakeDamage(currentHealth);
    }


    /*else if (GS.STATE == GameStates.GameState.DidDamage && anim.GetCurrentAnimatorStateInfo(0).IsName("BearAttack"))
    {

    }*/
  }

  public void BearFinishedAttacking()
  {
    //transform.position = Vector3.MoveTowards(transform.position, BearDamageTarget.position, movSpeed * Time.deltaTime);
    print("bear finished attacking");
    Vector3.MoveTowards(transform.position, BearStartPos.position, movSpeed * Time.deltaTime);
    GS.STATE = GameStates.GameState.BearRetreat;
    animator.SetTrigger("Retreat");
    bearCanRetreat = true;
  }

  private void Retreat()
  {
    //Then Go back -> Retreat
    if (GS.STATE == GameStates.GameState.BearRetreat && bearCanRetreat)
    {
      bearCanRetreat = false;
      print("BearRetreat");
      animator.SetTrigger("Retreat");
    }
  }

  public void DoneRetreating()
  {
    GS.STATE = GameStates.GameState.ResetTimer;
    bearCanRetreat = false;
    hasRetreated = true;
    print("RESETTIMER");
    animator.SetTrigger("FinishedRetreat");
  }

  // Update is called once per frame
  void Update()
  {

    // At Start, Check for Start Attack
    if (GS.STATE == GameStates.GameState.BearStartAttack)
    {
      print("Start attack");
      StartAttack();
    }

    MoveToHive();
    Damage();
    Retreat();

    // Retreat if no health
    if (GS.STATE != GameStates.GameState.Idle && currentHealth <= 0 && !hasRetreated)
    {
      GS.STATE = GameStates.GameState.BearRetreat;
      print("RETREAT BECAUSE DEAD");
      hasRetreated = true;
    }

    if (GS.STATE == GameStates.GameState.Idle && hasRetreated) hasRetreated = false;

    // If has retreated
    if (GS.STATE == GameStates.GameState.BearHasRetreated)
    {
      GS.STATE = GameStates.GameState.ResetTimer;
      print("ResetTimer");
      return;
    }


  }


  //Set attacking to true and let bear approach Hive
  private void StartAttack()
  {
    if (GS.STATE == GameStates.GameState.BearStartAttack)
    {
      GS.STATE = GameStates.GameState.BearMovingToHive;
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
      currentHealth = HealthPool[Random.Range(0, HealthPool.Length)];

    for (int i = 0; i < currentHealth; i++)
    {
      HealthBars[i].SetActive(true);
    }

    print("CurrenBearHealth:" + currentHealth);
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
