using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FastCritter : Critter
{
    private CircleCollider2D circleCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    protected override void Move()
    {
        
    }
    

    private void OnTriggerStay2D(Collider2D other)
    {
        Critter critter = other.GetComponent<Critter>();
        if (critter != null)
        {
            Rigidbody2D r = other.GetComponent<Rigidbody2D>();
            rigidbody2D.AddForce(r.position * Random.Range(-2.0f, 4.0f));
        }
    }
}
