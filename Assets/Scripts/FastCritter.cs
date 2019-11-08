using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FastCritter : Critter
{
    private CircleCollider2D circleCollider2D;
    private bool runningAway = false;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    protected override void Move()
    {
        if (!runningAway)
        {
            rigidbody2D.AddForce(new Vector2(rigidbody2D.position.x * Random.Range(-2.0f, 2.1f),
                rigidbody2D.position.y * Random.Range(-2.0f, 2.1f)));
        }
    }

    protected override void RunAway(Collider2D other)
    {
        runningAway = true;
        Critter critter = other.GetComponent<Critter>();
        if (critter != null)
        {
            Rigidbody2D r = other.GetComponent<Rigidbody2D>();
            rigidbody2D.AddForce(r.position * Random.Range(-2.0f, 4.0f));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        runningAway = false;
    }
}