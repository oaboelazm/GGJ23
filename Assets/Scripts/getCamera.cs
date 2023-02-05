using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getCamera : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = GameObject.FindAnyObjectByType<Camera>();
        GetComponent<Canvas>().worldCamera = cam;

    }
}
