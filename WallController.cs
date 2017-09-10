using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour {

	private GameObject thePlayer;
	private PlayerController thePlayerController;

	void Start(){
		thePlayer = GameObject.Find ("Player");
		thePlayerController = thePlayer.GetComponent<PlayerController> ();
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			Instantiate (thePlayerController.deathExplosion, thePlayer.transform.position, thePlayer.transform.rotation);
			Destroy (other.gameObject);
			GameManager.instance.GameOver ();
		}
	}

}
