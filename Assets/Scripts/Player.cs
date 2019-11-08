using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Critter
{
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rigidbody2D.AddForce(direction * 2.0f);
        if (Input.GetKeyDown("e"))
        {
            eatMode = true;
            spriteRenderer.color = new Color(255,0,0); 
        } else if (Input.GetKeyUp("e"))
        {
            eatMode = false;
            spriteRenderer.color = new Color(255,255,255); 
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
