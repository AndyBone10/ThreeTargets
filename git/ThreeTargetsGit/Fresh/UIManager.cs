using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public static UIManager instance;

	public GameObject gameOverPanel;
	public GameObject tapText;
	public Text score;
	public Text highScore1;
	public Text highScore2;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}

	public void GameStart(){
		tapText.SetActive (false);
	}

	public void homeButtonPressed(){
		SceneManager.LoadScene ("MainMenu");
	}

	public void GameOver(){
		gameOverPanel.SetActive (true);
		gameOverPanel.gameObject.transform.GetChild (0).GetComponent<SocialFace> ().ChangeButtonImage ();
		ScoreManager.instance.setScore ();
		ScoreManager.instance.setHighScore ();
		highScore1.text = ScoreManager.instance.score.ToString();
		highScore2.text = PlayerPrefs.GetInt ("HighScore").ToString();
	}

	public void Reset(){
		SceneManager.LoadScene ("Scene1");
		PlayerPrefs.SetInt ("RestartCheck", 1);
	}

}
