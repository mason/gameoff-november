using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject whiteBloodCell;
    public int score;
    public Text scoreText;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        Vector2 position = gameObject.transform.position;
        for (int i = 0; i < 5; i++)
        {
            // position.x+1 since position.x is 0
            Instantiate(whiteBloodCell, new Vector2((position.x + 1) * Random.Range(-3, 3),
                (position.y + 1) * Random.Range(-3, 3)), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }

    public void incrementScore()
    {
        score++;
    }

}
