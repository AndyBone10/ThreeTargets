using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Data; 
using System.Reflection;
using System.IO;
using Mono.Data.SqliteClient;

public class GameManager : MonoBehaviour {

	private string connection;
	private IDbConnection dbcon;
	private IDbCommand dbcmd;
	private IDataReader reader;
	private string p = "LanguageData.db";

	public Text EnglishCorrectAnswer;
	public Text IrishCorrectAnswer;

	public GameObject player;
	public Rigidbody2D myRigidbody;
	public DeathMenu theDeathScreen;

	public AudioSource rightSound;

	private int count;

	public int scoreCount;
	public Text scoreText;
	public Text deathScreenScoreText;
	public Text currentHighScoreText;

	public Text countdownText;
	public float secondsLeft; 
	public GameObject countdownObject;

	public GameObject theMixedPanel;
	public GameObject theIrishPanel;

	public Text engText;
	public Text irishText;
	public Text irishText1;
	public Text irishText2;
	public Text irishText3;
	public Text engText1;
	public Text engText2;
	public Text engText3;
	
	public bool isGrounded;
	private bool swappedOver;

	public Text option1;
	public Text option2;
	public Text option3;

	public Button Button1;
	public Button Button2;
	public Button Button3;
	private Button btn1;
	private Button btn2;
	private Button btn3;

	private string rightAnswer;
	private bool correct;
	private bool changeQuestion;
	public bool pointOfChangeReached;

	public bool presentTense;
	public bool pastTense;
	public bool futureTense;
	public bool mixedTense;
	public bool regular;
	public bool irregular;
	public bool easy;

	//database management
	private int databaseIndex;
	private int numberOfRows;
	private int[] arrayOfIndexes;
	private int databaseSize = 520;
	private ArrayList irishFromDatabase;
	private ArrayList englishFromDatabase;
	string sqlQuery = "";
	private int databaseCount = 0;

	public Vector3 platformSpawnerResetPos;
	public PlatformSpawner platformSpawner;

	public string[] threeEnglish;
	public string[] threeIrish;

	private GlobalObjects theGlobalObjects;

	// Use this for initialization
	void Start () {


		irishFromDatabase = new ArrayList();
		englishFromDatabase = new ArrayList();
		threeEnglish = new string[3];
		threeIrish = new string[3];
		theMixedPanel = GameObject.Find ("MixedTextPanel");
		theIrishPanel = GameObject.Find ("IrishTextPanel");


		theGlobalObjects = FindObjectOfType<GlobalObjects> ();
		easy = theGlobalObjects.easy;

		if (easy) {
			platformSpawnerResetPos = new Vector3(4.2f, -1.15f, 0f);
		} else {
			platformSpawnerResetPos = new Vector3(0.074f, -1.15f, 0f);
		}

		presentTense = theGlobalObjects.present;
		pastTense = theGlobalObjects.past;
		futureTense = theGlobalObjects.future;
		mixedTense = theGlobalObjects.mixed;
		regular = theGlobalObjects.regular;
		irregular = theGlobalObjects.irregular;

		if (easy) {
			secondsLeft = 8.0f;
			theIrishPanel.gameObject.SetActive (false);
		} else {
			countdownObject.gameObject.SetActive (false);
		}

		scoreText.text = scoreCount.ToString();

		myRigidbody = GetComponent<Rigidbody2D> ();
		btn1 = Button1.GetComponent<UnityEngine.UI.Button>();
		btn2 = Button2.GetComponent<UnityEngine.UI.Button>();
		btn3 = Button3.GetComponent<UnityEngine.UI.Button>();
		btn1.onClick.AddListener(CheckButton1);
		btn2.onClick.AddListener(CheckButton2);
		btn3.onClick.AddListener(CheckButton3);

		pointOfChangeReached = true;
		changeQuestion = true;



		if (mixedTense && theGlobalObjects.bothVerbTypes == true) {
			sqlQuery = "SELECT Irish,English FROM Verbs";
			Debug.Log ("1");
		}
		else if (presentTense && theGlobalObjects.bothVerbTypes == true) 
		{
			Debug.Log ("2");
			sqlQuery = "SELECT Irish,English FROM Verbs WHERE Verbs.tense like '%present%'";
		} 
		else if (futureTense && theGlobalObjects.bothVerbTypes == true) 
		{
			Debug.Log ("3");
			sqlQuery = "SELECT Irish,English FROM Verbs WHERE Verbs.tense like '%future%'";
		} 
		else if(pastTense && theGlobalObjects.bothVerbTypes == true) //
		{
			Debug.Log ("4");
			sqlQuery = "SELECT Irish,English FROM Verbs WHERE Verbs.tense like '%past%'";
		}
		else if (mixedTense && theGlobalObjects.regular == true) {
			Debug.Log ("5");
			sqlQuery = "SELECT Irish,English FROM Verbs WHERE Verbs.type like 'regular'";
		}
		else if (mixedTense && theGlobalObjects.irregular == true) {
			Debug.Log ("6");
			sqlQuery = "SELECT Irish,English FROM Verbs WHERE Verbs.type like '%irregular%'";
		}
		else if (presentTense && theGlobalObjects.regular == true) 
		{
			Debug.Log ("7");
			sqlQuery = "SELECT Irish,English FROM Verbs WHERE Verbs.tense like '%present%' AND Verbs.type like '%regular%'";
		} 
		else if (futureTense && theGlobalObjects.regular == true) 
		{
			Debug.Log ("8");
			sqlQuery = "SELECT Irish,English FROM Verbs WHERE Verbs.tense like '%future%' AND Verbs.type like '%regular%'";
		} 
		else if(pastTense && theGlobalObjects.regular == true) //pastTense
		{
			Debug.Log ("9");
			sqlQuery = "SELECT Irish,English FROM Verbs WHERE Verbs.tense like '%past%' AND Verbs.type like '%regular%'";
		}
		else if (mixedTense && theGlobalObjects.irregular == true) {
			Debug.Log ("10");
			sqlQuery = "SELECT Irish,English FROM Verbs WHERE Verbs.type like '%irregular%'";
		}
		else if (presentTense && theGlobalObjects.irregular == true) 
		{
			Debug.Log ("11");
			sqlQuery = "SELECT Irish,English FROM Verbs WHERE Verbs.tense like '%present%' AND Verbs.type like '%irregular%'";
		} 
		else if (futureTense && theGlobalObjects.irregular == true) 
		{
			Debug.Log ("12");
			sqlQuery = "SELECT Irish,English FROM Verbs WHERE Verbs.tense like '%future%' AND Verbs.type like '%irregular%'";
		} 
		else if(pastTense && theGlobalObjects.irregular == true) //pastTense
		{
			Debug.Log ("13");
			sqlQuery = "SELECT Irish,English FROM Verbs WHERE Verbs.tense like '%past%' AND Verbs.type like '%irregular%'";
		}

		OpenDB (p);
			using (IDbCommand dbcmd = dbcon.CreateCommand ()) {

			dbcmd.CommandText = sqlQuery;
			using (IDataReader reader = dbcmd.ExecuteReader ()) {
				while (reader.Read ()) {

					irishFromDatabase.Add (reader.GetString (0));
					englishFromDatabase.Add (reader.GetString (1));

				}

			}
		}
		
		if (easy) {
			option1.gameObject.SetActive (false);
			option2.gameObject.SetActive (false);
			option3.gameObject.SetActive (false);
			Button1.gameObject.SetActive (false);
			Button2.gameObject.SetActive (false);
			Button3.gameObject.SetActive (false);
		}

	}
	
