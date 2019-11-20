using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject whiteBloodCell;
    public GameObject redBloodCell;
    public GameObject critter;
    public Text scoreText;

    private int score = 0;
    private int maxRedBloodCells = 10;
    private int numRedBloodCells = 10;
    private int numWhiteBloodCells = 7;
    private int numCritters = 7;
    private List<CellPosition>[] availableCells = new List<CellPosition>[10];
    private Player player;
    private Random rnd = new Random();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return; // return so we don't call InitGame() again
        }

        DontDestroyOnLoad(gameObject);
    }

    private void PopulateAvailableCellLocations()
    {
        // maybe do some calculation on camera width
        // TODO hard coded max rows and cols
        // TODO hard coded +=4 meaning the cell is 3 units wide and 1 unit buffer
        for (int row = 0; row < 5; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                if (availableCells[row] == null)
                {
                    availableCells[row] = new List<CellPosition>();
                }

                List<CellPosition> cells = availableCells[row];
                // TODO +3 and +5 are size of red blood cell. remove hard coding
                int randomRow = ((row * 5) - 10) + Random.Range(-2, 2);
                int randomCol = ((col * 7) - 10) + Random.Range(-3, 3);
                CellPosition cp = new CellPosition(randomCol, randomRow);
                cells.Add(cp);
            }
        }
    }

    private CellPosition GetRandomCell(int row)
    {
        int randomCell = Random.Range(0, availableCells[row].Count);
        CellPosition cp = availableCells[row][randomCell];
        availableCells[row].RemoveAt(randomCell);
        return cp;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static public void CallbackInitialization()
    {
        //register the callback to be called everytime the scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void InitGame()
    {
        // Reset variables
        numRedBloodCells = maxRedBloodCells;
        
        // UI
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreText.text = "Score: " + score;

        // White Blood Cells
        CreateWhiteBloodCells();

        // Red Blood Cells
        PopulateAvailableCellLocations();
        CreateRedBloodCells();
        
        // Critters
        CreateCritters();
    }

    private void CreateCritters()
    {
        Vector2 position = gameObject.transform.position;
        for (int i = 0; i < numCritters; i++)
        {
            IList<int> randomX = new List<int>()
            {
                Random.Range(Convert.ToInt32(position.x -3), Convert.ToInt32(position.x -13)),
                Random.Range(Convert.ToInt32(position.x +3), Convert.ToInt32(position.x +13)),
            };
            
            IList<int> randomY = new List<int>()
            {
                Random.Range(Convert.ToInt32(position.y -3), Convert.ToInt32(position.y -13)),
                Random.Range(Convert.ToInt32(position.y +3), Convert.ToInt32(position.y +13)),
            };
            // position.x+1 since position.x is 0
            Instantiate(critter, new Vector2(randomX[Random.Range(0,1)], 
                randomY[Random.Range(0,1)]), Quaternion.identity);
        }
    }

    private void CreateWhiteBloodCells()
    {
        Vector2 position = gameObject.transform.position;
        for (int i = 0; i < numWhiteBloodCells; i++)
        {
            IList<int> randomX = new List<int>()
            {
                Random.Range(Convert.ToInt32(position.x -3), Convert.ToInt32(position.x -13)),
                Random.Range(Convert.ToInt32(position.x +3), Convert.ToInt32(position.x +13)),
            };
            
            IList<int> randomY = new List<int>()
            {
                Random.Range(Convert.ToInt32(position.y -3), Convert.ToInt32(position.y -13)),
                Random.Range(Convert.ToInt32(position.y +3), Convert.ToInt32(position.y +13)),
            };
            // position.x+1 since position.x is 0
            Instantiate(whiteBloodCell, new Vector2(randomX[Random.Range(0,1)], 
                randomY[Random.Range(0,1)]), Quaternion.identity);
        }
    }

    private void CreateRedBloodCells()
    {
        for (int i = 0; i < numRedBloodCells; i++)
        {
            // TODO use cols instead of i%3
            CellPosition cp = GetRandomCell(i % 4);
            Instantiate(redBloodCell, new Vector2(cp.Row,
                cp.Col), Quaternion.identity);
        }
    }

    //This is called each time a scene is loaded.
    static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        instance.InitGame();
    }

    public void DecrementRedBloodCell()
    {
        numRedBloodCells--;
        score++;
        if (numRedBloodCells < 0) // < 0 because the platform for our player
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    public void ResetGame()
    {
        score = 0;
        SceneManager.LoadScene("MainScene");
    }

    public int Score
    {
        get => score;
    }

    class CellPosition
    {
        private int row;
        private int col;

        public CellPosition(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public int Row
        {
            get => row;
            set => row = value;
        }
        public int Col
        {
            get => col;
            set => col = value;
        }
    }
}