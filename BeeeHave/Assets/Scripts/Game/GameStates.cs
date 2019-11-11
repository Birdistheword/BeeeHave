using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour
{
  public enum GameState
  {
    Idle,
    BearStartAttack,
    BearMovingToHive,
    BearAtHive,
    ThereIsGuardBees,
    GuardBeesAttacking,
    DamagePhase,
    DidDamage,
    LoseCondition,
    BearRetreat,
    BearHasRetreated,
    ResetTimer
  }

  public GameState STATE = GameState.Idle;
}