	// Update is called once per frame
	void Update () 
	{

		if (secondsLeft < 0f) 
		{
			countdownObject.gameObject.SetActive (false);
		}
		

		if (changeQuestion == true && pointOfChangeReached == true) 
		{
			swappedOver = false;
			
			if (easy) {
				option1.gameObject.SetActive (false);
				option2.gameObject.SetActive (false);
				option3.gameObject.SetActive (false);
				Button1.gameObject.SetActive (false);
				Button2.gameObject.SetActive (false);
				Button3.gameObject.SetActive (false);
			}
			if (presentTense == true && easy == false) {
				GetPresentTense ();
			} else if (presentTense == true && easy == true) {
				EasyGetPresentTense ();
			}
			else if (futureTense && easy == false) {
				GetFutureTense ();
			}
			else if (futureTense && easy == true) {
				EasyGetFutureTense ();
			}else if (pastTense && easy == false) {
				GetPastTense ();
			}
			else if (pastTense && easy == true) {
				EasyGetPastTense ();
			} else if (mixedTense && easy == false) {
				GetMixedTense ();
			} else if (mixedTense && easy == true){
				EasyGetMixedTense ();
			}
			changeQuestion = false;
			pointOfChangeReached = false;
		}

		secondsLeft -= Time.deltaTime;
		countdownText.text = secondsLeft.ToString ();
	}
		
	//shuffle array
	public void RandomizeArray(string [] arr)
	{
		for (var i = arr.Length - 1; i > 0; i--) 
		{
			var r = UnityEngine.Random.Range(0,i);
			var tmp = arr[i];
			arr[i] = arr[r];
			arr[r] = tmp;
		}
	}

	void CheckButton1(){
		string input = option1.text;

		if(input == rightAnswer)
		{
			correct = true;
			engText.text = "Correct!";
			changeQuestion = true;
			player.GetComponent<PlayerController> ().MakePlayerJump ();
		}
		else 
		{
			correct = false;
			player.SetActive (false);
		}	

		if (!correct) 
		{	
			
			player.gameObject.SetActive (false);
			theDeathScreen.gameObject.SetActive (true);
			countdownObject.SetActive (false);
			deathScreenScoreText.text = scoreCount.ToString();
			engText.gameObject.SetActive (false);
			theIrishPanel.gameObject.SetActive (false);
			SetHighScore ();
			GetHighScore ();

		}
	}

	void CheckButton2(){
		string input = option2.text;

		if(input == rightAnswer)
		{
			player.GetComponent<PlayerController> ().MakePlayerJump ();
			correct = true;
			engText.text = "Correct!";
			changeQuestion = true;
		}
		else 
		{
			correct = false;
			player.SetActive (false);
		}

		if (!correct) 
		{
			player.gameObject.SetActive (false);
			theDeathScreen.gameObject.SetActive (true);
			countdownObject.SetActive (false);
			deathScreenScoreText.text = scoreCount.ToString();
			engText.gameObject.SetActive (false);
			theIrishPanel.gameObject.SetActive (false);
			SetHighScore ();
			GetHighScore ();

		}
	}

	void CheckButton3(){
		string input = option3.text;

		if(input == rightAnswer)
		{
			player.GetComponent<PlayerController> ().MakePlayerJump ();
			correct = true;
			engText.text = "Correct!";
			changeQuestion = true;
		}
		else 
		{
			correct = false;
			player.SetActive (false);

		}

		if (!correct) 
		{
			player.gameObject.SetActive (false);
			theDeathScreen.gameObject.SetActive (true);
			countdownObject.SetActive (false);
			deathScreenScoreText.text = scoreCount.ToString();
			engText.gameObject.SetActive (false);
			theIrishPanel.gameObject.SetActive (false);
			SetHighScore ();
			GetHighScore ();
			
		}
	}


