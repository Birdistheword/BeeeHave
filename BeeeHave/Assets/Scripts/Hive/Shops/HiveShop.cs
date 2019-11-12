using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HiveShop : MonoBehaviour
{
  [SerializeField] ShopType shopType;
  [SerializeField] Canvas shopUI;

  [SerializeField] Text beeGuardCostText;
  [SerializeField] Text flowerCostText;
  [SerializeField] Text bearRepelentCostText;

  [SerializeField] int flowerBuyPrice = 10;
  [SerializeField] int flowerPriceAdder = 1;

  [SerializeField] GameObject flowerPrefab = null;

  [SerializeField] int bearRepelentsUsed = 0;
  [SerializeField] int[] bearRepelentPrice;

  [SerializeField] int maxBeeGuards;
  int beeGuardAmount = 0;
  [SerializeField] int[] beeGuardPrice;

  bool playerIsIn;
  bool canBuyItem;
  bool canBuyGuards = true;

  int itemPrice;

  StatManager statManager;
  PollenManager pollenManager;
  FlowerSpawner flowerSpawner;
  Hive hive;
  DefenseBeeManager defenseBeeManager;
  TimeToBearAttack timeToBearAttack;

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
    defenseBeeManager = FindObjectOfType<DefenseBeeManager>();
    timeToBearAttack = FindObjectOfType<TimeToBearAttack>();
    SetFlowerText(flowerBuyPrice.ToString());
    SetBearRepelentText(bearRepelentPrice[bearRepelentsUsed].ToString());
    SetBeeText(beeGuardPrice[beeGuardAmount].ToString());
  }

  private void Update()
  {
    if (beeGuardAmount >= maxBeeGuards && canBuyGuards)
    {
      shopUI.transform.GetChild(1).GetComponent<Button>().interactable = false;
      canBuyGuards = false;
    }
    else if (beeGuardAmount < maxBeeGuards && !canBuyGuards)
    {
      shopUI.transform.GetChild(1).GetComponent<Button>().interactable = false;
      canBuyGuards = false;
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

  public void SetBeeText(string priceAmount)
  {
    beeGuardCostText.text = priceAmount;
  }

  public void SetFlowerText(string priceAmount)
  {
    flowerCostText.text = priceAmount;
  }

  public void SetBearRepelentText(string priceAmount)
  {
    bearRepelentCostText.text = priceAmount;
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.tag == "Player")
    {
      playerIsIn = false;
      CloseShop();
    }
  }

  private void OnTriggerStay(Collider other)
  {
    if (other.tag == "Player" && playerIsIn)
    {
      beeGuardAmount = defenseBeeManager.GetBeeNumber();
      if (beeGuardAmount < maxBeeGuards)
      {
        SetBeeDefenderUIActive();
      }
    }
  }

  public void SetBeeDefenderUIActive()
  {
    shopUI.transform.GetChild(1).GetComponent<Button>().interactable = true;
    canBuyGuards = true;
    SetBeeDefenderPrice();
  }

  public void SetBeeDefenderPrice()
  {
    beeGuardCostText.text = beeGuardPrice[defenseBeeManager.GetBeeNumber()].ToString();
  }

  public void BuyBeeDefender()
  {
    canBuyItem = false;
    beeGuardAmount = defenseBeeManager.GetBeeNumber();
    itemPrice = beeGuardPrice[defenseBeeManager.GetBeeNumber()];
    if (pollenManager.GetPollenCount() >= itemPrice)
    {
      canBuyItem = true;
    }
    if (!canBuyItem) { print("cannot buy, not enough pollen"); return; }
    if (!canBuyGuards) { return; }
    pollenManager.RemovePollen(itemPrice);
    defenseBeeManager.SpawnGuardBee();
    beeGuardAmount = defenseBeeManager.GetBeeNumber();
    if (beeGuardAmount >= maxBeeGuards)
    {
      SetBeeText(" ");
      shopUI.transform.GetChild(1).GetComponent<Button>().interactable = false;
    }
    else
    {
      SetBeeText(beeGuardPrice[beeGuardAmount].ToString());
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
    pollenManager.RemovePollen(flowerBuyPrice);
    flowerBuyPrice += flowerPriceAdder;
    flowerSpawner.SpawnFlower(flowerPrefab);
    if (flowerSpawner.FlowersMaxed())
    {
      SetFlowerText(flowerBuyPrice.ToString(" "));
      shopUI.transform.GetChild(2).GetComponent<Button>().interactable = false;
      return;
    }
    else
    {
      SetFlowerText(flowerBuyPrice.ToString());
    }
  }

  public void BuyBearRepelent()
  {
    canBuyItem = false;
    if (bearRepelentsUsed >= 3) { return; }
    itemPrice = bearRepelentPrice[bearRepelentsUsed];
    if (pollenManager.GetPollenCount() >= itemPrice)
    {
      canBuyItem = true;
    }
    if (!canBuyItem) { print("cannot buy, not enough pollen"); return; }
    if (!timeToBearAttack.CanBuyBearRepelent()) { print("bear is not in Idle mode"); return; }
    pollenManager.RemovePollen(itemPrice);
    bearRepelentsUsed++;
    if (bearRepelentPrice.Length > bearRepelentsUsed)
    {
      SetBearRepelentText(bearRepelentPrice[bearRepelentsUsed].ToString());
    }
    if (bearRepelentsUsed >= 3)
    {
      SetBearRepelentText(" ");
      shopUI.transform.GetChild(3).GetComponent<Button>().interactable = false;
    }
  }

  public void OpenShop()
  {
    shopUI.enabled = true;
    beeGuardAmount = defenseBeeManager.GetBeeNumber();
    if (beeGuardAmount < maxBeeGuards)
    {
      SetBeeDefenderUIActive();
    }
  }

  public void CloseShop()
  {
    shopUI.enabled = false;
  }
}