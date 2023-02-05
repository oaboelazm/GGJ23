using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 100f;
    [Range (1f, 10f)]
    public float maxCmaeraZoom = 4.5f;
    [Range (1f, 10f)]
    public float minCameraZoom = 2.5f;
    private float cameraZoomFactor = (1f / 5f);
    Camera cam;
    private GameObject[] trees;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        trees = GameObject.FindGameObjectsWithTag("Tree");
        Vector3 desiredPoisition = player.position - new Vector3(0, 0, 10);
        Vector3 newPosition = Vector3.Lerp(transform.position, desiredPoisition, speed * Time.deltaTime);
        transform.position = newPosition;

        float nearestTreePosition = FindNearest(trees);
        if (nearestTreePosition > 1 && nearestTreePosition < 10)
        {
          //  Debug.Log(cameraZoomFactor * nearestTreePosition);

            float val =  Mathf.Lerp(cam.orthographicSize, (cameraZoomFactor * nearestTreePosition) + minCameraZoom, speed * Time.deltaTime);
            cam.orthographicSize = val;

        }
    }

        float FindNearest(GameObject[] trees)
    {
        float maxVal = 10f;
        for (int i = 0; i< trees.Length; i++)
        {
            Vector3 diff = trees[i].transform.position - transform.position;
            float absVal = Mathf.Abs(diff.x) + Mathf.Abs(diff.y);
            if (absVal < maxVal)
            {
                maxVal= absVal;
            }
        }
        return maxVal;
    }
}
