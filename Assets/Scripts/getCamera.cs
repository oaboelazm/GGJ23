using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getCamera : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = GameObject.FindObjectOfType<Camera>();
        GetComponent<Canvas>().worldCamera = cam;

    }
}
