using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keep : MonoBehaviour
{
    void Awake()
     {
     DontDestroyOnLoad(this.gameObject);
   }
}
