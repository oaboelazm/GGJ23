using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInv : MonoBehaviour
{
    public int seeds = 0;

    public bool IsPoweredUp = false;

    public void AddItem (string _Item)
    {
        if (_Item == "Seeds")
        {
            seeds++;

        }else if (_Item == "PowerUp")
        {
            IsPoweredUp = true;           
        }
    }
}
