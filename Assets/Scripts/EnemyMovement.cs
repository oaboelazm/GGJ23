using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject[] trees;
    // Update is called once per frame

    public float MovingSpeed = 1.5f;

    public float TimeBetweenHarms = 2f;
    public float TimeTillHarm = 0f;
    public float TreeDamageRadius = 2f;
    public float hp = 5f;
    public float damageRate = 0.5f;
    public GameObject seedPrefab;
    Rigidbody2D rb;
    public  bool isDamaging = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
         trees = GameObject.FindGameObjectsWithTag("Tree");
        if(trees.Length == 0)
        {
            
            Destroy(gameObject);
        }
        if(hp <= 0f)
        {
            var LevelManagerObject = GameObject.Find("LevelManagerObject");
            var LevelManagerScript = LevelManagerObject.GetComponent<LevelManager>();
            LevelManagerScript.notifyScore();
            Instantiate(seedPrefab , transform.position , Quaternion.identity);
            GameObject Soundfx;
            Soundfx = GameObject.FindWithTag("sfx");
            Soundfx.GetComponent<SFXSystem>().MakeSound("KillEnemy");
            Destroy(gameObject);
        }
       
        GameObject x = FindNearest(trees);
        if((x.transform.position - transform.position).sqrMagnitude > TreeDamageRadius)
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, x.transform.position, MovingSpeed * Time.deltaTime));
            isDamaging = false;
        }
        else
        {
            x.GetComponent<TreeBehaviour>().Damage();
            isDamaging = true;
          //var LevelManagerObject = GameObject.Find("LevelManagerObject");
            //var LevelManagerScript = LevelManagerObject.GetComponent<LevelManager>();
             //LevelManagerScript.notifyTreeAttacked();
        }
        if (isDamaging)
        {
            DangerIndicatorManager.OnDangerTriggered.Invoke(transform);
        }
        // transform.up = x - (Vector2) transform.position;
    }

    GameObject FindNearest(GameObject[] trees)
    {
        float maxVal = Mathf.Infinity;
        int maxIndex = 0;
        Vector3 diff;
        for (int i = 0; i < trees.Length; i++)
        {
            diff = trees[i].transform.position - transform.position;
            if (diff.sqrMagnitude < maxVal)
            {
                maxVal = diff.sqrMagnitude;
                maxIndex = i;
            }
        }
        return trees[maxIndex];
    }

    public void Damage()
    {
        hp -= Time.deltaTime * damageRate;
    }
}
