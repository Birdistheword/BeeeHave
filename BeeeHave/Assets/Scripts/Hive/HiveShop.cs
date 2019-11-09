using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HiveShop : MonoBehaviour
{
  [SerializeField] ShopType shopType;
  [SerializeField] Canvas shopUI;

  [SerializeField] int flowerBuyPrice = 10;
  [SerializeField] int flowerPriceAdder = 1;

  [SerializeField] GameObject beePrefab = null;
  [SerializeField] GameObject flowerPrefab = null;

  [SerializeField] int bearRepelentsUsed = 0;
  [SerializeField] int[] bearRepelentPrice;

  [SerializeField] int maxBeeGuards;
  int beeGuardAmount;
  [SerializeField] int[] beeGuardPrice;

  bool playerIsIn;
  bool canBuyItem;

  int itemPrice;

  StatManager statManager;
  PollenManager pollenManager;
  FlowerSpawner flowerSpawner;
  Hive hive;

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
    flowerSpawner = FindObjectOfType<FlowerSpawner>();
    pollenManager = FindObjectOfType<PollenManager>();
    hive = FindObjectOfType<Hive>();
    statManager = FindObjectOfType<StatManager>();
  }

  private void Update()
  {
    if (beeGuardAmount >= maxBeeGuards)
    {
      shopUI.transform.GetChild(0).GetComponent<Button>().interactable = false;
    }

    if (playerIsIn && Input.GetKeyDown(KeyCode.Q))
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

  public void BuyBeeDefender()
  {
    canBuyItem = false;
    //itemPrice = beeGuardPrice[hive.GetBeeGuardAmount()];
    if (pollenManager.GetPollenCount() >= itemPrice)
    {
      canBuyItem = true;
    }
    if (!canBuyItem) { print("cannot buy, not enough pollen"); return; }
    beeGuardAmount++;
    if (beeGuardAmount >= maxBeeGuards)
    {
      shopUI.transform.GetChild(0).GetComponent<Button>().interactable = false;
    }
  }

  public void BuyFlower()
  {
    canBuyItem = false;
    if (pollenManager.GetPollenCount() >= flowerBuyPrice)
    {
      canBuyItem = true;
    }
    if (!canBuyItem) { print("cannot buy, not enough pollen"); return; }
    if (flowerSpawner.FlowersMaxed())
    {
      shopUI.transform.GetChild(1).GetComponent<Button>().interactable = false;
      return;
    }
    pollenManager.AddPollen(-flowerBuyPrice);
    flowerBuyPrice += flowerPriceAdder;
    flowerSpawner.SpawnFlower(flowerPrefab);
  }

  public void BuyBearRepelent()
  {
    if (bearRepelentsUsed >= 3) { return; }
    itemPrice = bearRepelentPrice[bearRepelentsUsed];
    print(itemPrice);
    if (pollenManager.GetPollenCount() >= itemPrice)
    {
      canBuyItem = true;
    }
    if (!canBuyItem) { print("cannot buy, not enough pollen"); return; }
    bearRepelentsUsed++;
    if (bearRepelentsUsed >= 3)
    {
      shopUI.transform.GetChild(2).GetComponent<Button>().interactable = false;
    }
  }

  public void OpenShop()
  {
    shopUI.enabled = true;
  }

  public void CloseShop()
  {
    shopUI.enabled = false;
  }
}