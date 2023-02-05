using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadTree : MonoBehaviour
{
    public int treeIndex;
    public Sprite[] treeSprites;
    public bool isSeeded = false;
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = treeSprites[treeIndex];
    }


    public void plantSeed()
    {
        isSeeded = true;
        Debug.Log("Tree Is Seeded");
    }
}