	void GetAllVerbsFromDatabase()
	{
		string filepath = Application.persistentDataPath + "/" + p;

		if(!File.Exists(filepath))
		{
			WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/Assets/" + p);  // this is the path to your StreamingAssets in android

			while(!loadDB.isDone) {} 

			File.WriteAllBytes(filepath, loadDB.bytes);

		}


		connection = "URI=file:" + filepath;

		dbcon = new SqliteConnection(connection);

		dbcon.Open();
		IDbCommand dbcmd = dbcon.CreateCommand();
		string sqlQuery = "SELECT Irish,English FROM Verbs";
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		int indexForBoth = 0;
		while (reader.Read())
		{
			irishFromDatabase[indexForBoth] = reader.GetString(0);
			englishFromDatabase [indexForBoth] = reader.GetString (1);
			
			indexForBoth++;


		}
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbcon.Close();
		dbcon = null;
	}

	void GetMixedTense()
	{

		databaseCount = irishFromDatabase.Count;
		numberOfRows = 13;

		System.Random rando = new System.Random ();
		int randomNum = rando.Next (0, databaseCount - numberOfRows);
		databaseIndex = randomNum;
		string[] allIrishWords = new string[13];
		string[] allEnglishWords = new string[13];


		int indexForBoth = 0;

		for (int i = databaseIndex; i < databaseIndex + numberOfRows; i++) 
		{
			allIrishWords [indexForBoth] = irishFromDatabase [i].ToString();
			allEnglishWords [indexForBoth] = englishFromDatabase [i].ToString();
			indexForBoth++;
		}

		System.Random r = new System.Random();
		int random = r.Next(0, 12);
		int temp = random;

		if (random % 2 == 0) {
			irishText.text = allIrishWords [random];
			IrishCorrectAnswer.text = irishText.text;
			rightAnswer = allEnglishWords [temp];
			threeEnglish [0] = rightAnswer;
		} else {
			irishText.text = allEnglishWords [random];
			IrishCorrectAnswer.text = irishText.text;
			rightAnswer = allIrishWords [temp];
			swappedOver = true;
			threeIrish [0] = rightAnswer;
		}
		EnglishCorrectAnswer.text = rightAnswer;
		if (!swappedOver) {
			for (int i = 0; i < 2; i++) {
				System.Random rand = new System.Random ();
				int randomEnglish = rand.Next (0, 12);
				threeEnglish [1] = allEnglishWords [randomEnglish];
				if (threeEnglish [1] == threeEnglish [0]) {
					i = 0;
				}
			}
			for (int i = 0; i < 2; i++) {
				System.Random rand = new System.Random ();
				int randomEnglish = rand.Next (0, 12);
				threeEnglish [2] = allEnglishWords [randomEnglish];
				if (threeEnglish [2] == threeEnglish [0] || threeEnglish [2] == threeEnglish [1]) {
					i = 0;
				}
			}
		} else {
			for (int i = 0; i < 2; i++) {
				System.Random rand = new System.Random ();
				int randomIrish = rand.Next (0, 12);
				threeIrish [1] = allIrishWords [randomIrish];
				if (threeIrish [1] == threeIrish [0]) {
					i = 0;
				}
			}
			for (int i = 0; i < 2; i++) {
				System.Random rand = new System.Random ();
				int randomIrish = rand.Next (0, 12);
				threeIrish [2] = allIrishWords [randomIrish];
				if (threeIrish [2] == threeIrish [0] || threeIrish [2] == threeIrish [1]) {
					i = 0;
				}
			}
		}
		if (!swappedOver) {
			RandomizeArray (threeEnglish);

			option1.text = threeEnglish [0];
			option2.text = threeEnglish [1];
			option3.text = threeEnglish [2];
		} else {
			RandomizeArray (threeIrish);

			option1.text = threeIrish [0];
			option2.text = threeIrish [1];
			option3.text = threeIrish [2];
		}
	}

