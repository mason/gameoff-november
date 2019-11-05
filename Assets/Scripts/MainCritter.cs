using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCritter : Critter
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rigidbody2D.AddForce(direction * 2.0f);
    }

    // Do Nothing
    protected override void Move()
    {
        
    }
}
