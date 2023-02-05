using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 10f;
    private Rigidbody2D rb;
    public float EnemyAudioRadius = 20f;

    public float EnemyDamageRadius = 5f;
    private GameObject[] enemies;
    private GameObject[] deadTrees;
    public bool nearDeadTree = false;
    public GameObject nearestDeadTree = null;
    public int seeds = 0;
    EnemyMovement em;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 x = Input.GetAxis("Horizontal") * Vector2.right * speed * Time.fixedDeltaTime;
        Vector2 y = Input.GetAxis("Vertical") * Vector2.up * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + x + y);
    }


    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        deadTrees = GameObject.FindGameObjectsWithTag("DeadTree");
        enemies = FindAllNearest(enemies);
        foreach(GameObject enemy in enemies)
        {

            em = enemy.GetComponent<EnemyMovement>();
            if (em.isDamaging)
            {
                // Run Damage Voice
            }
            if ((enemy.transform.position - transform.position).sqrMagnitude < EnemyDamageRadius)
            {
                em.Damage();
            }
        }
        if (deadTrees.Length > 0) {
            nearestDeadTree = FindNearest(deadTrees);
            float x = (nearestDeadTree.transform.position - transform.position).sqrMagnitude;
            if (x < 2f)
                nearDeadTree = true;
            else
                nearDeadTree = false;
        }
    }

    GameObject FindNearest(GameObject[] trees)
    {
        Vector3 diff;
        float maxVal = Mathf.Infinity;
        int treeIndex = 0;
        for (int i = 0; i < enemies.Length; i++)
        {
            diff = trees[i].transform.position - transform.position;
            if (diff.sqrMagnitude < maxVal)
            {
                maxVal = diff.sqrMagnitude;
                treeIndex = i;
            }
        }
        return trees[treeIndex];
    }


    GameObject[] FindAllNearest(GameObject[] enemies)
    {
        Vector3 diff;
        List<GameObject> nearEnemies = new List<GameObject>();
        for (int i = 0; i < enemies.Length; i++)
        {
            diff = enemies[i].transform.position - transform.position;
            if (diff.sqrMagnitude < EnemyAudioRadius)
            {
                nearEnemies.Add(enemies[i]);
            }
        }
        return nearEnemies.ToArray();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        seeds++;
    }
}
