using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    private float speed = 5f;
    private bool up = false;
    private bool down = false;
    private Vector2 currentPosition;
    private void Start()
    {
        currentPosition = 
            transform.position;
    }
    private void Update()
    {
        if ((transform.position.y - currentPosition.y) > 1)
            up = true;
        if(up)
           if ((transform.position.y - currentPosition.y) <= 0)
                down = true;
        if (!up)
            transform.position += transform.up * Time.deltaTime * speed;
        if(!down && up)
            transform.position -= transform.up * Time.deltaTime * speed;
        if(up && down)
        {
            GetComponent<Collider2D>().enabled = true;
        }
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject Soundfx;
        Soundfx = GameObject.FindWithTag("sfx");
        Soundfx.GetComponent<SFXSystem>().MakeSound("Collect");
        Destroy(gameObject);
    }
}
