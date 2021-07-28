using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    [SerializeField] float trainSpeed;
    private Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveTrain();
    }

    //Moves the train towards the destination
    void MoveTrain()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            destination, trainSpeed * Time.deltaTime);
    }

    //Sets the train boxes destination
    public void SetDestination(Vector3 endDestination)
    {
        destination = endDestination;
    }
}