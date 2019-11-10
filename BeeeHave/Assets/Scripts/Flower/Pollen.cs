using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollen : MonoBehaviour
{

  float timeSinceEnteredPollenCollider;

  [SerializeField] float timeToPickupPollen;
  [SerializeField] float maxTimeToPickupPollen = 1;
  [SerializeField] float minTimeToPickupPollen = 2;

  [SerializeField] float timeToDestroyPollen = 2;

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
    print("Player entered pollen");
    timeSinceEnteredPollenCollider = 0;
    timeToPickupPollen = UnityEngine.Random.Range(maxTimeToPickupPollen, minTimeToPickupPollen);
  }

  private void OnTriggerStay(Collider other)
  {
    timeSinceEnteredPollenCollider += Time.deltaTime;
    if (timeSinceEnteredPollenCollider >= timeToPickupPollen)
    {
      if (other.tag == "Player")
      {
        PickupPollen(other);
      }
    }
  }

  private void PickupPollen(Collider other)
  {
    print("Player picked up pollen");
    if (!pollenWasGiven)
    {
      other.GetComponent<PollenManager>().AddPollen(pollenAmount);
      pollenWasGiven = true;
      print("Player got pollen");
      Instantiate(PickUpEffect, transform.position, Quaternion.identity);
    }
    flowerParent.SetPollenIsSpawned(false);
    print("Pollen spawn timer is reset now");
    Destroy(gameObject);
  }
}
