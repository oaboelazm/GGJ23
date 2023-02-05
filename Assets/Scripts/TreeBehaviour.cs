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
    private void Start()
    {
        treeIndex = Random.Range(0, treeSprites.Length);
        GetComponent<SpriteRenderer>().sprite = treeSprites[treeIndex];
    }
    private void Update()
    {
        if (hp <= 0)
        {
            Instantiate(deadTree, transform.position, Quaternion.identity);
            deadTree.GetComponent<DeadTree>().treeIndex = treeIndex;
            Destroy(gameObject);
        }
    }
    public void Damage()
    {
        hp-= Time.deltaTime * damageRate;
    }
}
