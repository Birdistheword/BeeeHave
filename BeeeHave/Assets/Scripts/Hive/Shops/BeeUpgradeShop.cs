using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BeeUpgradeShop : MonoBehaviour
{
  [SerializeField] ShopType shopType;
  [SerializeField] Canvas shopUI;

  [SerializeField] Text speedStatText;
  [SerializeField] Text efficiencyStatText;
  [SerializeField] Text carryStatText;

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
    ResetPricesToDefault();
  }

  public void ResetPricesToDefault()
  {
    UpdatePriceTexts(speedStatUpgradePrice[0].ToString(), efficiencyStatUpgradePrice[0].ToString(), carryStatUpgradePrice[0].ToString());
  }

  private void UpdatePriceTexts(string speedParameter, string efficiencyParameter, string carryParameter)
  {
    UpdateSpeedPriceText(speedParameter);
    UpdateEfficiencyPriceText(efficiencyParameter);
    UpdateCarryPriceText(carryParameter);
  }

  private void UpdateSpeedPriceText(string speedParameter)
  {
    print("Update speed price text to" + speedParameter);
    speedStatText.text = speedParameter.ToString();
  }

  private void UpdateEfficiencyPriceText(string efficiencyParameter)
  {
    efficiencyStatText.text = efficiencyParameter.ToString();
  }

  private void UpdateCarryPriceText(string carryParameter)
  {
    carryStatText.text = carryParameter.ToString();
  }

  private void Update()
  {
    if (speedMaxed)
    {
      shopUI.transform.GetChild(1).GetComponent<Button>().interactable = false;
    }
    if (efficiencyMaxed)
    {
      shopUI.transform.GetChild(2).GetComponent<Button>().interactable = false;
    }
    if (carryMaxed)
    {
      shopUI.transform.GetChild(3).GetComponent<Button>().interactable = false;
    }
    if (playerIsIn && Input.GetKeyDown(KeyCode.Q))
    {
      print("Opening shop");
      OpenShop();
    }
    if (playerIsIn && Input.GetKeyDown(KeyCode.Escape))
    {
      print("Closing shop");
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

    int nextLevelPrice = speedStatUpgradePrice[statManager.GetSpeedStatLevel()];

    pollenManager.AddPollen(-itemPrice);
    statManager.AddSpeedStat();

    if (speedStatUpgradePrice.Length - 1 >= statManager.GetSpeedStatLevel())
    {
      UpdateSpeedPriceText(nextLevelPrice.ToString());
    }
    else
    {
      UpdateSpeedPriceText("");
    }

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

    int nextLevelPrice = speedStatUpgradePrice[statManager.GetEfficiencyStatLevel()];

    pollenManager.AddPollen(-itemPrice);
    statManager.AddEfficiencyStat();

    if (speedStatUpgradePrice.Length - 1 >= statManager.GetEfficiencyStatLevel())
    {
      UpdateEfficiencyPriceText(nextLevelPrice.ToString());
    }
    else
    {
      UpdateEfficiencyPriceText("");
    }

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

    int nextLevelPrice = speedStatUpgradePrice[statManager.GetCarryStatLevel()];

    pollenManager.AddPollen(-itemPrice);
    statManager.AddCarryStat();

    if (speedStatUpgradePrice.Length - 1 >= statManager.GetCarryStatLevel())
    {
      UpdateCarryPriceText(nextLevelPrice.ToString());
    }
    else
    {
      UpdateCarryPriceText("");
    }

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
