using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDoor : MonoBehaviour
{
    public GameObject relatedDoor;

    void Start()
    {
        enabled = false;
    }

    void FixedUpdate()
    {
        relatedDoor.GetComponent<Move>().enabled = true;
    }
}