	void EasyGetMixedTense()
	{
		theIrishPanel.gameObject.SetActive (false);
		databaseCount = irishFromDatabase.Count;
		numberOfRows = 13;

		System.Random rando = new System.Random ();
		int randomNum = rando.Next (0, databaseCount - numberOfRows);
		databaseIndex = randomNum;
		string[] allIrishWords = new string[13];
		string[] allEnglishWords = new string[13];
		string[] threeIrish = new string[3];
	
		int indexForBoth = 0;

		for (int i = databaseIndex; i < databaseIndex + numberOfRows; i++) 
		{
			allIrishWords [indexForBoth] = irishFromDatabase [i].ToString();
			allEnglishWords [indexForBoth] = englishFromDatabase [i].ToString();
			indexForBoth++;
		}

		System.Random r = new System.Random();
		int random = r.Next(0, 12);
		int temp = random;

		if (random % 2 == 0) {
			irishText.text = allIrishWords [random];
			IrishCorrectAnswer.text = irishText.text;
			threeIrish [0] = allIrishWords [random];
			rightAnswer = allEnglishWords [temp];
			threeEnglish [0] = rightAnswer;
		} else {
			irishText.text = allEnglishWords [random];
			IrishCorrectAnswer.text = irishText.text;
			threeEnglish [0] = allEnglishWords [random];
			rightAnswer = allIrishWords [temp];
			threeIrish [0] = rightAnswer;
			swappedOver = true;
		}
		EnglishCorrectAnswer.text = rightAnswer;

			for (int i = 0; i < 2; i++) {
				System.Random rand = new System.Random ();
				int randomWord = rand.Next (0, 12);
				threeIrish [1] = allIrishWords [randomWord];
				threeEnglish [1] = allEnglishWords [randomWord];
				if (threeEnglish [1] == threeEnglish [0] || threeIrish [1] == threeIrish [0]) {
					i = 0;
				}
			}
			for (int i = 0; i < 2; i++) {
				System.Random rand = new System.Random ();
				int randomWord = rand.Next (0, 12);
				threeIrish [2] = allIrishWords [randomWord];
				threeEnglish [2] = allEnglishWords [randomWord];
				if (threeEnglish [2] == threeEnglish [0] || threeEnglish [2] == threeEnglish [1] ||
				   threeIrish [2] == threeIrish [0] || threeIrish [2] == threeIrish [1]) {
					i = 0;
				}
			}


		if (!swappedOver) {

			engText1.text = threeEnglish [0];
			engText2.text = threeEnglish [1];
			engText3.text = threeEnglish [2];

			irishText1.text = threeIrish [0];
			irishText2.text = threeIrish [1];
			irishText3.text = threeIrish [2];

			option1.text = threeEnglish [0];
			option2.text = threeEnglish [1];
			option3.text = threeEnglish [2];
		} else {

			engText1.text = threeEnglish [0];
			engText2.text = threeEnglish [1];
			engText3.text = threeEnglish [2];

			irishText1.text = threeIrish [0];
			irishText2.text = threeIrish [1];
			irishText3.text = threeIrish [2];

			option1.text = threeIrish [0];
			option2.text = threeIrish [1];
			option3.text = threeIrish [2];
		}


	}

	public void GetPresentTense()
	{
		databaseCount = irishFromDatabase.Count;
		numberOfRows = 5;

		System.Random rando = new System.Random ();
		int randomNum = rando.Next (0, databaseCount - numberOfRows);
		databaseIndex = randomNum;
		string[] allIrishWords = new string[5];
		string[] allEnglishWords = new string[5];

		int indexForBoth = 0;
		for (int i = databaseIndex; i < databaseIndex + numberOfRows; i++) 
		{
			allIrishWords [indexForBoth] = irishFromDatabase [i].ToString();
			allEnglishWords [indexForBoth] = englishFromDatabase [i].ToString();
			indexForBoth++;
		}
		System.Random r = new System.Random();
		int random = r.Next(0, 4);
		int temp = random;

		if (random % 2 == 0) {
			irishText.text = allIrishWords [random];
			IrishCorrectAnswer.text = irishText.text;
			rightAnswer = allEnglishWords [temp];
			threeEnglish [0] = rightAnswer;
		} else {
			irishText.text = allEnglishWords [random];
			IrishCorrectAnswer.text = irishText.text;
			rightAnswer = allIrishWords [temp];
			swappedOver = true;
			threeIrish [0] = rightAnswer;
		}
		EnglishCorrectAnswer.text = rightAnswer;
		if (!swappedOver) {
			for (int i = 0; i < 2; i++) {
				System.Random rand = new System.Random ();
				int randomEnglish = rand.Next (0, 4);
				threeEnglish [1] = allEnglishWords [randomEnglish];
				if (threeEnglish [1] == threeEnglish [0]) {
					i = 0;
				}
			}
			for (int i = 0; i < 2; i++) {
				System.Random rand = new System.Random ();
				int randomEnglish = rand.Next (0, 4);
				threeEnglish [2] = allEnglishWords [randomEnglish];
				if (threeEnglish [2] == threeEnglish [0] || threeEnglish [2] == threeEnglish [1]) {
					i = 0;
				}
			}
		} else {
			for (int i = 0; i < 2; i++) {
				System.Random rand = new System.Random ();
				int randomIrish = rand.Next (0, 4);
				threeIrish [1] = allIrishWords [randomIrish];
				if (threeIrish [1] == threeIrish [0]) {
					i = 0;
				}
			}
			for (int i = 0; i < 2; i++) {
				System.Random rand = new System.Random ();
				int randomIrish = rand.Next (0, 4);
				threeIrish [2] = allIrishWords [randomIrish];
				if (threeIrish [2] == threeIrish [0] || threeIrish [2] == threeIrish [1]) {
					i = 0;
				}
			}
		}
		if (!swappedOver) {
			RandomizeArray (threeEnglish);

			option1.text = threeEnglish [0];
			option2.text = threeEnglish [1];
			option3.text = threeEnglish [2];
		} else {
			RandomizeArray (threeIrish);

			option1.text = threeIrish [0];
			option2.text = threeIrish [1];
			option3.text = threeIrish [2];
		}
	}

