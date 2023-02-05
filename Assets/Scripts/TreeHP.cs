using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHP : MonoBehaviour
{
    private float maxHP = 20f;
    Vector2 localScale;
    private float maxScale;
    private void Start()
    {
        localScale= transform.localScale;
        maxScale = localScale.x;
    }
    private void Update()
    {
        localScale.x = (GetComponentInParent<TreeBehaviour>().hp / maxHP) * maxScale;
        transform.localScale = localScale;
    }
}
