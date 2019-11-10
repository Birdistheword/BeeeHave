﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowlyRotate : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0.5f;
    [SerializeField] float maxAltitudeChange = 2f;
    private Vector3 startPos;


    private void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, rotationSpeed, 0f);

        if (Mathf.Abs(startPos.y - transform.position.y) <= maxAltitudeChange)
        {

        }

    }
}
