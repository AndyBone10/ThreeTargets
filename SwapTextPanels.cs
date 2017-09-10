using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapTextPanels : MonoBehaviour {

	private GameManager theGameManager;


	// Use this for initialization
	void Start () {
		theGameManager = FindObjectOfType<GameManager> ();
	
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			theGameManager.RandomizeArray (theGameManager.threeEnglish);


			theGameManager.theMixedPanel.gameObject.SetActive (false);
			theGameManager.theIrishPanel.gameObject.SetActive (true);
			theGameManager.option1.gameObject.SetActive (true);
			theGameManager.option2.gameObject.SetActive (true);
			theGameManager.option3.gameObject.SetActive (true);
			theGameManager.Button1.gameObject.SetActive (true);
			theGameManager.Button2.gameObject.SetActive (true);
			theGameManager.Button3.gameObject.SetActive (true);
			
		}
	}

}
