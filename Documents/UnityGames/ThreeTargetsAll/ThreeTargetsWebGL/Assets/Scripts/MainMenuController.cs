using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public UnityEngine.UI.Text highScoreText;
	public AudioSource buttonPressedAudio;
	bool soundPressed;
	public Sprite soundEnabledSprite, soundDisabledSprite;
	public UnityEngine.UI.Button soundButton;

	void Start(){
		if (!PlayerPrefs.HasKey("Muted")) {
			PlayerPrefs.SetInt ("Muted", 1);
		}

		if (PlayerPrefs.GetInt ("Muted") == 1) {
			soundButton.image.sprite = soundEnabledSprite;

		}

		if (PlayerPrefs.GetInt ("Muted") == 0) {
			soundButton.image.sprite = soundDisabledSprite;

		}

		highScoreText.text = PlayerPrefs.GetInt ("HighScore").ToString();
		soundPressed = false;
	}

	void Update(){

		if (PlayerPrefs.GetInt ("Muted") == 1) {
				soundButton.image.sprite = soundEnabledSprite;
		}

		if (PlayerPrefs.GetInt ("Muted") == 0) {
			soundButton.image.sprite = soundDisabledSprite;
		}

	}

	public void twitterPress(){
		buttonPressedAudio.Play ();
		Application.OpenURL ("https://twitter.com/AndyBone10?lang=en");
	}

	public void tumblrPress(){
		buttonPressedAudio.Play ();
		Application.OpenURL ("https://andybones.tumblr.com/");
	}

	public void playGame(){
		buttonPressedAudio.Play ();
		SceneManager.LoadScene ("Scene1");
	}

	public void soundPress(){
		buttonPressedAudio.Play ();

		if (soundPressed == true) {
			soundPressed = false;
			PlayerPrefs.SetInt ("Muted", 0);
			AudioListener.volume = 0;
		} else if (soundPressed == false) {
			soundPressed = true;

			PlayerPrefs.SetInt ("Muted", 1);

			AudioListener.volume = 100;

		}

	}
}
