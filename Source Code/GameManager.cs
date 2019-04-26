using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    GameObject[] spawnPoints;

    public GameObject chaserEnemy;
    public GameObject missileEnemy;
    public GameObject mineEnemy;
    public Text scoreText;

    private float elapsedTime;

    GameObject[] enemyTypes = new GameObject[3];

    private int score;

	// Use this for initialization
	void Start ()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawner");
        StartCoroutine(SpawnTimer(3.0f));
        enemyTypes[0] = chaserEnemy;
        enemyTypes[1] = missileEnemy;
        enemyTypes[2] = mineEnemy;
    }
	
	// Update is called once per frame
	void Update ()
    {
        elapsedTime += Time.deltaTime;
    }

    private IEnumerator SpawnTimer(float waitTime)
    {
        while (true)
        {
            waitTime = Random.Range(1.0f, 3.0f);
            yield return new WaitForSeconds(waitTime);
            int randSpawnAmount = Random.Range(1, 4);
            for (int i = 0; i < randSpawnAmount; i++)
            {
                int randSpawn = Random.Range(0, 8);
                int randEnemyType = 0;
                if (elapsedTime < 10.0f)
                {
                    randEnemyType = Seconds0State();
                }
                else if(elapsedTime > 10.0f && elapsedTime < 20.0f)
                {
                    randEnemyType = Seconds10State();
                }
                else if(elapsedTime > 20.0f)
                {
                    randEnemyType = Seconds20State();
                }
                GameObject tmpEnemy = Instantiate(enemyTypes[randEnemyType], spawnPoints[randSpawn].transform.position, Quaternion.identity);
            }
        }
    }

    private int Seconds0State()
    {
        return 0;
    }

    private int Seconds10State()
    {
        return Random.Range(0, 2);
    }

    private int Seconds20State()
    {
        return Random.Range(0, 3);
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int score_)
    {
        score = score_;
        scoreText.text = score.ToString();
        PlayerPrefs.SetInt("CurrentScore", score);
        if(PlayerPrefs.HasKey("Highscore"))
        {
            if(score > PlayerPrefs.GetInt("Highscore"))
            {
                PlayerPrefs.SetInt("Highscore", score);
            }
        }
        else
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
    }
}
