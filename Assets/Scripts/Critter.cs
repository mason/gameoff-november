using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Critter : MonoBehaviour
{
    protected bool eatMode = false;
    protected Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    public void Start()
    {
        
    }
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.drag = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    
    protected void OnCollisionEnter2D(Collision2D other)
    {
//        if (eatMode && other.gameObject.GetComponent<Critter>() != null && !other.gameObject.CompareTag(gameObject.tag))
//        {
//            // take other critters power
//            Critter c = other.gameObject.GetComponent<Critter>();
//            gameObject = Instantiate(c, new Vector2(gameObject.transform.position.x+1 * Random.Range(-3,3), 
//                gameObject.transform.position.y+1 * Random.Range(-3,3)),  Quaternion.identity);  
//        }
    }
    protected void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag(gameObject.tag))
        {
            RunAway(other);
        }
    }

    protected abstract void Move();

    protected abstract void RunAway(Collider2D other);
}
