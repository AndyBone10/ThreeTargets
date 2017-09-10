using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorAndRoofGenerator : MonoBehaviour {

	public GameObject theFloorAndRoof;
	public Transform generationPoint;
	public float distanceBetween;

	private float theFloorAndRoofWidth;

	// Use this for initialization
	void Start () {
		theFloorAndRoofWidth = theFloorAndRoof.gameObject.transform.GetChild (0).transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.position.x < generationPoint.position.x) {
			transform.position = new Vector3 (transform.position.x + theFloorAndRoofWidth + distanceBetween, transform.position.z);
			Instantiate (theFloorAndRoof, transform.position, transform.rotation);
		}

	}
}
