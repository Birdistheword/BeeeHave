using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeToBearAttack : MonoBehaviour
{
    [SerializeField] Image img;
    [SerializeField] float timeTillAttack = 0f;


    void Start()
    {
        
    }


    void Update()
    {
        timeTillAttack -= Time.deltaTime;

        if(timeTillAttack <= 0f)
        {
            // Bear Attack!!
        }

        img.fillAmount += timeTillAttack / 100f;
    }
}
