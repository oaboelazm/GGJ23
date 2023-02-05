using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float TimeUntillSpawn = 1f;
    public float TimeBetweenSpawns = 10f;
    public Transform[] constraints;
    public float radius = 5f;
    public GameObject enemy;
    private GameObject[] trees;
    void Update()
    {
        trees = GameObject.FindGameObjectsWithTag("Tree");

        if (Time.timeSinceLevelLoad > TimeUntillSpawn)
        {
            Spawn();
            TimeUntillSpawn += TimeBetweenSpawns;
        }
    }
    void CalculateSpaceBetweenTwoObjects()
    {

    }

    float FindNearest(GameObject[] trees , Vector3 poistion)
    {
        float maxVal = Mathf.Infinity;
        for (int i = 0; i < trees.Length; i++)
        {
            Vector3 diff = trees[i].transform.position - poistion;
            float absVal = Mathf.Abs(diff.x) + Mathf.Abs(diff.y);
            if (absVal < maxVal)
            {
                maxVal = absVal;
            }
        }
        return maxVal;
    }


    void Spawn()
    {
        bool notOk = true;
        Vector3 NewGeneratedPosition = new(0,0,0);
        while (notOk)
        {
            notOk = false;
            NewGeneratedPosition = new Vector3(
                Random.Range(constraints[0].position.x, constraints[1].position.x)
                , Random.Range(constraints[0].position.y, constraints[1].position.y),
                0);
            float x = FindNearest(trees, NewGeneratedPosition);
            if(x < radius)
            {
                notOk = true;
            }
        }
        Instantiate(enemy, NewGeneratedPosition, Quaternion.identity);
    }
}
