﻿using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject whiteBloodCell;
    public GameObject redBloodCell;
    public Text scoreText;

    private int score = 0;
    private int numRedBloodCells = 10;
    private int numWhiteBloodCells = 1;
    private List<CellPosition>[] availableCells = new List<CellPosition>[10];
    private Player player;

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
        InitGame(); // RuntimeInitializeOnLoadMethod called after Awake()
    }

    private void PopulateAvailableCellLocations()
    {
        // maybe do some calculation on camera width
        // TODO hard coded max rows and cols
        // TODO hard coded +=4 meaning the cell is 3 units wide and 1 unit buffer
        for (int row = 0; row < 5; row++)
        {
            for (int col = 0; col < 5; col++)
            {
                if (availableCells[row] == null)
                {
                    availableCells[row] = new List<CellPosition>();
                }

                List<CellPosition> cells = availableCells[row];
                CellPosition cp = new CellPosition((row - 3) * 4, (col - 3) * 5);
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
        // UI
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreText.text = "Score: " + score;

        // White Blood Cells
        CreateWhiteBloodCells();

        // Red Blood Cells
        PopulateAvailableCellLocations();
        CreateRedBloodCells();
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
        if (numRedBloodCells <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void incrementScore()
    {
        score++;
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