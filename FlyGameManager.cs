using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite; 
using System.Data; 
using System;
using System.IO;
using System.Reflection;

public class FlyGameManager : MonoBehaviour {

	public string platformClicked;

	private string connection;
	private IDbConnection dbcon;
	private IDbCommand dbcmd;
	private IDataReader reader;
	private string p = "LanguageData.db";

	public ObstacleSpawner theObsSpawner;

	public DeathMenu theDeathScreen;
	public Text deathScreenScoreText;
	public Text currentHighScoreText;

	public Text irishRightAnswer;
	public Text englishRightAnswer;

	public Text countdownText;
	public float secondsLeft; 
	public GameObject countdownObject;

	public GameObject PlatformOne;
	public GameObject PlatformTwo;
	public GameObject PlatformThree;

	public Text platformOneText;
	public Text platformTwoText;
	public Text platformThreeText;
	public string platOne,platTwo,platThree;

	public Text theSentence;

	public GameObject player;

	public GameObject topCube, bottomCube, middleCube;


	private string[] tenseArray = { "past", "present", "future"};
	private ArrayList presentVerbs = new ArrayList();
	private ArrayList pastVerbs = new ArrayList();
	private ArrayList futureVerbs = new ArrayList();


	string vtense,vform,vpform,btext, ctext, lemmaText = "";
	string firstTenseToGet,secondTenseToGet = "";
	public string rightAnswer = "";

	public int score;
	public Text scoreText;

	void Start () {

		score = 0;

		countdownObject = GameObject.Find ("CountDown");
		secondsLeft = 8.0f;


		GenerateQuestions();
		GetRightAnswerFromDatabase ();
		GetPlatformStrings ();
		AssignPlatformStrings ();

		ChangeSentenceText ();
			
	}
	
	// Update is called once per frame
	void Update () {
		if (secondsLeft < 0f) 
		{
			
			countdownObject.gameObject.SetActive (false);

			PlatformOne.gameObject.SetActive (false);
			PlatformTwo.gameObject.SetActive (false);
			PlatformThree.gameObject.SetActive (false);
		}
	
		secondsLeft -= Time.deltaTime;
		countdownText.text = secondsLeft.ToString ();

	}

	public void GenerateQuestions()
	{
		string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/LanguageData.db";

		
		using(dbcon = (IDbConnection)new SqliteConnection (connection)){
		OpenDB(p);

		
			using (IDbCommand dbcmd = dbcon.CreateCommand ()) {
				string sqlQuery = "DELETE FROM QuizItem";
				dbcmd.CommandText = sqlQuery;
				dbcmd.ExecuteReader ();
			
			}
		
			using (IDbCommand dbcmd = dbcon.CreateCommand ()) {
		
				string sqlQuery = "INSERT INTO QuizItem (q_id,q_lemma,q_tense,q_verb,q_person,q_body,q_context) SELECT Verb.v_id,v_lemma,v_tense,v_form,v_p_form,b_text,c_text FROM Verb,IrregularPerson,Body,Context WHERE Verb.v_num = IrregularPerson.v_num AND Body.v_id = Verb.b_id AND Context.c_tense = Verb.v_context AND Body.c_id = Context.b_id";
				dbcmd.CommandText = sqlQuery;
				dbcmd.ExecuteReader ();
			}
		}
	}



	public void GetRightAnswerFromDatabase() {
	
		using (dbcon = (IDbConnection)new SqliteConnection (connection)) {
			OpenDB (p);
			
			using (IDbCommand dbcmd = dbcon.CreateCommand ()) {
				string sqlQuery = "SELECT QuizItem.q_tense,q_verb,q_person,q_body,q_context,q_lemma FROM QuizItem ORDER BY RANDOM() LIMIT 1";
				dbcmd.CommandText = sqlQuery;
				using (IDataReader reader = dbcmd.ExecuteReader ()) {
					while (reader.Read ()) {
						vtense = reader.GetString (0);
						vform = reader.GetString (1);
						vpform = reader.GetString (2);
						btext = reader.GetString (3);
						ctext = reader.GetString (4);
						lemmaText = reader.GetString (5);

					}
					rightAnswer = vform;
					englishRightAnswer.text = rightAnswer;
				}
			}
		}
	}	

