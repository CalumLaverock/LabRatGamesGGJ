using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    GameObject hitObj;

    GameObject LightBeam;
    public GameObject LightObject;

    public Vector3 reflectAngle;
    public Vector3 norm;

    // Start is called before the first frame update
    void Start()
    {
        LightBeam = Instantiate(LightObject);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit;

        float width = GetComponent<BoxCollider2D>().bounds.size.x + 0.001f;
        Vector3 startPos = transform.position + (transform.right * (width / 2));

        if (hit = Physics2D.Raycast(startPos, transform.right))
        {
            Debug.DrawRay(startPos, transform.right);

            if (!hit.collider.isTrigger)
            {
                if (hitObj != null)
                {
                    Vector3 normal = hit.point;
                    norm = normal.normalized;

                    // if the light beam hits a different object then set the previous objects createBeam flag to false
                    if (hitObj.tag == "Mirror" && (hit.collider.gameObject != hitObj || normal.normalized != hit.transform.right))
                    {
                        hitObj.GetComponent<Mirror>().createBeam = false;
                    }
                }   

                // store the object hit by the light beam
                hitObj = hit.collider.gameObject;

                if (hit.collider.gameObject.tag == "Mirror")
                {
                    hitObj.GetComponent<Mirror>().createBeam = true;
                }
            }
        }
        else
        {
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

            if (normal.normalized == hit.transform.right && hitObj.tag == "Mirror")
            {
                reflectAngle = Vector3.Reflect(dir, hit.normal);

                hitObj.GetComponent<Mirror>().lightOutAngle = reflectAngle;
                hitObj.GetComponent<Mirror>().norm = normal;
            }
        }
    }
}
