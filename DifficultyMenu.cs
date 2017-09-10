using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Reflection;

public class DifficultyMenu : MonoBehaviour {

	public string easyGameLevel;
	public string normalGameLevel;
	public string mainMenu;
	public AudioSource buttonSound;
	private GameObject translatorPanel;
	private GameObject translatorButton;
	private GameObject conjugatorPanel;
	private GameObject conjugatorButton;
	private GlobalObjects theGlobalObjects;

	void Start(){
		translatorButton = GameObject.Find("TranslatorButton");
		translatorPanel = GameObject.Find("TranslatorPanel");
		conjugatorButton = GameObject.Find("ConjugatorButton");
		conjugatorPanel = GameObject.Find("ConjugatorPanel");
		theGlobalObjects = FindObjectOfType<GlobalObjects> ();

		translatorPanel.gameObject.SetActive (false);
		conjugatorPanel.gameObject.SetActive (false);
	}


	public void EasyChoice()
	{
		buttonSound.Play ();
		translatorPanel.gameObject.SetActive (true);
		translatorButton.gameObject.SetActive (false);
		conjugatorButton.gameObject.SetActive (true);
		conjugatorPanel.gameObject.SetActive (false);
	}

	public void NormalChoice()
	{
		buttonSound.Play ();

		conjugatorPanel.gameObject.SetActive (true);
		conjugatorButton.gameObject.SetActive (false);
		translatorPanel.gameObject.SetActive (false);
		translatorButton.gameObject.SetActive (true);
	
	}

	public void StartPressCon()
	{
		theGlobalObjects.conjugatorPressed = true;
		theGlobalObjects.translatorPressed = false;
		SceneManager.LoadScene ("FlyDodger");
	}

	public void StartPressTran()
	{
		theGlobalObjects.translatorPressed = true;
		theGlobalObjects.conjugatorPressed = false;
		SceneManager.LoadScene ("TenseMenu");
	}

	public void QuitGame()
	{
		buttonSound.Play ();
		SceneManager.LoadScene (mainMenu);
	}

	public void BackButtonClick(){
		buttonSound.Play ();
		SceneManager.LoadScene ("MainMenu");
	}
}
