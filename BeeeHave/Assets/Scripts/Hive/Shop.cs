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
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.tag == "Player")
    {
      playerIsIn = false;
      CloseShop();
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
