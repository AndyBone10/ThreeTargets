using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JetPackPlayer : MonoBehaviour {

	private GlobalObjects theGlobalObjects;

	public float originalY;
	public float originalX;

	public float floatStrengthY = 1.0f;
	public float floatStrengthX = 1.0f;

	private Vector3 startPosition = new Vector3 (-2.3f, -0.78f, 0.0f);
	private Animator playerAnimator;

	Rigidbody2D myRigidbody;
	
	void Start () {
		theGlobalObjects = FindObjectOfType<GlobalObjects> ();

		myRigidbody = GetComponent<Rigidbody2D> ();
		RuntimeAnimatorController theAnimController = (RuntimeAnimatorController)Resources.Load("Animations/Player6");
		if (theGlobalObjects.selectChar1) {
			theAnimController = (RuntimeAnimatorController)Resources.Load("Animations/Player6");
		}
		else if (theGlobalObjects.selectChar2) {
			theAnimController = (RuntimeAnimatorController)Resources.Load("Animations/Player3");
		}
		else if (theGlobalObjects.selectChar3) {
			theAnimController = (RuntimeAnimatorController)Resources.Load("Animations/Player4");
		}
		else if (theGlobalObjects.selectChar4) {
			theAnimController = (RuntimeAnimatorController)Resources.Load("Animations/Player5");
		}
		else if (theGlobalObjects.selectChar5) {
			theAnimController = (RuntimeAnimatorController)Resources.Load("Animations/Player2");
		}
		else if (theGlobalObjects.selectChar6) {
			theAnimController = (RuntimeAnimatorController)Resources.Load("Animations/Player1");
		}
		playerAnimator = GetComponent<Animator>();
		
		playerAnimator.runtimeAnimatorController = theAnimController;

		gameObject.transform.position = startPosition;

		Vector3 position = gameObject.transform.position;
		Quaternion rotation = gameObject.transform.rotation;
		Vector3 velocity = myRigidbody.velocity;
		playerAnimator.runtimeAnimatorController = theAnimController;
			gameObject.transform.position = position;
		myRigidbody.velocity = velocity;
		transform.rotation = rotation;

		this.originalY = this.transform.position.y;
		this.originalX = this.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
			transform.position = new Vector3 (originalX + ((float)Math.Sin (Time.time) * floatStrengthX), originalY + ((float)Math.Sin (Time.time) * floatStrengthY), transform.position.z);
	}
}