	void EasyGetPresentTense()
	{
		theIrishPanel.gameObject.SetActive (false);
		databaseCount = irishFromDatabase.Count;
		numberOfRows = 5;

		System.Random rando = new System.Random ();
		int randomNum = rando.Next (0, databaseCount - numberOfRows);
		databaseIndex = randomNum;
		Debug.Log ("databaseCount = irishFromDatabase.Count; " + databaseCount);
		string[] allIrishWords = new string[5];
		string[] allEnglishWords = new string[5];
		string[] threeIrish = new string[3];

		int indexForBoth = 0;

		for (int i = databaseIndex; i < databaseIndex + numberOfRows; i++) 
		{
			allIrishWords [indexForBoth] = irishFromDatabase [i].ToString();
			allEnglishWords [indexForBoth] = englishFromDatabase [i].ToString();
			indexForBoth++;
		}
			
		System.Random r = new System.Random();
		int random = r.Next(0, 4);
		int temp = random;

		if (random % 2 == 0) {
			irishText.text = allIrishWords [random];
			IrishCorrectAnswer.text = irishText.text;
			threeIrish [0] = allIrishWords [random];
			rightAnswer = allEnglishWords [temp];
			threeEnglish [0] = rightAnswer;
		} else {
			irishText.text = allEnglishWords [random];
			IrishCorrectAnswer.text = irishText.text;
			threeEnglish [0] = allEnglishWords [random];
			rightAnswer = allIrishWords [temp];
			threeIrish [0] = rightAnswer;
			swappedOver = true;
		}
		EnglishCorrectAnswer.text = rightAnswer;
		for (int i = 0; i < 2; i++) 
		{
			System.Random rand = new System.Random();
			int randomWord = rand.Next(0, 4);
			threeIrish [1] = allIrishWords [randomWord];
			threeEnglish[1] = allEnglishWords[randomWord];
			if (threeEnglish [1] == threeEnglish [0] || threeIrish [1] == threeIrish [0] ) 
			{
				i = 0;
			}
		}
		for (int i = 0; i < 2; i++) 
		{
			System.Random rand = new System.Random();
			int randomWord = rand.Next(0, 4);
			threeIrish [2] = allIrishWords [randomWord];
			threeEnglish[2] = allEnglishWords[randomWord];
			if (threeEnglish [2] == threeEnglish [0] || threeEnglish [2] == threeEnglish [1] || 
				threeIrish [2] == threeIrish [0] || threeIrish [2] == threeIrish [1]) 
			{
				i = 0;
			}
		}

		if (!swappedOver) {

			engText1.text = threeEnglish [0];
			engText2.text = threeEnglish [1];
			engText3.text = threeEnglish [2];

			irishText1.text = threeIrish [0];
			irishText2.text = threeIrish [1];
			irishText3.text = threeIrish [2];

			option1.text = threeEnglish [0];
			option2.text = threeEnglish [1];
			option3.text = threeEnglish [2];
		} else {

			engText1.text = threeEnglish [0];
			engText2.text = threeEnglish [1];
			engText3.text = threeEnglish [2];

			irishText1.text = threeIrish [0];
			irishText2.text = threeIrish [1];
			irishText3.text = threeIrish [2];

			option1.text = threeIrish [0];
			option2.text = threeIrish [1];
			option3.text = threeIrish [2];
		}
	}

	public void GetFutureTense()
	{
		
		databaseCount = irishFromDatabase.Count;
		numberOfRows = 4;

		System.Random rando = new System.Random ();
		int randomNum = rando.Next (0, databaseCount - numberOfRows);
		databaseIndex = randomNum;

		Debug.Log ("databaseCount = irishFromDatabase.Count; " + databaseCount);

		string[] allIrishWords = new string[4];
		string[] allEnglishWords = new string[4];

		int indexForBoth = 0;
		for (int i = databaseIndex; i < databaseIndex + numberOfRows; i++) 
		{
			allIrishWords [indexForBoth] = irishFromDatabase [i].ToString();
			allEnglishWords [indexForBoth] = englishFromDatabase [i].ToString();
			indexForBoth++;
		}

		System.Random r = new System.Random();
		int random = r.Next(0, 3);
		int temp = random;

		if (random % 2 == 0) {
			irishText.text = allIrishWords [random];
			IrishCorrectAnswer.text = irishText.text;
			rightAnswer = allEnglishWords [temp];
			threeEnglish [0] = rightAnswer;
		} else {
			irishText.text = allEnglishWords [random];
			IrishCorrectAnswer.text = irishText.text;
			rightAnswer = allIrishWords [temp];
			swappedOver = true;
			threeIrish [0] = rightAnswer;
		}
		EnglishCorrectAnswer.text = rightAnswer;
		if (!swappedOver) {
			for (int i = 0; i < 2; i++) {
				System.Random rand = new System.Random ();
				int randomEnglish = rand.Next (0, 3);
				threeEnglish [1] = allEnglishWords [randomEnglish];
				if (threeEnglish [1] == threeEnglish [0]) {
					i = 0;
				}
			}
			for (int i = 0; i < 2; i++) {
				System.Random rand = new System.Random ();
				int randomEnglish = rand.Next (0, 3);
				threeEnglish [2] = allEnglishWords [randomEnglish];
				if (threeEnglish [2] == threeEnglish [0] || threeEnglish [2] == threeEnglish [1]) {
					i = 0;
				}
			}
		} else {
			for (int i = 0; i < 2; i++) {
				System.Random rand = new System.Random ();
				int randomIrish = rand.Next (0, 3);
				threeIrish [1] = allIrishWords [randomIrish];
				if (threeIrish [1] == threeIrish [0]) {
					i = 0;
				}
			}
			for (int i = 0; i < 2; i++) {
				System.Random rand = new System.Random ();
				int randomIrish = rand.Next (0, 3);
				threeIrish [2] = allIrishWords [randomIrish];
				if (threeIrish [2] == threeIrish [0] || threeIrish [2] == threeIrish [1]) {
					i = 0;
				}
			}
		}
		if (!swappedOver) {
			RandomizeArray (threeEnglish);

			option1.text = threeEnglish [0];
			option2.text = threeEnglish [1];
			option3.text = threeEnglish [2];
		} else {
			RandomizeArray (threeIrish);

			option1.text = threeIrish [0];
			option2.text = threeIrish [1];
			option3.text = threeIrish [2];
		}
	}

