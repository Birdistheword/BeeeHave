using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeToBearAttack : MonoBehaviour
{
    [SerializeField] GameObject bearPrefab;
    [SerializeField] Image img;


    float[] attackIntervals =  { 3f, 60f, 60f, 50f, 50f, 40f, 40f, 35f };

    private float counter = 0f, timeTillAttack;
    private bool countingTime = true;
    public bool  sentAttack = false;
    private GameStates GS;



    void Start()
    {
        timeTillAttack = attackIntervals[0];
        GS = GameObject.FindObjectOfType<GameStates>();
    }


    void Update()
    {

        if(countingTime)
        {
            counter += Time.deltaTime;
            Debug.Log(counter);
            img.fillAmount = (timeTillAttack - counter) / timeTillAttack;
        }

        if ( counter >= timeTillAttack && !sentAttack )
        {
            GS.STATE = GameStates.GameState.BearStartAttack;
            countingTime = false;
            ChooseNewAttackInterval();
            sentAttack = true;
        }


    }

    private void ChooseNewAttackInterval ()
    {
        timeTillAttack = attackIntervals[Random.Range(0, attackIntervals.Length)];
        Debug.Log("Time till next attack:" + timeTillAttack);
    }
}
