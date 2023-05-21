using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverDisplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    
    private void Start()
    {
        _scoreText.text = "Score: " + PlayerPrefs.GetInt("Score", 0);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
