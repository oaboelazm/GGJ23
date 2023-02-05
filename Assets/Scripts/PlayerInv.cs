using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInv : MonoBehaviour
{

    public Button SeedsButton;
    public int seedLimit = 10;

    public int seeds = 0;

    public bool IsPoweredUp = false;

    void Start() 
    {
        SeedsButton.interactable = false;
    }
    void Update() 
    {
       if (seeds == seedLimit)
        {
            SeedsButton.interactable = true;
            // make dialog to notifiy the player
        }
    }
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

    public void PutSeeds ()
    {   
        // get dead tree and revive it
    }
}
