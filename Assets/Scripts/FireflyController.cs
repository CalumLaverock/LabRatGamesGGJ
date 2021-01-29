using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class FireflyController : MonoBehaviour
{
    public GameObject fireflyLightPrefab;
    GameObject lightObject;

    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;

    float timer;
    int direction = 1;

    CircleCollider2D lightCollider;
    Light2D lightRef;
    // Start is called before the first frame update
    void Start()
    {
        lightObject = Instantiate(fireflyLightPrefab, transform.position, Quaternion.identity);
        lightCollider = gameObject.AddComponent<CircleCollider2D>();
        timer = changeTime;

        lightRef = lightObject.GetComponent<Light2D>();
        lightCollider.radius = lightRef.pointLightOuterRadius;
        lightCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        transform.position = position;
        lightObject.gameObject.transform.position = position;

        lightCollider.transform.position = position;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        
    }
}
