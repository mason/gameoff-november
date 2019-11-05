using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Critter : MonoBehaviour
{
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

    protected abstract void Move();
}
