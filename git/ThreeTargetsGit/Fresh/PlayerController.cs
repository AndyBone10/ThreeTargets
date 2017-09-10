using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	private Rigidbody myRigidbody;

	//handling firing

	public Transform firePoint;
	public GameObject projectile;
	private GameObject activeProjectile;
	private int projectileCount;

	public float fireSpeed;
	public Vector3 firstPos;
	public Vector3 lastPos;

	LineRenderer line;
	private Vector3 lineStartPos;
	private Vector3 lineEndPos;
	Vector3 lineVel;

	public GameObject deathExplosion;


	public bool started;


	// Use this for initialization
	void Start () {

		firePoint.gameObject.SetActive (false);
		started = false;
	
		myRigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (started == false ) {
			if (Input.GetMouseButtonDown (0) || PlayerPrefs.GetInt("RestartCheck") == 1) {
				firePoint.gameObject.SetActive (true);
				started = true;
				UIManager.instance.GameStart();
				PlayerPrefs.SetInt ("RestartCheck", 0);
			}
		}

		//Moves Player
		if (started) {
			myRigidbody.velocity = new Vector3 (moveSpeed, myRigidbody.velocity.y, 0f);
		}

	}

}
	
