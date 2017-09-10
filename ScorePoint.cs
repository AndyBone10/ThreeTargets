using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePoint : MonoBehaviour {

	private GameManager theGameManager;
	private GlobalObjects theGlobalObjects;

	public int scoreValue;

	// Use this for initialization
	void Start () {
		theGameManager = FindObjectOfType<GameManager> ();
		theGlobalObjects = FindObjectOfType<GlobalObjects> ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && theGlobalObjects.easy == true) {
			theGameManager.pointOfChangeReached = true;
			theGameManager.theMixedPanel.gameObject.SetActive (true);
			theGameManager.theIrishPanel.gameObject.SetActive (false);
			theGameManager.countdownObject.gameObject.SetActive (true);
			theGameManager.secondsLeft = 8.2f;
			theGameManager.AddScore (scoreValue);
			Destroy (gameObject);
		} 
		else if (other.tag == "Player" && theGlobalObjects.easy == false) 
		{
			theGameManager.pointOfChangeReached = true;
			theGameManager.AddScore (scoreValue);
			Destroy (gameObject);
		}
	}

}
