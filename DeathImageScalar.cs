using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathImageScalar : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var height = Camera.main.orthographicSize * 2f;
		var width = height * Screen.width / Screen.height;

		if (gameObject.name == "Background") 
		{
			transform.localScale = new Vector3 (width + 0.3f, height - 0.6f, 0);
		}
		else 
		{
			transform.localScale = new Vector3 (width + 3f, 5, 0);
		}
	}
}
