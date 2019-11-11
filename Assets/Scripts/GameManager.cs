using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject whiteBloodCell;
    public int score = 0;
    public Text scoreText;
    public int numRedBloodCells = 1;
    public int numWhiteBloodCells = 1;
    
    private Player player;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
            return; // return so we don't call InitGame() again
        }
        DontDestroyOnLoad(gameObject);
        InitGame(); // RuntimeInitializeOnLoadMethod called after Awake()
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static public void CallbackInitialization()
    {
        //register the callback to be called everytime the scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void InitGame()
    {
        // UI
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreText.text = "Score: " + score;
        
        // White Blood Cells
        CreateWhiteBloodCells();
    }

    private void CreateWhiteBloodCells()
    {
        Vector2 position = gameObject.transform.position;
        for (int i = 0; i < numWhiteBloodCells; i++)
        {
            // position.x+1 since position.x is 0
            Instantiate(whiteBloodCell, new Vector2((position.x + 1) * Random.Range(-3, 3),
                (position.y + 1) * Random.Range(-3, 3)), Quaternion.identity);
        }
    }
    
    //This is called each time a scene is loaded.
    static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        instance.InitGame();
    }
    
    
    // Update is called once per frame
    void Update()
    {
//        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }

    public void DecrementRedBloodCell()
    {
        numRedBloodCells--;
        if (numRedBloodCells <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name) ;
        }
    }

    public void incrementScore()
    {
        score++;
    }

}
