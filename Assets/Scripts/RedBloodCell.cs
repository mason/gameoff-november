using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBloodCell : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void DestroyObjectDelayed()
    {
        Destroy(gameObject, 2);
    }
}
