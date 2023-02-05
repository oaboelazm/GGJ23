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

    Vector3[] TreesPos = new Vector3[] { new Vector3(-15.88f, 2.06f, 0), new Vector3(0.18f, 7.36f, 0f), new Vector3(15.0900002f, 3.46000004f, 0), new Vector3(-1.42999995f, -8.19999981f, 0) };
    int etreeIndex;
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = treeSprites[treeIndex];


        int index = 0;
        Vector3 myPos = this.gameObject.transform.position;
        for (int i = 0; i<TreesPos.Length; i++)
        {
            if (Vector3.Distance(myPos, TreesPos[index])>Vector3.Distance(myPos, TreesPos[i]))
            {
                index= i;
            }
        }
        etreeIndex = index;
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
        var LevelManagerObject = GameObject.Find("LevelManagerObject");
        var LevelManagerScript = LevelManagerObject.GetComponent<LevelManager>();
        LevelManagerScript.notifyTreeSpawned(etreeIndex);

        yield return new WaitForSeconds(cooldownDuration); 
        Instantiate(tree , transform.position , Quaternion.identity);
        
        
        Destroy(gameObject);
    }
}