	public void GetPlatformStrings(){


		if (vtense == "past") {
			firstTenseToGet = "present";
			secondTenseToGet = "future";
		} else if (vtense == "present") {
			firstTenseToGet = "past";
			secondTenseToGet = "future";
		} else if (vtense == "future") {
			firstTenseToGet = "past";
			secondTenseToGet = "present";
		}

			using (dbcon = (IDbConnection)new SqliteConnection (connection)) {
				dbcon.Open ();

			using (IDbCommand dbcmd = dbcon.CreateCommand ()) {
				string sqlQuery = "SELECT q_verb FROM QuizItem WHERE q_tense like '%" + firstTenseToGet + "%' AND q_person like '%" + vpform + "%' AND q_body like '%" + btext + "%' AND q_lemma like '%" + lemmaText + "%' ORDER BY RANDOM() LIMIT 1";
				dbcmd.CommandText = sqlQuery;
				using (IDataReader reader = dbcmd.ExecuteReader ()) {
					while (reader.Read ()) {	
						platOne = reader.GetString (0);

					}

				}
			}

			using (IDbCommand dbcmd = dbcon.CreateCommand ()) {
				string sqlQuery = "SELECT q_verb FROM QuizItem WHERE q_tense like '%" + secondTenseToGet + "%' AND q_person like '%" + vpform + "%' AND q_body like '%" + btext + "%' AND q_lemma like '%" + lemmaText + "%' ORDER BY RANDOM() LIMIT 1";
				dbcmd.CommandText = sqlQuery;
				using (IDataReader reader = dbcmd.ExecuteReader ()) {
					while (reader.Read ()) {	
						platTwo = reader.GetString (0);

					}

				}
			}
		}
	}

	public void AssignPlatformStrings(){

		bool changed = false;

		platThree = rightAnswer;
		string[] platTextArray = { platOne, platTwo, platThree };
		for (int i = 0; i < platTextArray.Length; i++) {
			Debug.Log ("Plat Text Array " + i + ":" + platTextArray [i]);
		}
		for (int i = 0; i < platTextArray.Length; i++) {
			Debug.Log ("Plat Text Array " + i + ":" + platTextArray [i]);
		}
		Debug.Log ("platText1: " + platformOneText.text);
		Debug.Log ("platText2: " + platformTwoText.text);
		Debug.Log ("platText3: " +platformThreeText.text);
		System.Random rando = new System.Random();
		int randomNum = rando.Next (0, 3);
		platformOneText.text = platTextArray[randomNum];
		randomNum = rando.Next (0, 3);
		while (changed == false) {
			if (platformOneText.text == platTextArray [randomNum]) {
				randomNum = rando.Next (0, 3);
			} 
			else {
				platformTwoText.text = platTextArray [randomNum];
				changed = true;
			}
		}
		changed = false;
		randomNum = rando.Next (0, 3);
		while (changed == false) {
			if (platformOneText.text == platTextArray [randomNum] || platformTwoText.text == platTextArray [randomNum]) {
				randomNum = rando.Next (0, 3);
			} 
			else {
				platformThreeText.text = platTextArray [randomNum];
				changed = true;
			}
		}

	}

	public void RandomizeArray(string [] arr)
	{
		System.Random rand = new System.Random();

		for (int i = 0; i < arr.Length - 1; i++)
		{
			int j = rand.Next(i, arr.Length);
			string temp = arr[i];
			arr[i] = arr[j];
			arr[j] = temp;
		}
	}

	public void ChangeSentenceText(){
		if(vpform.Length < 2){
			theSentence.text = "(" + lemmaText + ")" + " " + btext +  " " + ctext; 
		} else {
			theSentence.text =  "(" + lemmaText + ")" + " " + vpform + " " + btext +  " " + ctext; 
		}

		irishRightAnswer.text = theSentence.text;
	}

	public void AddScore(int scoreToAdd)
	{
		score += scoreToAdd;
		scoreText.text = score.ToString();
		Debug.Log ("SCORE TEXT: " + scoreText.text);
	}

	public void SetFlyHighScore()
	{
			if (PlayerPrefs.HasKey ("FlyGameScore")) {
				if (score > PlayerPrefs.GetInt ("FlyGameScore")) {
					PlayerPrefs.SetInt ("FlyGameScore", score);
				}
			} else {
				PlayerPrefs.SetInt ("FlyGameScore", score);
			}

	}

	public void GetFlyHighScore()
	{
			if (PlayerPrefs.HasKey ("FlyGameScore")) {
			currentHighScoreText.text = PlayerPrefs.GetInt ("FlyGameScore").ToString ();
			}

	}

	public void ResetGame()
	{
		theDeathScreen.gameObject.SetActive (false);

		score = 0;
		scoreText.text = score.ToString ();

		PlatformOne.gameObject.SetActive (true);
		PlatformTwo.gameObject.SetActive (true);
		PlatformThree.gameObject.SetActive (true);
		topCube.SetActive (true);
		middleCube.SetActive (true);
		bottomCube.SetActive (true);



		GetRightAnswerFromDatabase ();
		GetPlatformStrings ();
		AssignPlatformStrings ();
		ChangeSentenceText ();
		countdownObject.gameObject.SetActive (true);
		secondsLeft = 8.0f;
		theObsSpawner.GetWall ();
		theObsSpawner.theWallToDelete.SetActive (false);
		theObsSpawner.TheCuboid.transform.position = theObsSpawner.StartPoint.transform.position;


		player.SetActive (true);
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


}
