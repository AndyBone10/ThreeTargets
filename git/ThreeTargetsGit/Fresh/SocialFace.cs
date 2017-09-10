using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialFace : MonoBehaviour {

	public AudioSource buttonPressedAudio;
	public Sprite freak,doubleChin, creep;

	public void ChangeButtonImage(){
		System.Random rand = new System.Random ();
		int rando = rand.Next (1, 4);
		if (rando == 1) {
			this.gameObject.GetComponent<UnityEngine.UI.Image>().overrideSprite = freak;
		} else if (rando == 2) {
			this.gameObject.GetComponent<UnityEngine.UI.Image>().overrideSprite = doubleChin;

		} else if (rando == 3) {
			this.gameObject.GetComponent<UnityEngine.UI.Image>().overrideSprite = creep;

		}
	}

	public void twitterPress(){
		buttonPressedAudio.Play ();
		//Application.OpenURL ("https://twitter.com/AndyBone10?lang=en");
		Application.ExternalEval("window.open(\"https://twitter.com/AndyBone10?lang=en\")");
	}

	public void tumblrPress(){
		buttonPressedAudio.Play ();
		//Application.OpenURL ("https://andybones.tumblr.com/");
		Application.ExternalEval("window.open(\"https://andybones.tumblr.com/\")");

	}
}
