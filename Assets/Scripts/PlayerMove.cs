using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Vector2 startPos;
    public float speed = 20f;
    public CircleCollider2D self;
    public Rigidbody2D rb;

    public Animator animator;

    bool interacting;
    int interactions;
    float interactTimer;
    Vector2 move;

    bool clockwise = false;
    bool anticlockwise = false;

    void Update()
    {
        if(!interacting)
        {
            move.x = Input.GetAxisRaw("Horizontal");
            move.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("horizontal", move.x);
            animator.SetFloat("vertical", move.y);
            animator.SetFloat("speed", move.sqrMagnitude);

            if(Input.GetKeyDown(KeyCode.Q))
            {
                animator.SetBool("interacting", true);
                interacting = true;
                clockwise = true;
                interactTimer = 0;
                interactions = 1;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetBool("interacting", true);
                interacting = true;
                anticlockwise = true;
                interactTimer = 0;
                interactions = 1;
            }
        }
        else
        {
            move.x = 0;
            move.y = 0;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);

        if(interacting)
        {
            interactTimer += Time.fixedDeltaTime;

            if(interactTimer >= 0.1)
            {
                animator.SetBool("interacting", false);
                interacting = false;
            }
        }
    }

    void OnTriggerStay2D(Collider2D trigger)
    {
        if(interacting && interactions > 0)
        {
            if(trigger.gameObject.tag == "Mirror")
            {
                if (clockwise)
                {
                    trigger.gameObject.transform.Rotate(trigger.gameObject.transform.forward * -5f);
                    clockwise = false;
                }
            
                if(anticlockwise)
                {
                    trigger.gameObject.transform.Rotate(trigger.gameObject.transform.forward * 5f);
                    anticlockwise = false;
                }

                interactions--;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.gameObject.tag == "Key")
        {
            trigger.gameObject.SetActive(false);
        }
    }

    public void Reset()
    {
        rb.transform.position = startPos;
    }
}
