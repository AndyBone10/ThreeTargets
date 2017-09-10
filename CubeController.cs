using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour {

	public GameObject player;
	public FlyGameManager theFlyGameManager;
	void Start(){
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player") {
			theFlyGameManager.SetFlyHighScore ();
			theFlyGameManager.GetFlyHighScore ();
			theFlyGameManager.deathScreenScoreText.text = theFlyGameManager.score.ToString();
			theFlyGameManager.theDeathScreen.gameObject.SetActive (true);
			player.SetActive(false);
		}
	}
}
