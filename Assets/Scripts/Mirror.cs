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
    Vector3 reflectAngle;

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

        transform.Rotate(new Vector3(0.0f, 0.0f, 1.0f), vert * 0.05f);

        if(!createBeam && LightBeam != null)
        {
            Destroy(LightBeam);
            LightBeam = null;
        }

        RaycastHit2D hit;

        float width = GetComponent<BoxCollider2D>().bounds.size.x + 0.001f;
        Vector3 startPos = transform.position + (transform.right * (width / 2));

        if (hit = Physics2D.Raycast(startPos, lightOutAngle))
        {
            if (!hit.collider.isTrigger)
            {
                if (hitObj != null)
                {
                    if (hit.collider.gameObject != hitObj && hitObj.tag == "Mirror")
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

            Vector3 mid = new Vector3(((point.x + startPos.x) / 2), ((point.y + startPos.y) / 2), ((point.z + startPos.z) / 2));
            LightBeam.transform.position = mid;
            LightBeam.transform.localScale = new Vector3(dir.magnitude, 1.4f, 0.0f);
            LightBeam.transform.eulerAngles = transform.localEulerAngles;

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
