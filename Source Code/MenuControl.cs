using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour {

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void Start()
    {
        if(!PlayerPrefs.HasKey("CurrentScore"))
        {
            PlayerPrefs.SetInt("CurrentScore", 0);
        }
        if (!PlayerPrefs.HasKey("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", 0);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        PlayerPrefs.SetInt("CurrentScore", 0);
    }

    public void QuitGame()
    {
        Application.Quit();
        PlayerPrefs.SetInt("CurrentScore", 0);
    }

    public void MenuReturn()
    {
        SceneManager.LoadScene(0);
        PlayerPrefs.SetInt("CurrentScore", 0);
    }
}
