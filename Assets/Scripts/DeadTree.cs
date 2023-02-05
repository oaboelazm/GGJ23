using System.Collections;
using UnityEngine;

public class DeadTree : MonoBehaviour
{
    public int treeIndex;
    public Sprite[] treeSprites;
    public bool isSeeded = false;
    public GameObject tree;
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = treeSprites[treeIndex];
    }


    public void plantSeed()
    {
        isSeeded = true;
        StartCoroutine("plantTree");
    }


    private IEnumerator plantTree()
    {

        yield return new WaitForSeconds(3f); 
        Instantiate(tree , transform.position , Quaternion.identity);
        Destroy(tree);
    }
}
