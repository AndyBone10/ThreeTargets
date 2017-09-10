using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public static ScoreManager instance;
	public int score;
	public int highScore;
	public Text currentHighScoreText;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		score = 0;
		PlayerPrefs.SetInt ("Score", score);
	}

	public void incrementScore(){
		score += 1;
		currentHighScoreText.text = score.ToString ();
	}

	public void setScore(){
		PlayerPrefs.SetInt ("Score", score);
	}

	public void setHighScore(){
		if (PlayerPrefs.HasKey ("HighScore")) {
			if (score > PlayerPrefs.GetInt ("HighScore")) {
				PlayerPrefs.SetInt ("HighScore", score);
			}
		} else {
			PlayerPrefs.SetInt ("HighScore", score);
		}
	}
		
}
