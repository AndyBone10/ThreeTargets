using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class CloudSpawner : MonoBehaviour {

	public GameObject StartPoint;
	public GameObject EndPoint;
	public GameObject TheCloud;

	private Vector3 targetPosition;
	public float speed;
	// Use this for initialization
	void Start () {
		TheCloud.transform.position = StartPoint.transform.position;
		targetPosition = EndPoint.transform.position;

	}

	// Update is called once per frame
	void Update () {

		TheCloud.transform.position = Vector3.MoveTowards (TheCloud.transform.position, targetPosition, Time.deltaTime * speed);

		if (TheCloud.transform.position == targetPosition) {
			TheCloud.transform.position = StartPoint.transform.position;
		}

	}

}
