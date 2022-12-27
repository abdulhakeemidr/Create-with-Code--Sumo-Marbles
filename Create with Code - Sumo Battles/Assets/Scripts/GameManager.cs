using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public GameObject gameOverScreen;
    void Awake() 
    {
        instance = this;
    }

    public void GameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void MainButton()
    {
        SceneManager.LoadScene("Menu");
    }
}
