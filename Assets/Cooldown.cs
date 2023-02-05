using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    private DeadTree dt;
    private TextMeshProUGUI text;

    void Start()
    {
        dt = GetComponentInParent<DeadTree>();
        text = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        if (dt.isSeeded)
        {
            text.text = dt.cooldown.ToString("0");
        }
    }
}
