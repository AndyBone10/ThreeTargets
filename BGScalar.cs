using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScalar : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		var height = Camera.main.orthographicSize * 2f;
		var width = height * Screen.width / Screen.height;

		if (gameObject.name == "Background") {
			transform.localScale = new Vector3 (width + 1.0f, height, 0);
		}
	
	}
}
