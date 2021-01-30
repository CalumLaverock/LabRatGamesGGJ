using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    public GameObject player;
    public PlayerMove playerScript;
    public GameObject keyOne;
    public KeyScript keyScript;
    public GameObject doorOne;
    public DoorScript doorScript;
    public GameObject mirror;
    public MirrorScript mirrorScript;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }
    
    void FixedUpdate()
    {

    }

    void Reset()
    {
        playerScript.Reset();
        doorScript.Reset();
        keyScript.Reset();
        mirrorScript.Reset();
    }
}
