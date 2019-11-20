using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBloodCell : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public IEnumerator DestroyObjectDelayed()
    {
        animator.SetBool("Kill", true);
        yield return new WaitForSeconds(2);
        GameManager.instance.DecrementRedBloodCell();
        GameManager.instance.scoreText.text = "Score: " + GameManager.instance.Score;
        Destroy(gameObject);
    }
}
