using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Mirror : MonoBehaviour
{
    GameObject hitObj;

    GameObject LightBeam;
    public GameObject LightObject;
    public bool createBeam;

    public Vector3 lightOutAngle;
    public Vector3 reflectAngle;

    public Vector3 startPos;
    public Vector3 mid;
    public Vector3 rightVec;
    public Vector3 norm;
    public Vector3 normalizedNormal;
    public float width;

    // Start is called before the first frame update
    void Start()
    {
        lightOutAngle = new Vector3(0.0f, 0.0f, 0.0f);
        createBeam = false;
    }

    // Update is called once per frame
    void Update()
    {
        float vert = Input.GetAxis("Vertical");
        float horiz = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown("space"))
        {
            transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), 5.0f);
        }

        if((Mathf.Round(norm.x * 100.0f)) / 100.0f != (Mathf.Round(transform.up.x * 100.0f)) / 100.0f)
        {
            createBeam = false;
        }

        if(LightBeam != null && !createBeam)
        {
            Destroy(LightBeam);
            LightBeam = null;
        }

        RaycastHit2D hit;

        width = GetComponent<BoxCollider2D>().bounds.size.x;
        startPos = transform.position + (transform.up*0.5f);// (width / 2));

        if (hit = Physics2D.Raycast(startPos, lightOutAngle))
        {
            if (!hit.collider.isTrigger)
            {
                rightVec = transform.right;
                if (hitObj != null)
                {
                    if (hitObj.tag == "Mirror" && hit.collider.gameObject != hitObj)
                    {
                        hitObj.GetComponent<Mirror>().createBeam = false;
                    }
                }
                
                hitObj = hit.collider.gameObject;

                if (createBeam)
                {
                    if (LightBeam == null)
                    {
                        LightBeam = Instantiate(LightObject);
                    }

                    if (hit.collider.gameObject.tag == "Mirror")
                    {
                        hitObj.GetComponent<Mirror>().createBeam = true;
                    }
                }
            }
        }
        else
        {
            if(LightBeam != null)
            {
                Destroy(LightBeam);
                LightBeam = null;
            }

            if (hitObj != null)
            {
                if (hitObj.tag == "Mirror")
                {
                    hitObj.GetComponent<Mirror>().createBeam = false;
                }
            }
        }

        if (LightBeam != null)
        {
            // Update the light beam's position, size, and rotation based on where the ray hits
            Vector3 point = hit.point;
            Vector3 dir = point - startPos;

            mid = new Vector3(((point.x + startPos.x) / 2), ((point.y + startPos.y) / 2), ((point.z + startPos.z) / 2));
            LightBeam.transform.position = mid;
            LightBeam.transform.localScale = new Vector3(dir.magnitude, 1.4f, 0.0f);

            float angle = Vector3.Angle(transform.up, lightOutAngle);
            angle = lightOutAngle.y > 0.0f ? -angle : angle;
            LightBeam.transform.localEulerAngles = (new Vector3(0.0f,0.0f,1.0f) * angle * 2);

            // Update the reflection angle of the hit mirror's light beam
            Vector3 normal = hit.normal;

            if (normal == hit.transform.right && hitObj.tag == "Mirror")
            { 
                reflectAngle = Vector3.Reflect(dir, hit.normal);

                hitObj.GetComponent<Mirror>().lightOutAngle = reflectAngle;
            }
        }
    }
}
