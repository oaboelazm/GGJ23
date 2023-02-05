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

    public PlayerMovement pm;
    private DeadTree dt;

    void Start()
    {
        SeedsButton.interactable = false;
    }
    void Update() 
    {
        seeds = pm.seeds;
        if (seeds == seedLimit)
        {
           // SeedsButton.interactable = true;

            // make dialog to notifiy the player Omar
        }
        if (pm.nearDeadTree && seeds >= seedLimit)
        {
            dt = pm.nearestDeadTree.GetComponent<DeadTree>();
            if (!dt.isSeeded)
                SeedsButton.interactable = true;
            else
                SeedsButton.interactable = false;
        }


    }
    public void AddItem (string _Item)
    {
       if (_Item == "PowerUp")
        {
            IsPoweredUp = true;           
        }
    }

    public void PutSeeds ()
    {
                dt.plantSeed();
                pm.seeds -= seedLimit;
        GameObject Soundfx;
        Soundfx = GameObject.FindWithTag("sfx");
        Soundfx.GetComponent<SFXSystem>().MakeSound("GrowSeeds");
    }
}