	void EasyGetFutureTense()
	{
		theIrishPanel.gameObject.SetActive (false);

		databaseCount = irishFromDatabase.Count;
		numberOfRows = 4;

		System.Random rando = new System.Random ();
		int randomNum = rando.Next (0, databaseCount - numberOfRows);
		databaseIndex = randomNum;

		string[] allIrishWords = new string[4];
		string[] allEnglishWords = new string[4];
		string[] threeIrish = new string[3];

		int indexForBoth = 0;

		for (int i = databaseIndex; i < databaseIndex + numberOfRows; i++) 
		{
			allIrishWords [indexForBoth] = irishFromDatabase [i].ToString();
			allEnglishWords [indexForBoth] = englishFromDatabase [i].ToString();
			
			indexForBoth++;
		}

		System.Random r = new System.Random();
		int random = r.Next(0, 3);
		int temp = random;
	
		if (random % 2 == 0) {
			irishText.text = allIrishWords [random];
			IrishCorrectAnswer.text = irishText.text;
			threeIrish [0] = allIrishWords [random];
			rightAnswer = allEnglishWords [temp];
			threeEnglish [0] = rightAnswer;
		} else {
			irishText.text = allEnglishWords [random];
			IrishCorrectAnswer.text = irishText.text;
			threeEnglish [0] = allEnglishWords [random];
			rightAnswer = allIrishWords [temp];
			threeIrish [0] = rightAnswer;
			swappedOver = true;
		}
		EnglishCorrectAnswer.text = rightAnswer;
		for (int i = 0; i < 2; i++) 
		{
			System.Random rand = new System.Random();
			int randomWord = rand.Next(0, 3);
			threeIrish [1] = allIrishWords [randomWord];
			threeEnglish[1] = allEnglishWords[randomWord];
			if (threeEnglish [1] == threeEnglish [0] || threeIrish [1] == threeIrish [0] ) 
			{
				i = 0;
			}
		}
		for (int i = 0; i < 2; i++) 
		{
			System.Random rand = new System.Random();
			int randomWord = rand.Next(0, 3);
			threeIrish [2] = allIrishWords [randomWord];
			threeEnglish[2] = allEnglishWords[randomWord];
			if (threeEnglish [2] == threeEnglish [0] || threeEnglish [2] == threeEnglish [1] || 
				threeIrish [2] == threeIrish [0] || threeIrish [2] == threeIrish [1]) 
			{
				i = 0;
			}
		}

		if (!swappedOver) {

			engText1.text = threeEnglish [0];
			engText2.text = threeEnglish [1];
			engText3.text = threeEnglish [2];

			irishText1.text = threeIrish [0];
			irishText2.text = threeIrish [1];
			irishText3.text = threeIrish [2];

			option1.text = threeEnglish [0];
			option2.text = threeEnglish [1];
			option3.text = threeEnglish [2];
		} else {

			engText1.text = threeEnglish [0];
			engText2.text = threeEnglish [1];
			engText3.text = threeEnglish [2];

			irishText1.text = threeIrish [0];
			irishText2.text = threeIrish [1];
			irishText3.text = threeIrish [2];

			option1.text = threeIrish [0];
			option2.text = threeIrish [1];
			option3.text = threeIrish [2];
		}
	}

	public void GetPastTense()
	{
		databaseCount = irishFromDatabase.Count;
		numberOfRows = 4;

		System.Random rando = new System.Random ();
		int randomNum = rando.Next (0, databaseCount - numberOfRows);
		databaseIndex = randomNum;
		Debug.Log ("databaseCount = irishFromDatabase.Count; " + databaseCount);
		string[] allIrishWords = new string[4];
		string[] allEnglishWords = new string[4];

		int indexForBoth = 0;
		for (int i = databaseIndex; i < databaseIndex + numberOfRows; i++) 
		{
			allIrishWords [indexForBoth] = irishFromDatabase [i].ToString();
			allEnglishWords [indexForBoth] = englishFromDatabase [i].ToString();
			indexForBoth++;
		}

		System.Random r = new System.Random();
		int random = r.Next(0, 3);
		int temp = random;

		if (random % 2 == 0) {
			irishText.text = allIrishWords [random];
			IrishCorrectAnswer.text = irishText.text;
			rightAnswer = allEnglishWords [temp];
			threeEnglish [0] = rightAnswer;
		} else {
			irishText.text = allEnglishWords [random];
			IrishCorrectAnswer.text = irishText.text;
			rightAnswer = allIrishWords [temp];
			swappedOver = true;
			threeIrish [0] = rightAnswer;
		}
		EnglishCorrectAnswer.text = rightAnswer;
		if (!swappedOver) {
			for (int i = 0; i < 2; i++) {
				System.Random rand = new System.Random ();
				int randomEnglish = rand.Next (0, 3);
				threeEnglish [1] = allEnglishWords [randomEnglish];
				if (threeEnglish [1] == threeEnglish [0]) {
					i = 0;
				}
			}
			for (int i = 0; i < 2; i++) {
				System.Random rand = new System.Random ();
				int randomEnglish = rand.Next (0, 3);
				threeEnglish [2] = allEnglishWords [randomEnglish];
				if (threeEnglish [2] == threeEnglish [0] || threeEnglish [2] == threeEnglish [1]) {
					i = 0;
				}
			}
		} else {
			for (int i = 0; i < 2; i++) {
				System.Random rand = new System.Random ();
				int randomIrish = rand.Next (0, 3);
				threeIrish [1] = allIrishWords [randomIrish];
				if (threeIrish [1] == threeIrish [0]) {
					i = 0;
				}
			}
			for (int i = 0; i < 2; i++) {
				System.Random rand = new System.Random ();
				int randomIrish = rand.Next (0, 3);
				threeIrish [2] = allIrishWords [randomIrish];
				if (threeIrish [2] == threeIrish [0] || threeIrish [2] == threeIrish [1]) {
					i = 0;
				}
			}
		}
		if (!swappedOver) {
			RandomizeArray (threeEnglish);

			option1.text = threeEnglish [0];
			option2.text = threeEnglish [1];
			option3.text = threeEnglish [2];
		} else {
			RandomizeArray (threeIrish);

			option1.text = threeIrish [0];
			option2.text = threeIrish [1];
			option3.text = threeIrish [2];
		}
	}

