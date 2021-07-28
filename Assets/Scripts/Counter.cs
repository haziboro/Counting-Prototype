using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //Tracks when objects fall in a box
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cargo"))
        {
            if (gameObject.CompareTag("Bad"))
            {
                gameManager.UpdateCounter(-5);
            }
            else if (gameObject.CompareTag("HighPoint"))
            {
                gameManager.UpdateCounter(3);
            }
            else
            {
                gameManager.UpdateCounter(1);
            }
        }

    }

}
