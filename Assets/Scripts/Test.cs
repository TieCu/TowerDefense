using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Somthing 3d entered range");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Something 2d entered range");
    }
}
