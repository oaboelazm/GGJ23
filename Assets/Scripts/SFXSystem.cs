using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSystem : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip Click;

    public AudioClip Notifiy;

    public AudioClip collect;

    public AudioClip GrowSeeds;

    public AudioClip KillEnemy;

    public AudioClip Warning;

    public AudioClip Lose;

    public AudioClip Damage;

    public AudioClip Scream;

   public void MakeSound(string Type)
   {
        switch (Type)
        {
            case "Collect" :    
                Source.PlayOneShot(collect);
            break;

            case "Notifiy" :    
                Source.PlayOneShot(Notifiy);
            break;

            case "GrowSeeds" :    
                Source.PlayOneShot(GrowSeeds);
            break;

            case "KillEnemy" :    
                Source.PlayOneShot(KillEnemy);
            break;

            case "Warning" :    
                Source.PlayOneShot(Warning);
            break;

            case "Scream" :    
                Source.PlayOneShot(Scream);
            break;

            case "Damage" :    
                Source.PlayOneShot(Damage);
            break;
          
          case "Click" :    
                Source.PlayOneShot(Click);
            break;

            case "Lose" :    
                Source.PlayOneShot(Lose);
            break;

        }
   }
}
