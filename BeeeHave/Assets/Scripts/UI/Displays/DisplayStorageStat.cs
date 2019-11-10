using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayStorageStat : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI text;

  StatManager statManager;

  private void Start()
  {
    statManager = FindObjectOfType<StatManager>();
  }

  void Update()
  {
    text.text = String.Format("Storage lvl - {0}", statManager.GetCarryStatLevel().ToString());
  }
}
