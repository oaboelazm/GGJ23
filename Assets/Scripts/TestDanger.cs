using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDanger : MonoBehaviour
{
    void Update()
    {
        DangerIndicatorManager.OnDangerTriggered.Invoke(transform);
    }
}
