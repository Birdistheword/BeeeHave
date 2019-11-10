using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplaySpeedStat : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI text;

  StatManager statManager;

  private void Start()
  {
    statManager = FindObjectOfType<StatManager>();
  }

  void Update()
  {
    text.text = String.Format("Speed lvl - {0}", statManager.GetSpeedStatLevel().ToString());
  }
}
