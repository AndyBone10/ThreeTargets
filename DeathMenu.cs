using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

	public string mainMenuLevel;
	public AudioSource buttonSound;

	public void RestartGame()
	{
		buttonSound.Play ();
		FindObjectOfType<GameManager>().ResetGame();
	}

	public void QuitToMain()
	{
		buttonSound.Play ();
		SceneManager.LoadScene (mainMenuLevel);
	}

	public void ShowStats(){
		buttonSound.Play ();
		SceneManager.LoadScene ("HighScoreV&E");
	}
}
