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
    Vector2 currentMove;

    public bool moved;

    // Start is called before the first frame update
    void Start()
    {
        enabled = false;
        moved = false;

        unitMove.x = doorMovement.x / Speed;
        unitMove.y = doorMovement.y / Speed;

        currentMove.x = 0;
        currentMove.y = 0;
    }

    void FixedUpdate()
    {
        currentMove += unitMove * Speed * Time.fixedDeltaTime;
        door.MovePosition(door.position + unitMove * Speed * Time.fixedDeltaTime);
        
        if(currentMove.magnitude > doorMovement.magnitude)
        {
            door.MovePosition(startPos + doorMovement);
            enabled = false;
            moved = true;
        }
    }

    public void Reset()
    {
        moved = false;
        enabled = false;
        door.transform.position = startPos;
        currentMove.x = 0;
        currentMove.y = 0;
    }
}
