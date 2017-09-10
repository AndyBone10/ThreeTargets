using UnityEngine;
using System.Collections;

public class PlatformSpawner : MonoBehaviour {

	public GameObject parentPlatform;
	public GameObject platform;
	public Transform generationPoint;
	public float distanceBetween;
	private float platformWidth;

	public void Start ()
	{
		platformWidth = platform.GetComponent<BoxCollider2D>().size.x;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transform.position.x < generationPoint.position.x) 
		{
			transform.position = new Vector3 (transform.position.x + platformWidth + distanceBetween, transform.position.y, transform.position.z);
			var plat = Instantiate (platform, transform.position, transform.rotation);
			plat.transform.parent = parentPlatform.transform;
		}
	}

	public void DestroyAllPlatforms()
	{
		int childs = parentPlatform.transform.childCount;
		for (int i = childs - 1; i >= 0; i--) 
		{
			GameObject.Destroy (parentPlatform.transform.GetChild (i).gameObject);
		}
	}
}
