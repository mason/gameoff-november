using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Critter : MonoBehaviour
{
    private bool runningTowards = false;
    private Rigidbody2D rigidbody2D;
    
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.drag = 0;
    }

    private void FixedUpdate()
    {
        Move(); // random movements
    }

    void Move()
    {
        if (!runningTowards)
        {
            rigidbody2D.AddForce(new Vector2(rigidbody2D.position.x * Random.Range(-3.0f, 3.0f),
                rigidbody2D.position.y * Random.Range(-3.0f, 3.0f)));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(gameObject.tag))
        {
            RunTowards(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        runningTowards = false;
    }
    
    void RunTowards(GameObject other)
    {
        runningTowards = true;
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            Rigidbody2D r = other.GetComponent<Rigidbody2D>();
            rigidbody2D.AddForce(r.position * Random.Range(-1.0f, 2.0f));
        }
    }
}
