using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

	public GameObject StartPoint;
	public GameObject EndPoint;
	public GameObject TheCuboid;
	public GameObject theWallToDelete;
	public FlyGameManager theFlyGameManager;


	private Vector3 targetPosition;
	public float speed;
	// Use this for initialization
	void Start () {
		TheCuboid.transform.position = StartPoint.transform.position;
		targetPosition = EndPoint.transform.position;
		theWallToDelete.SetActive (true);
		GetWall ();
		theWallToDelete.SetActive (false);

	}
	
	// Update is called once per frame
	void LateUpdate () {
		
		TheCuboid.transform.position = Vector3.MoveTowards (TheCuboid.transform.position, targetPosition, Time.deltaTime * speed);

		if (TheCuboid.transform.position == targetPosition) {
			TheCuboid.transform.position = StartPoint.transform.position;
			theWallToDelete.SetActive (true);
			GetWall ();
			theWallToDelete.SetActive (false);
		}

	}
		
	public void GetWall(){

		if (theFlyGameManager.platformOneText.text == theFlyGameManager.rightAnswer) {
			theWallToDelete = GameObject.Find("TopCube");
		}
		else if (theFlyGameManager.platformTwoText.text == theFlyGameManager.rightAnswer) {
			theWallToDelete = GameObject.Find("MiddleCube");
		}
		else if (theFlyGameManager.platformThreeText.text == theFlyGameManager.rightAnswer) {
			theWallToDelete = GameObject.Find("BottomCube");
		}

	}

}
	