using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public bool gameOver;
	private bool playInstructions;
	private float timeLeft;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {

		GameObject.Find ("Pointer").GetComponent<Animator> ().Play ("Instructions");

		timeLeft = 3.0f;

		if (!PlayerPrefs.HasKey ("FirstTimeEver")) {
			playInstructions = true;
			PlayerPrefs.SetInt ("FirstTimeEver", 1);
		} else {
			Destroy (GameObject.Find ("Instructions"));
		}
		gameOver = false;
	}

	void Update(){
		if (playInstructions == true) {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0.0f) {
				
				playInstructions = false;
				Destroy (GameObject.Find ("Instructions"));

			}
		}
	}

	public void GameOver(){
		UIManager.instance.GameOver ();
	}
}