	void EasyGetPastTense()
	{
		theIrishPanel.gameObject.SetActive (false);

		databaseCount = irishFromDatabase.Count;
		numberOfRows = 4;

		System.Random rando = new System.Random ();
		int randomNum = rando.Next (0, databaseCount - numberOfRows);
		databaseIndex = randomNum;

		string[] allIrishWords = new string[4];
		string[] allEnglishWords = new string[4];
		string[] threeIrish = new string[3];

		int indexForBoth = 0;

		for (int i = databaseIndex; i < databaseIndex + numberOfRows; i++) 
		{
			allIrishWords [indexForBoth] = irishFromDatabase [i].ToString();
			allEnglishWords [indexForBoth] = englishFromDatabase [i].ToString();
			indexForBoth++;
		}

		System.Random r = new System.Random();
		int random = r.Next(0, 3);
		int temp = random;
		if (random % 2 == 0) {
			irishText.text = allIrishWords [random];
			IrishCorrectAnswer.text = irishText.text;
			threeIrish [0] = allIrishWords [random];
			rightAnswer = allEnglishWords [temp];
			threeEnglish [0] = rightAnswer;
		} else {
			irishText.text = allEnglishWords [random];
			IrishCorrectAnswer.text = irishText.text;
			threeEnglish [0] = allEnglishWords [random];
			rightAnswer = allIrishWords [temp];
			threeIrish [0] = rightAnswer;
			swappedOver = true;
		}
		EnglishCorrectAnswer.text = rightAnswer;
		for (int i = 0; i < 2; i++) 
		{
			System.Random rand = new System.Random();
			int randomWord = rand.Next(0, 3);
			threeIrish [1] = allIrishWords [randomWord];
			threeEnglish[1] = allEnglishWords[randomWord];
			if (threeEnglish [1] == threeEnglish [0] || threeIrish [1] == threeIrish [0] ) 
			{
				i = 0;
			}
		}
		for (int i = 0; i < 2; i++) 
		{
			System.Random rand = new System.Random();
			int randomWord = rand.Next(0, 3);
			threeIrish [2] = allIrishWords [randomWord];
			threeEnglish[2] = allEnglishWords[randomWord];
			if (threeEnglish [2] == threeEnglish [0] || threeEnglish [2] == threeEnglish [1] || 
				threeIrish [2] == threeIrish [0] || threeIrish [2] == threeIrish [1]) 
			{
				i = 0;
			}
		}

		if (!swappedOver) {

			engText1.text = threeEnglish [0];
			engText2.text = threeEnglish [1];
			engText3.text = threeEnglish [2];

			irishText1.text = threeIrish [0];
			irishText2.text = threeIrish [1];
			irishText3.text = threeIrish [2];

			option1.text = threeEnglish [0];
			option2.text = threeEnglish [1];
			option3.text = threeEnglish [2];
		} else {

			engText1.text = threeEnglish [0];
			engText2.text = threeEnglish [1];
			engText3.text = threeEnglish [2];

			irishText1.text = threeIrish [0];
			irishText2.text = threeIrish [1];
			irishText3.text = threeIrish [2];

			option1.text = threeIrish [0];
			option2.text = threeIrish [1];
			option3.text = threeIrish [2];
		}
	}

	public void AddScore(int scoreToAdd)
	{
		scoreCount += scoreToAdd;
		scoreText.text = scoreCount.ToString();

	}

