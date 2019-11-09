using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
  [SerializeField] float timeToReceivePollen = 2f;
  [SerializeField] float timeSinceEnteredCollider;
  [SerializeField] float pollenAmount = 10;

  bool pollenReadyToGive;
  bool playerIsIn;
  bool pollenWasGiven = true;

  float timeSinceGavePollen = Mathf.Infinity;
  [SerializeField] float pollenRespawnTimer;

  private void Update()
  {
    timeSinceGavePollen += Time.deltaTime;
    if (timeSinceGavePollen >= pollenRespawnTimer)
    {
      pollenReadyToGive = true;
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      NullificateTimers();
      timeSinceEnteredCollider = 0;
      playerIsIn = true;
    }
  }

  private void NullificateTimers()
  {
    timeSinceGavePollen = 0;
    timeSinceEnteredCollider = 0;
  }

  private void OnTriggerStay(Collider other)
  {
    if (other.tag == "Player")
    {
      print("player entereted");
      if (pollenReadyToGive)
      {
        if (pollenWasGiven)
        {
          timeSinceEnteredCollider = 0;
          pollenWasGiven = false;
        }
        timeSinceEnteredCollider += Time.deltaTime;
        if (timeSinceEnteredCollider >= timeToReceivePollen)
        {
          GivePollen(pollenAmount, other);
          timeSinceGavePollen = 0;
          pollenReadyToGive = false;
          pollenWasGiven = true;
        }
      }
    }
  }

  private void GivePollen(float pollenAmount, Collider other)
  {
    other.GetComponent<PollenManager>().AddPollen(pollenAmount);
    print(other.name + " received " + pollenAmount + " pollen");
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.tag == "Player")
    {
      playerIsIn = false;
    }
  }
}
