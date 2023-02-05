using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    public float hp = 20f;

    public float damageRate = 0.5f;

    public Sprite[] treeSprites;
    public GameObject deadTree;
    Vector3[] TreesPos = new Vector3[] { new Vector3(-15.88f, 2.06f, 0) , new Vector3(0.18f, 7.36f, 0f) , new Vector3(15.0900002f, 3.46000004f, 0), new Vector3(-1.42999995f, -8.19999981f, 0) };
    
    int treeIndex;
    int etreeIndex;
    GameObject Soundfx;
    private void Start()
    {
        treeIndex = Random.Range(0, treeSprites.Length);
        GetComponent<SpriteRenderer>().sprite = treeSprites[treeIndex];
        
        Soundfx = GameObject.FindWithTag("sfx");
        int index = 0;
        Vector3 myPos = this.gameObject.transform.position;
        for(int i =0;i<TreesPos.Length;i++)
        {
            if(Vector3.Distance(myPos, TreesPos[index])>Vector3.Distance(myPos, TreesPos[i]))
            {
                index= i;
            }
        }
        etreeIndex = index;
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
        var LevelManagerObject = GameObject.Find("LevelManagerObject");
        var LevelManagerScript = LevelManagerObject.GetComponent<LevelManager>();
        LevelManagerScript.notifyTreeAttacked(etreeIndex, hp);
        if (!Soundfx.GetComponent<SFXSystem>().Source.GetComponent<AudioSource>().isPlaying){
        Soundfx.GetComponent<SFXSystem>().MakeSound("Damage");
        }
    }
}
