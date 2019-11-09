using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
  [SerializeField] ShopType shopType;
  [SerializeField] Canvas shopUI;

  [SerializeField] float[] speedStatUpgradePrice;
  [SerializeField] float[] efficiencyStatUpgradePrice;
  [SerializeField] float[] carryStatUpgradePrice;

  [SerializeField] GameObject beePrefab = null;
  [SerializeField] GameObject flowerPrefab = null;

  bool playerIsIn;
  bool canBuyUpgrade;

  bool speedMaxed = false;
  bool efficiencyMaxed = false;
  bool carryMaxed = false;

  StatManager player;
  PollenManager pollenManager;

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
    GetPlayerComponents(out player, out pollenManager);
    if (pollenManager.GetPollenCount() >= speedStatUpgradePrice[player.GetSpeedStatLevel() - 1])
    {
      canBuyUpgrade = true;
    }
    if (!canBuyUpgrade) { print("cannot buy, not enough pollen"); return; }
    pollenManager.AddPollen(-speedStatUpgradePrice[player.GetSpeedStatLevel() - 1]);
    player.AddSpeedStat();
    if (player.GetSpeedStatLevel() >= speedStatUpgradePrice.Length)
    {
      speedMaxed = true;
    }
  }


  public void GiveEfficiencyStat()
  {
    GetPlayerComponents(out player, out pollenManager);
    if (pollenManager.GetPollenCount() >= speedStatUpgradePrice[player.GetEfficiencyStatLevel() - 1])
    {
      canBuyUpgrade = true;
    }
    if (!canBuyUpgrade) { print("cannot buy, not enough pollen"); return; }
    pollenManager.AddPollen(-speedStatUpgradePrice[player.GetEfficiencyStatLevel() - 1]);
    player.AddEfficiencyStat();
    if (player.GetEfficiencyStatLevel() >= speedStatUpgradePrice.Length)
    {
      efficiencyMaxed = true;
    }
  }

  public void GiveCarryStat()
  {
    GetPlayerComponents(out player, out pollenManager);
    if (pollenManager.GetPollenCount() >= speedStatUpgradePrice[player.GetCarryStatLevel() - 1])
    {
      canBuyUpgrade = true;
    }
    if (!canBuyUpgrade) { print("cannot buy, not enough pollen"); return; }
    pollenManager.AddPollen(-speedStatUpgradePrice[player.GetCarryStatLevel() - 1]);
    player.AddCarryStat();
    if (player.GetCarryStatLevel() >= speedStatUpgradePrice.Length)
    {
      carryMaxed = true;
    }
  }

  public void BuyBearRepelent()
  {

  }

  public void BuyFlower()
  {
      
  }

  public void GenerateFlowerSpawnLocation()
  {

  }

  public void BuyBeeDefender()
  {

  }

  public void OpenShop()
  {
    shopUI.enabled = true;
  }

  public void CloseShop()
  {
    shopUI.enabled = false;
  }

  private void GetPlayerComponents(out StatManager player, out PollenManager pollenManager)
  {
    player = FindObjectOfType<StatManager>();
    pollenManager = player.GetComponent<PollenManager>();
  }
}
