using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    private float maxHP = 5f;
    Vector2 localScale;
    private float maxScale;
    private void Start()
    {
        localScale = transform.localScale;
        maxScale = localScale.x;
    }
    private void Update()
    {
        localScale.x = (GetComponentInParent<EnemyMovement>().hp / maxHP) * maxScale;
        transform.localScale = localScale;
    }
}
