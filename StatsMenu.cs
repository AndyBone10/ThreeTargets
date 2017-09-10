using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsMenu : MonoBehaviour {

	public AudioSource buttonSound;
	public string gameMenu;
	public UnityEngine.UI.Button char1;
	public UnityEngine.UI.Button char2;
	public UnityEngine.UI.Button char3;
	public UnityEngine.UI.Button char4;
	public UnityEngine.UI.Button char5;
	public UnityEngine.UI.Button char6;

	private GameObject characterImage;
	private GameObject lockScreen;

	private GameObject selected1, selected2, selected3, selected4, selected5, selected6;

	private GlobalObjects theGlobalObjects;
	// Use this for initialization
	void Start () {
		theGlobalObjects = FindObjectOfType<GlobalObjects> ();
		selected1 = GameObject.Find ("ChoiceButton/Selected");
		selected2 = GameObject.Find ("ChoiceButton2/Selected");
		selected3 = GameObject.Find ("ChoiceButton3/Selected");
		selected4 = GameObject.Find ("ChoiceButton4/Selected");
		selected5 = GameObject.Find ("ChoiceButton5/Selected");
		selected6 = GameObject.Find ("ChoiceButton6/Selected");

		selected1.SetActive (false);
		selected2.SetActive (false);
		selected3.SetActive (false);
		selected4.SetActive (false);
		selected5.SetActive (false);
		selected6.SetActive (false);

		if (theGlobalObjects.selectChar1 == true) {
			selected1.SetActive(true);
		}
		else if(theGlobalObjects.selectChar2 == true){
			selected2.SetActive(true);
		}
		else if(theGlobalObjects.selectChar3 == true){
			selected3.SetActive(true);
		}
		else if(theGlobalObjects.selectChar4 == true){
			selected4.SetActive(true);
		}
		else if(theGlobalObjects.selectChar5 == true){
			selected5.SetActive(true);
		}
		else if(theGlobalObjects.selectChar6 == true){
			selected6.SetActive(true);
		}
	
	}

	public void Char1UnlockClick(){
		buttonSound.Play();
		theGlobalObjects.char1 = true;
		characterImage = GameObject.Find ("ChoiceButton/Character");
		lockScreen = GameObject.Find ("ChoiceButton/Lock");
		if (lockScreen == null) {
			theGlobalObjects.selectChar1 = true;
			theGlobalObjects.selectChar2 = false;
			theGlobalObjects.selectChar3 = false;
			theGlobalObjects.selectChar4 = false;
			theGlobalObjects.selectChar5 = false;
			theGlobalObjects.selectChar6 = false;

			selected1.SetActive (true);
			selected2.SetActive (false);
			selected3.SetActive (false);
			selected4.SetActive (false);
			selected5.SetActive (false);
			selected6.SetActive (false);
		}
		else if (lockScreen.activeSelf == true) {
			lockScreen.SetActive (false);
		} 

	}

	public void Char2UnlockClick(){
		buttonSound.Play();
		theGlobalObjects.char2 = true;
		characterImage = GameObject.Find ("ChoiceButton2/Character");
		lockScreen = GameObject.Find ("ChoiceButton2/Lock");
		if (lockScreen == null) {
			theGlobalObjects.selectChar2 = true;
			theGlobalObjects.selectChar1 = false;
			theGlobalObjects.selectChar3 = false;
			theGlobalObjects.selectChar4 = false;
			theGlobalObjects.selectChar5 = false;
			theGlobalObjects.selectChar6 = false;

			selected1.SetActive (false);
			selected2.SetActive (true);
			selected3.SetActive (false);
			selected4.SetActive (false);
			selected5.SetActive (false);
			selected6.SetActive (false);
		}
		else if (lockScreen.activeSelf == true) {
			lockScreen.SetActive (false);
		} 

	}

	public void Char3UnlockClick(){
		buttonSound.Play();
		theGlobalObjects.char3 = true;
		characterImage = GameObject.Find ("ChoiceButton3/Character");
		lockScreen = GameObject.Find ("ChoiceButton3/Lock");
		if (lockScreen == null) {
			theGlobalObjects.selectChar3 = true;
			theGlobalObjects.selectChar1 = false;
			theGlobalObjects.selectChar2 = false;
			theGlobalObjects.selectChar4 = false;
			theGlobalObjects.selectChar5 = false;
			theGlobalObjects.selectChar6 = false;

			selected1.SetActive (false);
			selected2.SetActive (false);
			selected3.SetActive (true);
			selected4.SetActive (false);
			selected5.SetActive (false);
			selected6.SetActive (false);
		}
		else if (lockScreen.activeSelf == true) {
			lockScreen.SetActive (false);
		} 

	}

	public void Char4UnlockClick(){
		buttonSound.Play();
		theGlobalObjects.char4 = true;
		characterImage = GameObject.Find ("ChoiceButton4/Character");
		lockScreen = GameObject.Find ("ChoiceButton4/Lock");
		if (lockScreen == null) {
			theGlobalObjects.selectChar4 = true;
			theGlobalObjects.selectChar1 = false;
			theGlobalObjects.selectChar2 = false;
			theGlobalObjects.selectChar3 = false;
			theGlobalObjects.selectChar5 = false;
			theGlobalObjects.selectChar6 = false;

			selected1.SetActive (false);
			selected2.SetActive (false);
			selected3.SetActive (false);
			selected4.SetActive (true);
			selected5.SetActive (false);
			selected6.SetActive (false);
		}
		else if (lockScreen.activeSelf == true) {
			lockScreen.SetActive (false);
		} 

	}

	public void Char5UnlockClick(){
		buttonSound.Play();
		theGlobalObjects.char5 = true;
		characterImage = GameObject.Find ("ChoiceButton5/Character");
		lockScreen = GameObject.Find ("ChoiceButton5/Lock");
		if (lockScreen == null) {
			theGlobalObjects.selectChar5 = true;
			theGlobalObjects.selectChar2 = false;
			theGlobalObjects.selectChar3 = false;
			theGlobalObjects.selectChar4 = false;
			theGlobalObjects.selectChar1 = false;
			theGlobalObjects.selectChar6 = false;

			selected1.SetActive (false);
			selected2.SetActive (false);
			selected3.SetActive (false);
			selected4.SetActive (false);
			selected5.SetActive (true);
			selected6.SetActive (false);
		}
		else if (lockScreen.activeSelf == true) {
			lockScreen.SetActive (false);
		} 

	}

	public void Char6UnlockClick(){
		buttonSound.Play();
		theGlobalObjects.char6 = true;
		characterImage = GameObject.Find ("ChoiceButton6/Character");
		lockScreen = GameObject.Find ("ChoiceButton6/Lock");
		if (lockScreen == null) {
			theGlobalObjects.selectChar6 = true;
			theGlobalObjects.selectChar2 = false;
			theGlobalObjects.selectChar3 = false;
			theGlobalObjects.selectChar4 = false;
			theGlobalObjects.selectChar5 = false;
			theGlobalObjects.selectChar1 = false;

			selected1.SetActive (false);
			selected2.SetActive (false);
			selected3.SetActive (false);
			selected4.SetActive (false);
			selected5.SetActive (false);
			selected6.SetActive (true);
		}
		else if (lockScreen.activeSelf == true) {
			lockScreen.SetActive (false);
		} 

	}

	public void BackButtonClick(){
		buttonSound.Play ();
		SceneManager.LoadScene ("MainMenu");
	}
		
}

