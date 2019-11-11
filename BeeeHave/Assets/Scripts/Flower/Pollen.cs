using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollen : MonoBehaviour
{
  [SerializeField] int pollenAmount = 10;

  [SerializeField] GameObject PickUpEffect;

  bool pollenWasGiven;

  Flower flowerParent;

  private void Start()
  {
    flowerParent = transform.GetComponentInParent<Flower>();
    pollenWasGiven = false;
    flowerParent.SetPollenIsSpawned(true);
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      PickupPollen(other);
    }
  }

  private void PickupPollen(Collider other)
  {
    if (!pollenWasGiven)
    {
      other.GetComponent<PollenManager>().AddPollen(pollenAmount);
      pollenWasGiven = true;
      Instantiate(PickUpEffect, transform.position, Quaternion.identity);
    }
    flowerParent.SetPollenIsSpawned(false);
    Destroy(gameObject);
  }
}
