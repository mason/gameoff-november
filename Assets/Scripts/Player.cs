using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    protected Rigidbody2D rigidbody2D;
    private bool onRedBloodCell = false;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
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
            rigidbody2D.AddForce(Vector2.up * 600);
        }
    }

    void Update()
    {
        if (onRedBloodCell && Input.GetKeyDown("e"))
        {
            Collider2D redBloodCell = Physics2D.Raycast(transform.position, Vector2.down, 10.0f,
                LayerMask.GetMask("RedBloodCell")).collider;
            if (redBloodCell != null)
            {
                GameManager.instance.incrementScore();
                redBloodCell.gameObject.GetComponent<RedBloodCell>().DestroyObjectDelayed();
                GameManager.instance.DecrementRedBloodCell();
                GameManager.instance.scoreText.text = "Score: " + GameManager.instance.Score;
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

    private void OnCollisionEnter2D(Collision2D other)
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
}
