using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeeGuardDisplay : MonoBehaviour
{
  DefenseBeeManager defenseBeeManager;
  [SerializeField] Image image1;
  [SerializeField] Image image2;
  [SerializeField] Image image3;
  [SerializeField] Image image4;
  [SerializeField] Image image5;

  int beeGuardianCount;

  void Start()
  {
    defenseBeeManager = FindObjectOfType<DefenseBeeManager>();
  }

  void Update()
  {
    beeGuardianCount = defenseBeeManager.GetBeeNumber();
    if (beeGuardianCount == 0)
    {
      image1.enabled = false;
      image2.enabled = false;
      image3.enabled = false;
      image4.enabled = false;
      image5.enabled = false;
    }
    if (beeGuardianCount == 1)
    {
      image1.enabled = true;
      image2.enabled = false;
      image3.enabled = false;
      image4.enabled = false;
      image5.enabled = false;
    }
    if (beeGuardianCount == 2)
    {
      image1.enabled = true;
      image2.enabled = true;
      image3.enabled = false;
      image4.enabled = false;
      image5.enabled = false;
    }
    if (beeGuardianCount == 3)
    {
      image1.enabled = true;
      image2.enabled = true;
      image3.enabled = true;
      image4.enabled = false;
      image5.enabled = false;
    }
    if (beeGuardianCount == 4)
    {
      image1.enabled = true;
      image2.enabled = true;
      image3.enabled = true;
      image4.enabled = true;
      image5.enabled = false;
    }
    if (beeGuardianCount == 5)
    {
      image1.enabled = true;
      image2.enabled = true;
      image3.enabled = true;
      image4.enabled = true;
      image5.enabled = true;
    }
  }
}
