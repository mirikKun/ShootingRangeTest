using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    [SerializeField] private Text highScoreText;
    [SerializeField] private int[] highScore;

    private const string HighScoreKey = "HighScore";
    private int _tableSize;


    private void Start()
    {
        _tableSize = highScore.Length;
        for (int i = 0; i < _tableSize; i++)
        {
            highScore[i] = PlayerPrefs.GetInt(HighScoreKey + (i + 1), 0);
        }
    }

    //Пересоздает таблицу рекаордов по записанным данным
    public void TableSetup()
    {
        highScoreText.text = "HighScore table: \n";
        if (highScore[0] == 0)
            highScoreText.text = "There is no records\n";
        for (int i = 0; i < _tableSize; i++)
        {
            if (highScore[i] == 0)
                continue;
            highScoreText.text += "№ " + (i + 1) + ": ";
            highScoreText.text += highScore[i] + " sec\n";
        }
    }

    //Добавляет значение в таблицу если достаточно мало
    public void AddScore(int time)
    {
        for (int i = 0; i < _tableSize; i++)
        {
            if (highScore[i] > time || highScore[i] == 0)
            {
                int newTime = time;
                time = highScore[i];
                highScore[i] = newTime;
            }
        }

        for (int i = 0; i < _tableSize; i++)
        {
            PlayerPrefs.SetInt(HighScoreKey + (i + 1), highScore[i]);
            PlayerPrefs.Save();
        }
    }

    //Удаляет все данные и пересоздает пустую таблицу
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        for (int i = 0; i < _tableSize; i++)
        {
            highScore[i] = PlayerPrefs.GetInt(HighScoreKey + (i + 1), 0);
        }
        TableSetup();
    }
}