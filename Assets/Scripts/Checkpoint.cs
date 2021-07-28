using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameObject gameManager;

    public void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    //Inform the game manager the checkpoint has been reached
    public void BoxPassedCheckpoint()
    {
        gameManager.GetComponent<GameManager>().CheckpointReached();
    }

}
