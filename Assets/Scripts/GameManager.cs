using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject fastCritter;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 position = gameObject.transform.position;
        for (int i = 0; i < 5; i++)
        {
            // position.x+1 since position.x is 0
            Instantiate(fastCritter, new Vector2((position.x+1) * Random.Range(-3,3), 
                (position.y+1) * Random.Range(-3,3)),  Quaternion.identity);    
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
