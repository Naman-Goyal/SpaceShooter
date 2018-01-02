using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	public Vector3 spawnVals;
	public GameObject hazard;

	public int hazardsInWave;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public float restartWait;

	private static Text scoreText;
	private static int score;

	private static bool gameOver;
	private static Text restartText;
	private static Text gameOverText;

	// Use this for initialization
	void Start () {
		scoreText = GameObject.Find ("ScoreText").GetComponent<Text>();
		score = 0;
		SetScore ();

		gameOver = false;
		gameOverText = GameObject.Find ("GameOverText").GetComponent<Text>();
		restartText = GameObject.Find ("RestartText").GetComponent<Text>();
		gameOverText.text = "";
		restartText.text = "";

		StartCoroutine (RunGame());
	}

	void Update(){
		if (gameOver) {
			if (Input.GetKeyDown (KeyCode.R)) {
				SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
			}
		}
	}

	IEnumerator RunGame() {
		yield return new WaitForSeconds (startWait);
		bool firstTime = true;
		while (!gameOver) {
			if (firstTime)
				firstTime = false;
			else 
				yield return new WaitForSeconds (restartWait);
			
			for (int i = 0; i < hazardsInWave; i++) {
				Vector3 spawnPt = new Vector3 (Random.Range (-spawnVals.x, spawnVals.x), spawnVals.y, spawnVals.z);
				Instantiate (hazard, spawnPt, Quaternion.identity);
				yield return new WaitForSeconds (spawnWait);
			}
		}

		yield return new WaitForSeconds (restartWait);
		restartText.text = "Press 'R' to Restart";
	}

	public static void AddScore(int toAdd) {
		score += toAdd; 
		SetScore ();
	}

	private static void SetScore() {
		scoreText.text = "Score: " + score;
	}

	public static void GameOver() {
		gameOver = true;
		gameOverText.text = "Game Over!";
	}
}
