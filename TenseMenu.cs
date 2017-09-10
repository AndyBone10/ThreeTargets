using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TenseMenu : MonoBehaviour {

	public string playGameLevel;
	public string mainMenu;
	private GlobalObjects theGlobalObjects;
	public AudioSource buttonSound;
	private string tense, type, difficulty;
	private UnityEngine.UI.Dropdown difficultyDropdown;
	private UnityEngine.UI.Dropdown verbTypeDropdown;
	private UnityEngine.UI.Dropdown verbTenseDropdown;


	void Start(){
		theGlobalObjects = FindObjectOfType<GlobalObjects> ();
		difficultyDropdown = GameObject.Find("DifficultyDropdown").GetComponent<UnityEngine.UI.Dropdown>();
		verbTypeDropdown = GameObject.Find ("VerbTypeDropdown").GetComponent<UnityEngine.UI.Dropdown>();
		verbTenseDropdown = GameObject.Find ("VerbTenseDropdown").GetComponent<UnityEngine.UI.Dropdown>();

		difficulty = difficultyDropdown.captionText.text;
		type = verbTypeDropdown.captionText.text;
		tense = verbTenseDropdown.captionText.text;

	}

	public void DifficultyChange(){
		buttonSound.Play ();
		difficulty = difficultyDropdown.captionText.text;
		Debug.Log (difficulty);
	}

	public void TypeChange(){
		buttonSound.Play ();
		type = verbTypeDropdown.captionText.text;
		Debug.Log (type);
	}

	public void TenseChange(){
		buttonSound.Play ();
		tense = verbTenseDropdown.captionText.text;
		Debug.Log (tense);
	}


	public void MixedTense()
	{
		buttonSound.Play ();
		theGlobalObjects.mixed = true;
		SceneManager.LoadScene (playGameLevel);
	}

	public void PresentTense()
	{
		buttonSound.Play ();
		theGlobalObjects.present = true;
		SceneManager.LoadScene (playGameLevel);
	}

	public void PastTense()
	{
		buttonSound.Play ();
		theGlobalObjects.past = true;
		SceneManager.LoadScene (playGameLevel);
	}

	public void FutureTense()
	{
		buttonSound.Play ();
		theGlobalObjects.future = true;
		SceneManager.LoadScene (playGameLevel);
	}


	public void IrregularVerbs()
	{
		buttonSound.Play ();
		SceneManager.LoadScene (playGameLevel);
	}

	public void StartGame()
	{
		buttonSound.Play ();
		if (tense == "All Tenses") {	
			theGlobalObjects.mixed = true;
			theGlobalObjects.present = false;
			theGlobalObjects.past = false;
			theGlobalObjects.future = false;
		} else if (tense == "Present Tense") {
			theGlobalObjects.present = true;
			theGlobalObjects.mixed = false;
			theGlobalObjects.past = false;
			theGlobalObjects.future = false;
		} else if (tense == "Past Tense") {
			theGlobalObjects.past = true;
			theGlobalObjects.mixed = false;
			theGlobalObjects.present = false;
			theGlobalObjects.future = false;
		} else if (tense == "Future Tense") {
			theGlobalObjects.future = true;
			theGlobalObjects.mixed = false;
			theGlobalObjects.present = false;
			theGlobalObjects.past = false;
		}

		if (type == "Regular & Irregular") {
			theGlobalObjects.bothVerbTypes = true;
			theGlobalObjects.regular = false;
			theGlobalObjects.irregular = false;
		}
		else if (type == "Regular") {
			theGlobalObjects.regular = true;
			theGlobalObjects.bothVerbTypes = false;
			theGlobalObjects.irregular = false;
		}
		else if (type == "Irregular") {
			theGlobalObjects.irregular = true;
			theGlobalObjects.regular = false;
			theGlobalObjects.bothVerbTypes = false;
		}

		if (difficulty == "Beginner" && theGlobalObjects.translatorPressed == true) {
			theGlobalObjects.easy = true;
			SceneManager.LoadScene ("EndlessRunnerEasy");
		
		} else if (difficulty == "Intermediate" && theGlobalObjects.translatorPressed == true){
			theGlobalObjects.easy = false;
			SceneManager.LoadScene ("EndlessRunner");
		}
		else if(theGlobalObjects.conjugatorPressed == true){
			SceneManager.LoadScene ("BlockFall");
		}
	}

	public void BackButtonClick(){
		buttonSound.Play ();
		SceneManager.LoadScene ("DifficultyMenu");
	}


}
