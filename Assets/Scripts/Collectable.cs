using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Kind KindOfOBJ;

    GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
   private void OnTriggerEnter2D(Collider2D other) 
   {
    if (other.tag == "Player")
    {
        Player.GetComponent<PlayerInv>().AddItem(KindOfOBJ.ToString());
        notifyLevelManagerSeeds();
        }
   }
    
    public enum Kind
    {
        Seeds,
        PowerUp,
    }
    public void notifyLevelManagerSeeds()
    {
        GameObject LevelManagerObject = GameObject.Find("LevelManagerObject");
        LevelManagerObject.GetComponent<LevelManager>().notifySeedOptained();
    }
}

