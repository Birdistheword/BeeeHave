using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
  [SerializeField] ShopType shopType;
  [SerializeField] Canvas shopUI;

  bool playerIsIn;

  private void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      playerIsIn = true;
    }
  }

  private void Start()
  {
    shopUI.enabled = false;
  }

  private void Update()
  {
    if (playerIsIn && Input.GetKeyDown(KeyCode.Space))
    {
      OpenShop();
    }
    if (playerIsIn && Input.GetKeyDown(KeyCode.Escape))
    {
      CloseShop();
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.tag == "Player")
    {
      playerIsIn = false;
      CloseShop();
    }
  }

  public void GiveStat(BeeStats statType)
  {
    if (statType == BeeStats.efficiencyStat)
    {
      StatManager player = FindObjectOfType<StatManager>();
    }
  }

  private void OpenShop()
  {
    shopUI.enabled = true;
    print("shop opened");
  }

  private void CloseShop()
  {
    shopUI.enabled = false;
    print("shop closed");
  }
}
