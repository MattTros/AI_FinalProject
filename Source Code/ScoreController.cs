using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public Text scoreText;
    public Text highscoreText;

	// Use this for initialization
	void Start ()
    {
        scoreText.text = "YOUR SCORE: " + PlayerPrefs.GetInt("CurrentScore").ToString();
        highscoreText.text = "HIGHSCORE: " + PlayerPrefs.GetInt("Highscore").ToString();
    }
}
