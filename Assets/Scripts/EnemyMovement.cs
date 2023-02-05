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
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

        if(hp <= 0f)
        {
            Instantiate(seedPrefab , transform.position , Quaternion.identity);
            GameObject Soundfx;
            Soundfx = GameObject.FindWithTag("sfx");
            Soundfx.GetComponent<SFXSystem>().MakeSound("KillEnemy");
            Destroy(gameObject);
        }
        trees = GameObject.FindGameObjectsWithTag("Tree");

        GameObject x = FindNearest(trees);
        if((x.transform.position - transform.position).sqrMagnitude > TreeDamageRadius) { 
            rb.MovePosition(Vector2.MoveTowards(transform.position, x.transform.position, MovingSpeed * Time.deltaTime));
        }
        else
        {
           x.GetComponent<TreeBehaviour>().Damage();       
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
