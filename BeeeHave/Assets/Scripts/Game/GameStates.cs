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
        DamagePhase,
        DidDamage,
        LoseCondition,
        BearRetreat,
        ResetTimer
    }

    public GameState STATE =  GameState.Idle;
}
