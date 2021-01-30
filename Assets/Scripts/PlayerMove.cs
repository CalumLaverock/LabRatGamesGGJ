using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Vector2 startPos;
    public float speed = 5f;
    public CircleCollider2D self;
    public Rigidbody2D rb;

    public Animator animator;

    bool interacting;
    float interactTimer;
    Vector2 move;

    bool clockwise = false;
    bool anticlockwise = false;

    public int litUp = 0;

    void Start()
    {
        Reset();
    }

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
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetBool("interacting", true);
                interacting = true;
                anticlockwise = true;
                interactTimer = 0;
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

    void OnCollisionStay2D(Collision2D collision)
    {
        if(interacting)
        {
            if(collision.gameObject.tag == "Mirror")
            {
                if (clockwise)
                {
                    collision.gameObject.transform.Rotate(collision.gameObject.transform.forward * -5f);
                    clockwise = false;
                }
            
                if(anticlockwise)
                {
                    collision.gameObject.transform.Rotate(collision.gameObject.transform.forward * 5f);
                    anticlockwise = false;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.gameObject.tag == "Key")
        {
            trigger.gameObject.SetActive(false);
        }

        if(trigger.gameObject.tag == "LightBeam")
        {
            litUp++;
        }
    }

    void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "LightBeam")
        {
            litUp--;
            if(litUp < 1)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void Reset()
    {
        gameObject.SetActive(true);
        rb.transform.position = startPos;
    }
}
