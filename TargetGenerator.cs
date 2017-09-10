using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerator : MonoBehaviour {

	public GameObject theTargets;
	public Transform targetGenerationPoint;
	public float distanceBetween;

	private float targetWidth;


	// Use this for initialization
	void Start () {
		targetWidth = 28.0f;
	}

	// Update is called once per frame
	void Update () {

		if (transform.position.x < targetGenerationPoint.position.x) {
			System.Random rand = new System.Random ();
			int random = rand.Next (1, 14);
			string targetToLoad = "Prefabs/ThreeTargets" + random;
			theTargets = Resources.Load(targetToLoad) as GameObject;
			transform.position = new Vector3 (transform.position.x + targetWidth + distanceBetween,transform.position.y, transform.position.z);
			Instantiate (theTargets, transform.position, transform.rotation);
		}

	}
}