	public void SetHighScore()
	{
		if (presentTense == true && easy == false) {
			if (PlayerPrefs.HasKey ("PresentTense")) {
				if (scoreCount > PlayerPrefs.GetInt ("PresentTense")) {
					PlayerPrefs.SetInt ("PresentTense", scoreCount);
				}
			} else {
				PlayerPrefs.SetInt ("PresentTense", scoreCount);
			}
		} 
		else if (presentTense == true && easy == true) {
			
			if (PlayerPrefs.HasKey ("EasyPresentTense")) {
				if (scoreCount > PlayerPrefs.GetInt ("EasyPresentTense")) {
					PlayerPrefs.SetInt ("EasyPresentTense", scoreCount);
				}
			} else {
				PlayerPrefs.SetInt ("EasyPresentTense", scoreCount);
			}
				
		}
		else if (futureTense && easy == false) {
			if (PlayerPrefs.HasKey ("FutureTense")) {
				if (scoreCount > PlayerPrefs.GetInt ("FutureTense")) {
					PlayerPrefs.SetInt ("FutureTense", scoreCount);
				}
			} else {
				PlayerPrefs.SetInt ("FutureTense", scoreCount);
			}
		}
		else if (futureTense && easy == true) {
			if (PlayerPrefs.HasKey ("EasyFutureTense")) {
				if (scoreCount > PlayerPrefs.GetInt ("EasyFutureTense")) {
					PlayerPrefs.SetInt ("EasyFutureTense", scoreCount);
				}
			} else {
				PlayerPrefs.SetInt ("EasyFutureTense", scoreCount);
			}
		}else if (pastTense && easy == false) {
			if (PlayerPrefs.HasKey ("PastTense")) {
				if (scoreCount > PlayerPrefs.GetInt ("PastTense")) {
					PlayerPrefs.SetInt ("PastTense", scoreCount);
				}
			} else {
				PlayerPrefs.SetInt ("PastTense", scoreCount);
			}
		}
		else if (pastTense && easy == true) {
			if (PlayerPrefs.HasKey ("EasyPastTense")) {
				if (scoreCount > PlayerPrefs.GetInt ("EasyPastTense")) {
					PlayerPrefs.SetInt ("EasyPastTense", scoreCount);
				}
			} else {
				PlayerPrefs.SetInt ("EasyPastTense", scoreCount);
			}
		} else if (mixedTense && easy == false) {
			if (PlayerPrefs.HasKey ("MixedTense")) {
				if (scoreCount > PlayerPrefs.GetInt ("MixedTense")) {
					PlayerPrefs.SetInt ("MixedTense", scoreCount);
				}
			} else {
				PlayerPrefs.SetInt ("MixedTense", scoreCount);
			}
		} else {
			if (PlayerPrefs.HasKey ("EasyMixedTense")) {
				if (scoreCount > PlayerPrefs.GetInt ("EasyMixedTense")) {
					PlayerPrefs.SetInt ("EasyMixedTense", scoreCount);
				}
			} else {
				PlayerPrefs.SetInt ("EasyMixedTense", scoreCount);
			}
		}
	}

	public void GetHighScore()
	{
		if (presentTense == true && easy == false) {
			if (PlayerPrefs.HasKey ("PresentTense")) {
				currentHighScoreText.text = PlayerPrefs.GetInt ("PresentTense").ToString();
				}
		} 
		else if (presentTense == true && easy == true) {
			if (PlayerPrefs.HasKey ("EasyPresentTense")) {
				currentHighScoreText.text = PlayerPrefs.GetInt ("EasyPresentTense").ToString();
			}
		}
			
		else if (futureTense && easy == false) {
			if (PlayerPrefs.HasKey ("FutureTense")) {
				currentHighScoreText.text = PlayerPrefs.GetInt ("FutureTense").ToString();
			}
		}
		else if (futureTense && easy == true) {
			if (PlayerPrefs.HasKey ("EasyFutureTense")) {
				currentHighScoreText.text = PlayerPrefs.GetInt ("EasyFutureTense").ToString();
			}
		}else if (pastTense && easy == false) {
			if (PlayerPrefs.HasKey ("PastTense")) {
				currentHighScoreText.text = PlayerPrefs.GetInt ("PastTense").ToString();
			}
		}
		else if (pastTense && easy == true) {
			if (PlayerPrefs.HasKey ("EasyPastTense")) {
				currentHighScoreText.text = PlayerPrefs.GetInt ("EasyPastTense").ToString();
			}
		} else if (mixedTense && easy == false) {
			if (PlayerPrefs.HasKey ("MixedTense")) {
				currentHighScoreText.text = PlayerPrefs.GetInt ("MixedTense").ToString();
			}
		} else {
			if (PlayerPrefs.HasKey ("EasyMixedTense")) {
				currentHighScoreText.text = PlayerPrefs.GetInt ("EasyMixedTense").ToString();
			}
		}
	}

	public void ResetGame()
	{
		changeQuestion = true;
		pointOfChangeReached = true;
		player.gameObject.SetActive (true);
		irishText.gameObject.SetActive (true);
		if (easy) {
			secondsLeft = 8.0f;
			theMixedPanel.gameObject.SetActive (true);
			countdownObject.SetActive (true);
		} 
		else {
			theIrishPanel.gameObject.SetActive (true);
		}
		theDeathScreen.gameObject.SetActive (false);
		countdownObject.SetActive (true);
		player.transform.position = PlayerController.respawnPosition;
		platformSpawner.transform.position = platformSpawnerResetPos;
		platformSpawner.DestroyAllPlatforms ();
		platformSpawner.Start();
		scoreCount = 0;
		scoreText.text = scoreCount.ToString ();

	}

	public void OpenDB(string p)
	{
		Debug.Log("Call to OpenDB:" + p);
		string filepath = "";
		
		// check if file exists in Application.persistentDataPath
		filepath = Application.persistentDataPath + "/" + p;
		Debug.Log ("PERSISTENT: " + Application.persistentDataPath);

		filepath = Application.persistentDataPath + "/" + p;  
	
		Debug.Log ("FILEPATH" + filepath);
		if(!File.Exists(filepath))
		{
			Debug.LogWarning("File \"" + filepath + "\" does not exist. Attempting to create from \"" +
				Application.dataPath + "!/assets/" + p);

			WWW loadDB;

			loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + p);
			
			while(!loadDB.isDone) {
				Debug.Log ("Loading");
			}
			Debug.Log ("BYTES DOWNLOADED: " + loadDB.bytesDownloaded);
			byte[] byteArray = loadDB.bytes;
			if (byteArray == null) {
				Debug.Log ("Empty");
			}
			
			File.WriteAllBytes(filepath, byteArray);
		
		
		connection = "URI=file:" + filepath;
		Debug.Log("Establishing connection to: " + connection);
		dbcon = new SqliteConnection(connection);
		dbcon.Open();
	}

	public void CloseDB(){
		reader.Close(); 
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbcon.Close();
		dbcon = null;
	}




}
	

