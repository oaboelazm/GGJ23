using System.Collections;
using UnityEngine;

public class DeadTree : MonoBehaviour
{
    public int treeIndex;
    public Sprite[] treeSprites;
    public bool isSeeded = false;
    public GameObject tree;
    public float cooldown = 0;
    public float cooldownDuration = 5f;
    public float currentTime = -1f;
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = treeSprites[treeIndex];
    }

    private void Update()
    {
        if (isSeeded)
        {
            if(currentTime == -1)
            {
                currentTime = Time.time;
            }

            cooldown = currentTime + cooldownDuration - Time.time;
        }        
    }

    public void plantSeed()
    {
        isSeeded = true;
        StartCoroutine("plantTree");
    }


    private IEnumerator plantTree()
    {

        yield return new WaitForSeconds(cooldownDuration); 
        Instantiate(tree , transform.position , Quaternion.identity);
        Destroy(gameObject);
    }
}
