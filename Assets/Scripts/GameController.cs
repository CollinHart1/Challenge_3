﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
public GameObject[] hazards;
public Vector3 spawnValues;
public int hazardCount;
public float spawnWait;
public float startWait;
public float waveWait;

public Text ScoreText;
public Text GameOverText;
public Text RestartText;
public Text winText;

private bool GameOver;
private bool Restart;
private int score;

void Start()
{
score = 0;
UpdateScore();
StartCoroutine(SpawnWaves());
GameOver = false;
Restart = false;
RestartText.text = "";
GameOverText.text = "";
winText.text = "";
}

void Update (){
  if (Restart){
    if (Input.GetKeyDown (KeyCode.Q)){
      SceneManager.LoadScene("Challenge_3");
    }
  }
  if (Input.GetKey("escape"))
    Application.Quit();
      
}

IEnumerator SpawnWaves()
{
yield return new WaitForSeconds(startWait);
while (true)
{
for (int i = 0; i < hazardCount; i++)
{
  GameObject hazard = hazards[Random.Range (0, hazards.Length)];
Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
Quaternion spawnRotation = Quaternion.identity;
Instantiate(hazard, spawnPosition, spawnRotation);
yield return new WaitForSeconds(spawnWait);
}
yield return new WaitForSeconds(waveWait);

if (GameOver == true){
  RestartText.text = "Press 'Q' to restart";
  Restart = true;
  break;
}
}
}

public void AddScore(int newScoreValue)
{
score += newScoreValue;
UpdateScore();
}

void UpdateScore()
{
ScoreText.text = "Points: " + score;
        if (score >= 100)
          {
            winText.text = "You win!";
            GameOverText.text = "GAME CREATED BY COLLIN HART";
            GameOver = true;
            Restart = true;
           }
      }

public void gameOver()
{
  GameOverText.text = "Game Over";
  GameOver = true;

}


}
