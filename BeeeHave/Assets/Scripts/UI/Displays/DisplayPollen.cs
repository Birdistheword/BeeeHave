using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayPollen : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI text;

  PollenManager pollenManager;

  private void Start()
  {
    pollenManager = FindObjectOfType<PollenManager>();
  }

  void Update()
  {
    text.text = String.Format("Pollen : {0}", pollenManager.GetPollenCount().ToString());
  }
}
