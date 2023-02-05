using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    public float hp = 20f;

    public float damageRate = 0.5f;

    public Sprite[] treeSprites;
    public GameObject deadTree;
    int treeIndex;
    GameObject Soundfx;
    private void Start()
    {
        treeIndex = Random.Range(0, treeSprites.Length);
        GetComponent<SpriteRenderer>().sprite = treeSprites[treeIndex];
        
        Soundfx = GameObject.FindWithTag("sfx");
    }
    private void Update()
    {
        if (hp <= 0)
        {
            Instantiate(deadTree, transform.position, Quaternion.identity);
            deadTree.GetComponent<DeadTree>().treeIndex = treeIndex;
            GameObject Soundfx;
        Soundfx = GameObject.FindWithTag("sfx");
        Soundfx.GetComponent<SFXSystem>().MakeSound("Scream");

            Destroy(gameObject);
        }
    }
    public void Damage()
    {
        hp-= Time.deltaTime * damageRate;
       
        if(!Soundfx.GetComponent<SFXSystem>().Source.GetComponent<AudioSource>().isPlaying){
        Soundfx.GetComponent<SFXSystem>().MakeSound("Damage");
        }
    }
}
