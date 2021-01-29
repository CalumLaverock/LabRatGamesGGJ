using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed = 20f;
    public CircleCollider2D self;
    public Rigidbody2D rb;

    public Animator animator;

    bool interacting;
    float interactTimer;
    Vector2 move;

    // Update is called once per frame
    void Update()
    {
        if(!interacting)
        {
            move.x = Input.GetAxisRaw("Horizontal");
            move.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("horizontal", move.x);
            animator.SetFloat("vertical", move.y);
            animator.SetFloat("speed", move.sqrMagnitude);

            if(Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("interacting", true);
                interacting = true;
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

            if(interactTimer >= 1)
            {
                animator.SetBool("interacting", false);
                interacting = false;
            }
        }
    }

    void OnCollisionStay2D(Collision collision)
    {
        if(interacting)
        {
            if(collision.gameObject.tag == "Mirror")
            {
                collision.gameObject.rotate();
            }
            if (collision.gameObject.tag == "Key")
            {
                collision.gameObject.pickUp();
            }
        }
    }
}
