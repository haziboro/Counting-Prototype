using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float dropperSpeed = 10f;
    [SerializeField] float levelBoundaries = 17.0f;

    [SerializeField] GameObject cargo;
    [SerializeField] GameManager gameManager;

    private float horizontalInput;
    private Vector3 cargoSpawnPosition;
    private float cargoDropVerticalOffset = 2;
    private float timeBetween = 0.5f; //interval between Spacebar presses
    private float timestamp;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            SideMovement();
            CheckBoundaries();
            DropCargo();
        }
    }


    //Allows Dropper movement from side to side
    private void SideMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * Time.deltaTime * dropperSpeed * horizontalInput);
    }

    //Establishes level boundaries
    private void CheckBoundaries()
    {
        // Check for left and right bounds, on the Z axis here
        if (transform.position.z < -levelBoundaries)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -levelBoundaries);
        }
        if (transform.position.z > levelBoundaries)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, levelBoundaries);
        }
    }

    //Drops Cargo from the players position offset vertically by cargoDropVerticalOffset
    void DropCargo()
    {
        if (Time.time >= timestamp && Input.GetKeyDown(KeyCode.Space))
        {
            cargoSpawnPosition = new Vector3(transform.position.x,
                transform.position.y - cargoDropVerticalOffset, transform.position.z);

            GameObject pooledCargo = ObjectPooler.SharedInstance.GetPooledObject();
            if (pooledCargo != null)
            {
                pooledCargo.SetActive(true); // activate it
                pooledCargo.transform.position = cargoSpawnPosition; // position it at player
            }//endif
            timestamp = Time.time + timeBetween;
        }//endif
    }

}
