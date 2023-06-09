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
    [SerializeField] TextMeshProUGUI _titleText;
    [SerializeReference] TextMeshProUGUI _highScoreText;
    int score = 0;
    int startScore = 0;
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

    private void Start()
    {
        PlayerPrefs.SetInt("TopScore", SceneVariable.highScore);
        _highScoreText.text = $"{PlayerPrefs.GetInt("TopScore"):D2}";
        scoreText.gameObject.SetActive(false);
    }
    public void UpdateScoreText(int score)
    {
        Score = score - startScore;
        if (Score > SceneVariable.highScore)
        {
            SceneVariable.highScore = Score;
            PlayerPrefs.SetInt("TopScore", SceneVariable.highScore);
            _highScoreText.text = $"{PlayerPrefs.GetInt("TopScore"):D2}";
        }
        scoreText.text = $"{Score:D2}";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_gameOver)
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
    public void TtileButton()
    {
        _gameOver = true;
        Score = 0;
        Time.timeScale = 1;
        _titleText.gameObject.SetActive(true);
        SceneManager.LoadScene("Youngseo");
    }
    public void StartButton()
    {
        startScore = Score;
        _titleText.gameObject.SetActive(false);
        PlayerPrefs.SetInt("TopScore", SceneVariable.highScore);
        _highScoreText.text = $"{PlayerPrefs.GetInt("TopScore"):D2}";
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
