using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorScript : MonoBehaviour
{
    public float startRotation;
    public GameObject mirror;

    Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        mirror.gameObject.transform.Rotate(mirror.gameObject.transform.forward, startRotation);
        originalRotation = mirror.gameObject.transform.rotation;
    }

    public void Reset()
    {
        mirror.gameObject.transform.rotation = originalRotation;
    }
}
