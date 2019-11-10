using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Critter
{
    private bool onRedBloodCell = false;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), 0);
        rigidbody2D.AddForce(direction * 15.0f);
        if (onRedBloodCell && Input.GetKeyDown("space"))
        {
            spriteRenderer.color = new Color(255,0,0);
            rigidbody2D.AddForce(Vector2.up * 400);
        }

        if (onRedBloodCell && Input.GetKeyDown("e"))
        {
            Collider2D redBloodCell = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity,
                LayerMask.GetMask("RedBloodCell")).collider;
            if (redBloodCell != null)
            {
                Destroy(redBloodCell.gameObject);
            }
        }

    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("RedBloodCell"))
        {
            onRedBloodCell = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("RedBloodCell"))
        {
            onRedBloodCell = false;
        }
    }

    // Do Nothing
    protected override void Move()
    {
        
    }
    
    protected override void RunAway(Collider2D other)
    {
        
    }
}
