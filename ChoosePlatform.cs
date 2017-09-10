using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class ChoosePlatform : MonoBehaviour {

	public JetPackPlayer thePlayer;
	public GameObject pointToStand;
	private FlyGameManager theManager;

	string thePlatform;
	public float speed = 3.0f;

	// Use this for initialization
	void Start () {
		theManager = FindObjectOfType<FlyGameManager> ();
	}

	void OnMouseDown(){
		thePlayer.originalY = pointToStand.transform.position.y;
	
		theManager.platformClicked = gameObject.name;
	}
		
}
