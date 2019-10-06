﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private void Start()
    {
        transform.rotation = Camera.main.transform.rotation;
    }

    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
