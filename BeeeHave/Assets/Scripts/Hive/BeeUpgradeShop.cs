using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BeeUpgradeShop : MonoBehaviour
{
  [SerializeField] ShopType shopType;
  [SerializeField] Canvas shopUI;

  [SerializeField] int[] speedStatUpgradePrice;
  [SerializeField] int[] efficiencyStatUpgradePrice;
  [SerializeField] int[] carryStatUpgradePrice;

  bool playerIsIn;
  bool canBuyItem;

  bool speedMaxed = false;
  bool efficiencyMaxed = false;
  bool carryMaxed = false;

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
    if (speedMaxed)
    {
      shopUI.transform.GetChild(0).GetComponent<Button>().interactable = false;
    }
    if (efficiencyMaxed)
    {
      shopUI.transform.GetChild(1).GetComponent<Button>().interactable = false;
    }
    if (carryMaxed)
    {
      shopUI.transform.GetChild(2).GetComponent<Button>().interactable = false;
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

  public void GiveSpeedStat()
  {
    canBuyItem = false;
    itemPrice = speedStatUpgradePrice[statManager.GetSpeedStatLevel() - 1];
    if (pollenManager.GetPollenCount() >= itemPrice)
    {
      canBuyItem = true;
    }
    if (!canBuyItem) { print("cannot buy, not enough pollen"); return; }
    pollenManager.AddPollen(-speedStatUpgradePrice[statManager.GetSpeedStatLevel() - 1]);
    statManager.AddSpeedStat();
    if (statManager.GetSpeedStatLevel() >= speedStatUpgradePrice.Length)
    {
      speedMaxed = true;
    }
  }


  public void GiveEfficiencyStat()
  {
    canBuyItem = false;
    itemPrice = speedStatUpgradePrice[statManager.GetEfficiencyStatLevel() - 1];
    if (pollenManager.GetPollenCount() >= itemPrice)
    {
      canBuyItem = true;
    }
    if (!canBuyItem) { print("cannot buy, not enough pollen"); return; }
    pollenManager.AddPollen(-speedStatUpgradePrice[statManager.GetEfficiencyStatLevel() - 1]);
    statManager.AddEfficiencyStat();
    if (statManager.GetEfficiencyStatLevel() >= speedStatUpgradePrice.Length)
    {
      efficiencyMaxed = true;
    }
  }

  public void GiveCarryStat()
  {
    canBuyItem = false;
    itemPrice = speedStatUpgradePrice[statManager.GetCarryStatLevel() - 1];
    if (pollenManager.GetPollenCount() >= itemPrice)
    {
      canBuyItem = true;
    }
    if (!canBuyItem) { print("cannot buy, not enough pollen"); return; }
    pollenManager.AddPollen(-speedStatUpgradePrice[statManager.GetCarryStatLevel() - 1]);
    statManager.AddCarryStat();
    if (statManager.GetCarryStatLevel() >= speedStatUpgradePrice.Length)
    {
      carryMaxed = true;
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
