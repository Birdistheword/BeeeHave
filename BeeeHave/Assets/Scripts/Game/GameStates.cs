using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    public enum GameState
    {
        Idle,
        BearStartAttack,
        BearAtHive,
        DamagePhase,
        LoseCondition,
        BearRetreat
    }

    public GameState STATE =  GameState.Idle;
}
