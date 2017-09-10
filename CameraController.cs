using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour {

	public GameObject target; 

	private Vector3 targetPosition;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (target.gameObject != null) {
			targetPosition = new Vector3 (target.transform.position.x + 8.0f, transform.position.y, transform.position.z);

			transform.position = new Vector3(targetPosition.x + 2.5f, targetPosition.y, targetPosition.z);
		}
			
	}
}
