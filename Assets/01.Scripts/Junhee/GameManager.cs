using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    
    public static GameManager instance = null;

    bool pause;
    public GameObject stoppanel;
    [SerializeField] GameObject Retrypanel;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject _startButton;
    [SerializeField] GameObject _exitButton;
    int score = 0;
    public bool _gameOver = true;

    public int Score
    {
        get { return score; }
        set { score = value; }

    }
    private void Awake()
    {
        instance = this;
    }

    public void AddScore(int point)
    {
        Score = point;
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        scoreText.text = $"{Score:2D}";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause == false)
            {
                Time.timeScale = 0;
                pause = true;
                stoppanel.SetActive(true);
                return;
            }

            if (pause == true)
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
    public void Die()
    {
        _gameOver = true;
        Retrypanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void Retry()
    {
        Score = 0;
        Time.timeScale = 1;
        _gameOver = false;
        SceneManager.LoadScene("Youngseo");
        StartButton();
    }
    public void TtileButton()
    {
        Score = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene("Youngseo");
    }
    public void StartButton()
    {
        SceneManager.LoadScene("Hanul", LoadSceneMode.Additive);
        scoreText.gameObject.SetActive(true);
        _startButton.SetActive(false);
        _exitButton.SetActive(false);
        _gameOver = false;
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
