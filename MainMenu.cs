using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public AudioSource buttonSound;

	public string playGameLevel;
	public string statsLevel;
	public string charSelectLevel;


	public void PlayGame()
	{
		buttonSound.Play ();
		SceneManager.LoadScene (playGameLevel);
	}

	public void QuitGame()
	{
		buttonSound.Play ();
		Application.Quit ();
	}

	public void ShowStats()
	{
		buttonSound.Play ();
		SceneManager.LoadScene (statsLevel);
	}

	public void CharSelect()
	{
		buttonSound.Play ();
		SceneManager.LoadScene (charSelectLevel);
	}
}
