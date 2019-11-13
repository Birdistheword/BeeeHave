using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BeeUpgradeShop : MonoBehaviour
{
  [SerializeField] Canvas shopUI;

  [SerializeField] Text speedStatText;
  [SerializeField] Text efficiencyStatText;
  [SerializeField] Text carryStatText;

  [SerializeField] int[] speedStatUpgradePrice = { 10, 30, 50, 70, 90 };
  [SerializeField] int[] efficiencyStatUpgradePrice = { 10, 30, 50, 70, 90 };
  [SerializeField] int[] carryStatUpgradePrice = { 10, 30, 50, 70, 90 };

  Dictionary<BeeStat, int[]> statUpgradePrice = new Dictionary<BeeStat, int[]>();

  bool playerIsIn;
  bool canBuyItem;

  bool speedMaxed = false;
  bool efficiencyMaxed = false;
  bool carryMaxed = false;

  int itemPrice;
  int nextLevelPrice;

  StatManager statManager;
  PollenManager pollenManager;
  FlowerSpawner flowerSpawner;
  Hive hive;

  public void OpenShop()
  {
    shopUI.enabled = true;
  }

  public void CloseShop()
  {
    shopUI.enabled = false;
  }

  public void GiveStat(string statName)
  {
    //gets the stat from string parameter
    BeeStat currentStat = CheckForStringStatInput(statName);

    // calculates the price
    itemPrice = CalculatePrice(currentStat);

    // checks if can buy item
    canBuyItem = CheckIfCanBuyItem(itemPrice);
    if (!canBuyItem) { print("cannot buy, not enough pollen"); return; }

    // adds level up and removes pollen from player
    pollenManager.RemovePollen(itemPrice);
    statManager.AddStat(currentStat);

    // calculates next level price
    nextLevelPrice = CalculatePrice(currentStat);

    // updates the text of price for given stat
    UpdateNextPriceText(currentStat);

    // checks if given stat is maxed
    CheckIfStatIsMaxed(currentStat);
  }

  public void ResetPricesToDefault()
  {
    UpdatePriceTexts(speedStatUpgradePrice[0].ToString(), efficiencyStatUpgradePrice[0].ToString(), carryStatUpgradePrice[0].ToString());
  }

  private void SetStatPrices()
  {
    statUpgradePrice[BeeStat.speedStat] = speedStatUpgradePrice;
    statUpgradePrice[BeeStat.efficiencyStat] = efficiencyStatUpgradePrice;
    statUpgradePrice[BeeStat.pollenCarryStat] = carryStatUpgradePrice;
  }

  private void Start()
  {
    shopUI.enabled = false;
    flowerSpawner = FindObjectOfType<FlowerSpawner>();
    pollenManager = FindObjectOfType<PollenManager>();
    hive = FindObjectOfType<Hive>();
    statManager = FindObjectOfType<StatManager>();
    SetStatPrices();
    ResetPricesToDefault();
  }

  private void Update()
  {
    CheckIfMaxed();
    if (playerIsIn && Input.GetKeyDown(KeyCode.Q))
    {
      OpenShop();
    }
    if (playerIsIn && Input.GetKeyDown(KeyCode.Escape))
    {
      CloseShop();
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      playerIsIn = true;
    }
  }

  private void UpdatePriceTexts(string speedParameter, string efficiencyParameter, string carryParameter)
  {
    UpdatePriceText(BeeStat.speedStat, speedParameter);
    UpdatePriceText(BeeStat.efficiencyStat, efficiencyParameter);
    UpdatePriceText(BeeStat.pollenCarryStat, carryParameter);
  }

  private void UpdatePriceText(BeeStat stat, string parameter)
  {
    if (stat == BeeStat.speedStat)
    {
      speedStatText.text = parameter.ToString();
    }
    if (stat == BeeStat.efficiencyStat)
    {
      efficiencyStatText.text = parameter.ToString();
    }
    if (stat == BeeStat.pollenCarryStat)
    {
      carryStatText.text = parameter.ToString();
    }
  }

  private void CheckIfMaxed()
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
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.tag == "Player")
    {
      playerIsIn = false;
      CloseShop();
    }
  }

  private void CheckIfStatIsMaxed(BeeStat currentStat)
  {
    if (statManager.GetStatLevel(currentStat) >= statUpgradePrice[currentStat].Length)
    {
      MaxStatPrice(currentStat);
    }
  }

  private void UpdateNextPriceText(BeeStat currentStat)
  {
    if (statUpgradePrice[currentStat].Length - 1 >= statManager.GetStatLevel(currentStat))
    {
      UpdatePriceText(currentStat, nextLevelPrice.ToString());
    }
    else
    {
      UpdatePriceText(currentStat, " ");
    }
  }

  private int CalculatePrice(BeeStat stat)
  {
    return statUpgradePrice[stat][statManager.GetStatLevel(stat) - 1];
  }

  private bool CheckIfCanBuyItem(int priceOfItem)
  {
    if (pollenManager.GetPollenCount() >= priceOfItem)
    {
      return true;
    }
    else
    {
      return false;
    }
  }

  private BeeStat CheckForStringStatInput(string input)
  {
    if (input == "Speed")
    {
      return BeeStat.speedStat;
    }
    else if (input == "Efficiency")
    {
      return BeeStat.efficiencyStat;
    }
    else
    {
      //last will always be carry
      return BeeStat.pollenCarryStat;
    }
  }

  private void MaxStatPrice(BeeStat stat)
  {
    if (stat == BeeStat.speedStat)
    {
      speedMaxed = true;
    }
    if (stat == BeeStat.efficiencyStat)
    {
      efficiencyMaxed = true;
    }
    if (stat == BeeStat.pollenCarryStat)
    {
      carryMaxed = true;
    }
  }
}
