using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

	private Rigidbody theRigidbody;

	void Start(){
		theRigidbody = GetComponent<Rigidbody> ();
	}

	void Update(){
		if (System.Math.Abs(theRigidbody.velocity.x) < 20.0f && System.Math.Abs(theRigidbody.velocity.y) < 20.0f) {
			Destroy (this.gameObject);
		}
	}
}



