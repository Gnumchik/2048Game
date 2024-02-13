using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControler : MonoBehaviour
{

    public static GameControler instance;
    public static int ticker;

    [SerializeField] GameObject fillPrefabs;
    [SerializeField] Cell[] allCells;

    public static Action<string> slide;
    public int score;
    [SerializeField] Text textScore;

    int isGameOver;
    [SerializeField] GameObject GameOverPanel;

    [SerializeField] int winScore;
    [SerializeField] GameObject winPanel;
    bool hasWon;

    private void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        StartSpawnFill();
        StartSpawnFill();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnFill();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ticker = 0;
            isGameOver = 0;
            slide("w");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ticker = 0;
            isGameOver = 0;
            slide("d");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ticker = 0;
            isGameOver = 0;
            slide("s");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            ticker = 0;
            isGameOver = 0;
            slide("a");
        }
    }

    public void SpawnFill()
    {
        bool isFull = true;
        for(int i = 0; i < allCells.Length; i++)
        {
            if(allCells[i].fill == null)
            {
                isFull = false;
            }
        }

        if(isFull == true)
        {
            return;
        }

        int whichSpawn = UnityEngine.Random.Range(0, allCells.Length);
        if(allCells[whichSpawn].transform.childCount != 0)
        {
            SpawnFill();
            return;
        }
        float chance = UnityEngine.Random.Range(0, 10);
        if(chance < 2)
        {
            return;
        }
        else if(chance < 8)
        {
            GameObject tempFill = Instantiate(fillPrefabs, allCells[whichSpawn].transform);
            Fill tempFillComp = tempFill.GetComponent<Fill>();
            allCells[whichSpawn].GetComponent<Cell>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(2);
        }
        else
        {
            GameObject tempFill = Instantiate(fillPrefabs, allCells[whichSpawn].transform);
            Fill tempFillComp = tempFill.GetComponent<Fill>();
            allCells[whichSpawn].GetComponent<Cell>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(4);
        }
    }



    private void StartSpawnFill()
    {
        int whichSpawn = UnityEngine.Random.Range(0, allCells.Length);
        if (allCells[whichSpawn].transform.childCount != 0)
        {
            SpawnFill();
            return;
        }
            GameObject tempFill = Instantiate(fillPrefabs, allCells[whichSpawn].transform);
            Fill tempFillComp = tempFill.GetComponent<Fill>();
            allCells[whichSpawn].GetComponent<Cell>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(2);
    }

    public void ScoreUpdate(int scoreIn)
    {
        score += scoreIn;
        textScore.text = score.ToString();
    }

    public void GameOverChek()
    {
        isGameOver++;
        if(isGameOver >= 16)
        {
            GameOverPanel.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void WinningCheck(int highestFill)
    {
        if (hasWon)
            return;
        if(highestFill == winScore)
        {
            winPanel.SetActive(true);
            hasWon = true; 
        }
    }
}
