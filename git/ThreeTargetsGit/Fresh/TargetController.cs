using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.tag == "Projectile") {
			Destroy (gameObject);
			GameObject.Find ("Badoop").GetComponent<AudioSource> ().Play ();
		}
	}
}
