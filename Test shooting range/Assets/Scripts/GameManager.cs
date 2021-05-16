using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int objective = 10;
    [SerializeField] private Timer timer;
    [SerializeField] private TargetSpawner targetSpawner;
    [SerializeField] private Menu menu;
    [SerializeField] private Text goal;
    private String _goalText;

    public static GameManager GM;

    //Создает ссылку ну игровой менеджер
    void Awake()
    {
        if (GM == null)
            GM = GetComponent<GameManager>();
    }

    private void Start()
    {
        _goalText = goal.text;
        SetGoalText();
    }

    public int GetObjective()
    {
        return objective;
    }

    public void Victory()
    {
        menu.GameEnd("Victory: " + timer.GetTime(), timer.GetTime());
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SetGoalText()
    {
        goal.text = _goalText + objective;
    }
}