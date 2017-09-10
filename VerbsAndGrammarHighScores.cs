using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class VerbsAndGrammarHighScores : MonoBehaviour {

	public Text easyPresent;
	public Text easyPast;
	public Text easyFuture;
	public Text easyMixed;
	public Text present;
	public Text past;
	public Text future;
	public Text mixed;
	public Text conjugator;
	public GameObject Translator;
	public GameObject Conjugator;
	public AudioSource buttonSound;


	// Use this for initialization
	void Start (){

		Conjugator.SetActive (false);

		if (PlayerPrefs.HasKey ("EasyPresentTense")) {
			easyPresent.text = PlayerPrefs.GetInt("EasyPresentTense").ToString();
		}
		if (PlayerPrefs.HasKey ("EasyPastTense")) {
			easyPast.text = PlayerPrefs.GetInt("EasyPastTense").ToString();
		}
		if (PlayerPrefs.HasKey ("EasyFutureTense")) {
			easyFuture.text = PlayerPrefs.GetInt("EasyFutureTense").ToString();
		}
		if (PlayerPrefs.HasKey ("EasyMixedTense")) {
			easyMixed.text = PlayerPrefs.GetInt("EasyMixedTense").ToString();
		}
		if (PlayerPrefs.HasKey ("PresentTense")) {
			present.text = PlayerPrefs.GetInt("PresentTense").ToString();
		}
		if (PlayerPrefs.HasKey ("PastTense")) {
			past.text = PlayerPrefs.GetInt("PastTense").ToString();
		}
		if (PlayerPrefs.HasKey ("FutureTense")) {
			future.text = PlayerPrefs.GetInt("FutureTense").ToString();
		}
		if (PlayerPrefs.HasKey ("MixedTense")) {
			mixed.text = PlayerPrefs.GetInt("MixedTense").ToString();
		}
		if (PlayerPrefs.HasKey ("FlyGameScore")) {
			conjugator.text = PlayerPrefs.GetInt("FlyGameScore").ToString();
		}
	}

	public void ConjugatorPressed()
	{
		buttonSound.Play ();
		Translator.SetActive(false);
		Conjugator.SetActive(true);
	}

	public void TranslatorPressed()
	{
		buttonSound.Play ();
		Conjugator.SetActive(false);
		Translator.SetActive(true);
	}

	public void BackButtonClick(){
		buttonSound.Play ();
		SceneManager.LoadScene ("MainMenu");
	}
}
