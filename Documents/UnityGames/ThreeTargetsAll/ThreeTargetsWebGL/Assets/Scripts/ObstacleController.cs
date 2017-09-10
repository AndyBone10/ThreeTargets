using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

	public GameObject target1;
	public GameObject target2;
	public GameObject target3;
	private GameObject theProjectile;

	private GameObject thePlayer;

	// Use this for initialization
	void Start () {
		thePlayer = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		theProjectile = GameObject.Find ("Projectile(Clone)");

		if (target1.gameObject == null && target2.gameObject == null && target3.gameObject == null) {
			this.gameObject.SetActive (false);

			if (thePlayer.gameObject != null) {
				ScoreManager.instance.incrementScore ();
				ScoreManager.instance.setScore ();
			}
		}

		if (this.gameObject.activeSelf == false) {
			Destroy (theProjectile);
		}
	}
}
