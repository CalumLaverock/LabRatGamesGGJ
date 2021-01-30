using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    float horizontal;
    float vertical;

    public bool dead = false;
    public float speed = 5;

    Rigidbody2D rigidbody2d;

    public GameObject fireflyLightPrefab;
    GameObject lightObject;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        lightObject = Instantiate(fireflyLightPrefab, rigidbody2d.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if(dead == true)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + (speed * horizontal * Time.deltaTime);
        position.y = position.y + (speed * vertical * Time.deltaTime);

        rigidbody2d.MovePosition(position);

        lightObject.gameObject.transform.position = position;
    }
}
