using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisions : MonoBehaviour
{

    private GameObject gameManager;
    Rigidbody cargoRb;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        cargoRb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            StopMovement();
        }
        else if (collision.gameObject.CompareTag("CargoDespawn"))
        {
            StopMovement();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sensor"))
        {
            //Removes forward momentum for consistent movement
            StopMovement();
        }
    }

    void StopMovement()
    {
        cargoRb.velocity = Vector3.zero;
        cargoRb.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
