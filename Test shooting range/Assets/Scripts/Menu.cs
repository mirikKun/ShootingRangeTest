using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject highScoreUI;
    [SerializeField] private Text endGameResult;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject restartButton;

    private static bool _gameInPaused;
    private HighScoreTable _highScoreTable;
    private bool _gameEnded;

    private void Start()
    {
        _highScoreTable = GetComponent<HighScoreTable>();
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    //Логика книпки Esc
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape) || _gameEnded) return;
        if (_gameInPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

   
    public void Exit()
    {
        Application.Quit();
    }
    
    //Переход из основного меню в меню рекордов
    public void OpenHighScoreTable()
    {
        _highScoreTable.TableSetup();
        highScoreUI.SetActive(true);
        mainMenuUI.SetActive(false);
    }

    //Переход из меню рекордов в основное
    public void OpenMainMenu()
    {
        highScoreUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }
    
    //Закрывает меню паузы
    public void Resume()
    {
        Time.timeScale = 1;
        OpenMainMenu();
        pauseMenuUI.SetActive(false);
        _gameInPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //Открывает меню паузы
    private void Pause()
    {
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
        _gameInPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //Открывает окно окончания игры
    public void GameEnd(String result,int score)
    {
        _gameEnded = true;
        _highScoreTable.AddScore(score);
        Pause();
        OpenHighScoreTable();
        backButton.SetActive(false);
        restartButton.SetActive(true);
        endGameResult.text = result;
        
    }

    public void ResetTable()
    {
        _highScoreTable.Reset();
    }

    public void Restart()
    {
       GameManager.GM.Restart();
    }
}
