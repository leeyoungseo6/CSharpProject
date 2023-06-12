using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    bool pause;
    public GameObject stoppanel;
    public static GameManager instance = null;
    [SerializeField] TextMeshProUGUI scoreText;
    int score = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        pause = false;
    }

    
    void Update()
    {
        if(Input.GetKeyDown (KeyCode.Escape))
        {
            if(pause ==false)
            {
                Time.timeScale = 0;
                pause = true;
                stoppanel.SetActive(true);
                return;
            }

            if(pause ==true)
            {
                Time.timeScale = 1;
                pause = false;
                stoppanel.SetActive(false);
                return;
            }
        }
    }
    public void BackButton()
    {
        stoppanel.SetActive(false);
        Time.timeScale = 1;
    }
    public int Score
    {
        get { return score; }
        set
        {
            if (value > 0) score = value;
        }
    }
    public void AddScore(int point=100)
    {
        score += point;
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        scoreText.text = "Score :  " + score;
    }


}
