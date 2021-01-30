using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript: MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 doorMovement;
    public float Speed;
    public Rigidbody2D door;
    Vector2 unitMove;

    // Start is called before the first frame update
    void Start()
    {
        

        unitMove.x = doorMovement.x / Speed;
        unitMove.y = doorMovement.y / Speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        door.MovePosition(door.position + unitMove * Speed * Time.fixedDeltaTime);
    }

    public void Reset()
    {
        door.transform.position = startPos;
    }
}
