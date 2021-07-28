using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{

    public void OnCollisionEnterChild()
    {
        Destroy(gameObject);
    }
}
