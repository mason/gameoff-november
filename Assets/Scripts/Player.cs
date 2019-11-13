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
        
        Vector2 directionX = new Vector2(Input.GetAxis("Horizontal"), 0);

        rigidbody2D.AddForce(directionX * 15.0f);
        if (onRedBloodCell && Input.GetKeyDown("space"))
        {
            spriteRenderer.color = new Color(255,0,0);
            rigidbody2D.AddForce(Vector2.up * 600);
        }
        
    }
    
    IEnumerator ReEnableBloodCellCollider(float time, GameObject gameObject)
    {
        yield return new WaitForSeconds(time);
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    void Update()
    {
        Vector2 directionY = new Vector2(0, Input.GetAxis("Vertical"));
        if (onRedBloodCell && Input.GetKeyDown("e"))
        {
            Collider2D redBloodCell = Physics2D.Raycast(transform.position, Vector2.down, 10.0f,
                LayerMask.GetMask("RedBloodCell")).collider;
            if (redBloodCell != null)
            {
                StartCoroutine(redBloodCell.gameObject.GetComponent<RedBloodCell>().DestroyObjectDelayed());
            }
        }
        if (onRedBloodCell && directionY.y < 0)
        {
            RaycastHit2D redBloodCell = Physics2D.Raycast(transform.position, Vector2.down, 10.0f,
                LayerMask.GetMask("RedBloodCell"));
            if (redBloodCell != null)
            {
                redBloodCell.collider.gameObject.GetComponent<Collider2D>().enabled = false;
                StartCoroutine(ReEnableBloodCellCollider(1.0f, redBloodCell.collider.gameObject));
            }

        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("RedBloodCell") || other.gameObject.CompareTag("WhiteBloodCell"))
        {
            onRedBloodCell = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("RedBloodCell") || other.gameObject.CompareTag("WhiteBloodCell"))
        {
            onRedBloodCell = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("RedBloodCell") || other.gameObject.CompareTag("WhiteBloodCell"))
        {
            onRedBloodCell = false;
        }
    }
}
