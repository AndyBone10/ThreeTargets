using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextQuestion : MonoBehaviour {

	private FlyGameManager theFlyGameManager;
	private ObstacleSpawner theObsSpawner;

	// Use this for initialization
	void Start () {
		theFlyGameManager = FindObjectOfType<FlyGameManager> ();
		theObsSpawner = FindObjectOfType<ObstacleSpawner> ();
	}

	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {

			theFlyGameManager.AddScore(1);
			theFlyGameManager.PlatformOne.gameObject.SetActive (true);
			theFlyGameManager.PlatformTwo.gameObject.SetActive (true);
			theFlyGameManager.PlatformThree.gameObject.SetActive (true);

			theFlyGameManager.GetRightAnswerFromDatabase ();
			theFlyGameManager.GetPlatformStrings ();
			theFlyGameManager.AssignPlatformStrings ();
			theFlyGameManager.ChangeSentenceText ();
			theFlyGameManager.countdownObject.gameObject.SetActive (true);
			theFlyGameManager.secondsLeft = 8.0f;

			theObsSpawner.theWallToDelete.SetActive (true);
			theObsSpawner.GetWall ();
			theObsSpawner.theWallToDelete.SetActive (false);
		
		} 
	
	}
}
