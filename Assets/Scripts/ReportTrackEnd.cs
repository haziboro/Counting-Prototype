using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportTrackEnd : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sensor"))
        {
            DestroyOutOfBounds parentScript = transform.parent.GetComponent<DestroyOutOfBounds>();
            parentScript.OnCollisionEnterChild();
        }
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            other.gameObject.GetComponent<Checkpoint>().BoxPassedCheckpoint();
        }
    }


}
