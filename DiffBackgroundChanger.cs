using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffBackgroundChanger : MonoBehaviour {

	private float secondsLeft = 5.0f;

	public UnityEngine.UI.Button button1;
	public UnityEngine.UI.Button button2;
	public UnityEngine.UI.Button button3;
	public UnityEngine.UI.Button button4;

	//purple
	static Color purp = new Color(0.8f,0.46f,0.96f,1.0f);

	//red
	static Color lightRed = new Color(0.96f,0.46f, 0.46f, 1.0f);

	//orange
	static Color lightOrange = new Color(0.96f, 0.73f, 0.46f, 1.0f);

	//yellow
	static Color blue = new Color(0.35f, 0.68f, 0.95f, 1.0f);

	//green
	static Color lightGreen = new Color(0.45f, 0.95f, 0.35f, 1.0f);

	//pink
	static Color lightPink = new Color(0.97f, 0.61f, 0.80f, 1.0f);

	Color [] colorArray = { purp, lightRed, lightOrange, blue, lightGreen, lightPink };
	Color currentColor;
	Color targetColor;
	// Use this for initialization
	void Start () {
		currentColor = ChooseRandomColour(colorArray);
		targetColor = ChooseRandomColour (colorArray);
		secondsLeft = 8.0f;
	}

	// Update is called once per frame
	void Update () {
		if (secondsLeft < 0f) 
		{	
			Color temp = ChooseRandomColour (colorArray);
			if (temp != Camera.main.backgroundColor) {
				targetColor = temp;
			}
			secondsLeft = 5.0f;
		}
		currentColor = Color.Lerp (currentColor, targetColor, 0.012f);
		Camera.main.backgroundColor = currentColor;
		button1.image.color = currentColor;
		button2.image.color = currentColor;
		button3.image.color = currentColor;
		button4.image.color = currentColor;
		secondsLeft -= Time.deltaTime;
	}

	private Color ChooseRandomColour(Color [] colorArray){
		System.Random r = new System.Random();
		int random = r.Next(0, 5);
		return colorArray [random];
	}
}
