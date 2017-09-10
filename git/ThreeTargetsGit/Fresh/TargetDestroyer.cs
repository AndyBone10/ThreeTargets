﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDestroyer : MonoBehaviour {

	public GameObject platformDestructionPoint;


	// Use this for initialization
	void Start () {
		platformDestructionPoint = GameObject.Find ("TargetDestructionPoint");
	}

	// Update is called once per frame
	void Update () {
		if (transform.position.x < platformDestructionPoint.transform.transform.position.x) {
			Destroy (gameObject);
		}
	}
}
