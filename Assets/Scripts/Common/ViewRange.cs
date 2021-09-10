﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRange : MonoBehaviour
{
    public bool seePlayer = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            seePlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            seePlayer = false;
        }
    }
}