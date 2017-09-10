using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite; 
using System.Data; 
using System;

public class Conjugator : MonoBehaviour {

	string verb,conNum = "";
	Hashtable hashVerbs = new Hashtable();

	// Use this for initialization
	void Start () {
		GetFromDatabase ();
		
		foreach(DictionaryEntry entry in hashVerbs)
		{
			Debug.Log (entry.Key.ToString() + ':' + entry.Value.ToString());
			GetFutureTense (entry.Value.ToString(), entry.Key.ToString());
			GetPresentTense(entry.Value.ToString(), entry.Key.ToString());
			GetPastTense(entry.Value.ToString(), entry.Key.ToString());
		}
	}

	void GetFutureTense(string conjugation, string verbToCheck){
		string root = verbToCheck;
		string body = "null";
		string context = "null";
		string[] verbsWithPronouns = new string[7];
		verbToCheck = verbToCheck.ToLower ();

		if (conjugation == "1") {

			char lastVowel = GetLastVowel (verbToCheck);

			if (IsBroad (lastVowel)) {
				verbsWithPronouns [6] = verbToCheck + "faimid"; 
				verbToCheck = verbToCheck + "faidh";

			} else if (IsSlender (lastVowel)) {
				verbsWithPronouns [0] = verbToCheck + "fidh"; 
				verbsWithPronouns [6] = verbToCheck + "fimid";
				verbToCheck = verbToCheck + "fidh";

			}

			verbsWithPronouns [0] = verbToCheck + " " + "mé";
			verbsWithPronouns [1] = verbToCheck + " " + "tú";
			verbsWithPronouns [2] = verbToCheck + " " + "sé";
			verbsWithPronouns [3] = verbToCheck + " " + "sí";
			verbsWithPronouns [4] = verbToCheck + " " + "siad";
			verbsWithPronouns [5] = verbToCheck + " " + "sibh";

		}

		else if (conjugation == "2") {
	
			verbToCheck = ConjugateFuture (verbToCheck);
	
			char lastVowel = GetLastVowel (verbToCheck);

			if (IsBroad (lastVowel)) {
				verbsWithPronouns [6] = verbToCheck + "óimid"; 
				verbToCheck = verbToCheck + "óidh";

			} else if (IsSlender (lastVowel)) {
				verbsWithPronouns [6] = verbToCheck + "eoimid";
				verbToCheck = verbToCheck + "eoidh";

			}


			verbsWithPronouns [0] = verbToCheck + " " + "mé";
			verbsWithPronouns [1] = verbToCheck + " " + "tú";
			verbsWithPronouns [2] = verbToCheck + " " + "sé";
			verbsWithPronouns [3] = verbToCheck + " " + "sí";
			verbsWithPronouns [4] = verbToCheck + " " + "siad";
			verbsWithPronouns [5] = verbToCheck + " " + "sibh";

		}

		string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/LanguageData.db";
		IDbConnection dbconn;
		dbconn = (IDbConnection)new SqliteConnection (conn);
		using (dbconn = (IDbConnection)new SqliteConnection (conn)) {
			dbconn.Open ();

			using (IDbCommand dbcmd = dbconn.CreateCommand ()) {

				string sqlQuery = "SELECT Body.b_text,Context.c_text FROM Body,Context,RegularVerb WHERE Body.v_id = RegularVerb.b_id AND Context.c_tense = '1' AND Body.c_id = Context.b_id AND RegularVerb.rv_lemma like '%" + root + "%' LIMIT 1";
				dbcmd.CommandText = sqlQuery;
				using (IDataReader reader = dbcmd.ExecuteReader())
				{
					while (reader.Read())
					{
						body = reader.GetString (0);
						context = reader.GetString (1);
					}

				}
			}

			for (int i = 0; i < verbsWithPronouns.Length; i++) {
				verbsWithPronouns [i] = FirstLetterToUpper (verbsWithPronouns [i]);
				using (IDbCommand dbcmd = dbconn.CreateCommand ()) {
					if (verbsWithPronouns [i].Contains (" ")) {
						string[] split = verbsWithPronouns [i].Split (' ');
						string sqlQuery = "INSERT INTO QuizItem (q_id,q_lemma,q_tense,q_verb,q_person,q_body,q_context) VALUES (0,'" + FirstLetterToUpper(root) + "','future','" + split [0] + "','" + split [1] + "','" + body + "','" + context + "')";
						dbcmd.CommandText = sqlQuery;
						dbcmd.ExecuteReader ();
					} else {
						string sqlQuery = "INSERT INTO QuizItem (q_id,q_lemma,q_tense,q_verb,q_person,q_body,q_context) VALUES (0,'" + FirstLetterToUpper(root) + "','future','" + verbsWithPronouns[i] + "','" + "" + "','" + body + "','" + context + "')";
						dbcmd.CommandText = sqlQuery;
						dbcmd.ExecuteReader ();
					}
				}
			}
		}

	}

	void GetPresentTense(string conjugation, string verbToCheck){
		string root = verbToCheck;
		string[] verbsWithPronouns = new string[7];
		verbToCheck = verbToCheck.ToLower ();
		bool longSoundException = false;
		string body = "null";
		string context = "null";
		if (conjugation == "1") {

			//Exception check
			if (verbToCheck.Length - 3 >= 0){
				if (verbToCheck == "suigh" || verbToCheck == "guigh" || verbToCheck == "nigh" || verbToCheck == "luigh") {
					longSoundException = true;
					verbToCheck = verbToCheck.Remove (verbToCheck.Length - 3, 3);
				} else if (verbToCheck.Substring (verbToCheck.Length - 3, 3) == "igh") {
					char[] temp = verbToCheck.Substring (verbToCheck.Length - 4, 1).ToCharArray ();
					if (IsFadaVowel (temp [0])) {
						verbToCheck = verbToCheck.Remove (verbToCheck.Length - 3, 3);
					}
				} 
			}
			char lastVowel = GetLastVowel (verbToCheck);

			if (longSoundException == false) {

				if (IsBroad (lastVowel)) {
					verbsWithPronouns [0] = verbToCheck + "aim"; 
					verbsWithPronouns [6] = verbToCheck + "aimid"; 
					verbToCheck = verbToCheck + "ann";

				} else if (IsSlender (lastVowel)) {
					verbsWithPronouns [0] = verbToCheck + "im"; 
					verbsWithPronouns [6] = verbToCheck + "imid";
					if (IsFadaVowel (lastVowel)) {
						verbToCheck = verbToCheck + "ann";
					} else {
						verbToCheck = verbToCheck + "eann";
					}
				 
				}

			} else {
				
				verbsWithPronouns [0] = verbToCheck + "ím"; 
				verbsWithPronouns [6] = verbToCheck + "ímid";
				verbToCheck = verbToCheck + "íonn";
			}
			verbsWithPronouns [1] = verbToCheck + " " + "tú";
			verbsWithPronouns [2] = verbToCheck + " " + "sé";
			verbsWithPronouns [3] = verbToCheck + " " + "sí";
			verbsWithPronouns [4] = verbToCheck + " " + "siad";
			verbsWithPronouns [5] = verbToCheck + " " + "sibh";

		}
		else if (conjugation == "2") {
			
			if (verbToCheck.Contains ("aigh")) {
				verbToCheck = verbToCheck.Replace ("aigh", "");
			}
			else if(verbToCheck.Contains ("igh")) {
				verbToCheck = verbToCheck.Replace ("igh", "");
			}
			else if (verbToCheck.Substring (verbToCheck.Length - 3, 2) == "ai") {
				verbToCheck = verbToCheck.Remove (verbToCheck.Length - 3, 2);
			} else if (verbToCheck.Substring (verbToCheck.Length - 2, 1) == "i") {
				verbToCheck = verbToCheck.Remove (verbToCheck.Length - 2, 1);
			}

			char lastVowel = GetLastVowel (verbToCheck);

			if (IsBroad (lastVowel)) {
				verbsWithPronouns [0] = verbToCheck + "aím"; 
				verbsWithPronouns [6] = verbToCheck + "aímid"; 
				verbToCheck = verbToCheck + "aíonn";
			
			} else if (IsSlender (lastVowel)) {
				verbsWithPronouns [0] = verbToCheck + "ím"; 
				verbsWithPronouns [6] = verbToCheck + "ímid"; 
				verbToCheck = verbToCheck + "íonn";

			}

			verbsWithPronouns [1] = verbToCheck + " " + "tú";
			verbsWithPronouns [2] = verbToCheck + " " + "sé";
			verbsWithPronouns [3] = verbToCheck + " " + "sí";
			verbsWithPronouns [4] = verbToCheck + " " + "siad";
			verbsWithPronouns [5] = verbToCheck + " " + "sibh";

		}



		string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/LanguageData.db";
		IDbConnection dbconn;
		dbconn = (IDbConnection)new SqliteConnection (conn);
		using (dbconn = (IDbConnection)new SqliteConnection (conn)) {
			dbconn.Open ();

			using (IDbCommand dbcmd = dbconn.CreateCommand ()) {

				string sqlQuery = "SELECT Body.b_text,Context.c_text FROM Body,Context,RegularVerb WHERE Body.v_id = RegularVerb.b_id AND Context.c_tense = '3' AND Body.c_id = Context.b_id AND RegularVerb.rv_lemma like '%" + root + "%' LIMIT 1";
				dbcmd.CommandText = sqlQuery;
				using (IDataReader reader = dbcmd.ExecuteReader())
				{
					while (reader.Read())
					{
						body = reader.GetString (0);
						context = reader.GetString (1);
					}
				
				}
			}


			for (int i = 0; i < verbsWithPronouns.Length; i++) {
				verbsWithPronouns [i] =  FirstLetterToUpper (verbsWithPronouns [i]);
				using (IDbCommand dbcmd = dbconn.CreateCommand ()) {

					if (verbsWithPronouns [i].Contains (" ")) {
						string[] split = verbsWithPronouns [i].Split (' ');
						string sqlQuery = "INSERT INTO QuizItem (q_id,q_lemma,q_tense,q_verb,q_person,q_body,q_context) VALUES (0,'" + FirstLetterToUpper(root) + "','present','" + split [0] + "','" + split [1] + "','" + body + "','" + context + "')";
						dbcmd.CommandText = sqlQuery;
						dbcmd.ExecuteReader ();
					} else {
						string sqlQuery = "INSERT INTO QuizItem (q_id,q_lemma,q_tense,q_verb,q_person,q_body,q_context) VALUES (0,'" + FirstLetterToUpper(root) + "','present','" + verbsWithPronouns[i] + "','" + "" + "','" + body + "','" + context + "')";
						dbcmd.CommandText = sqlQuery;
						dbcmd.ExecuteReader ();
					}
				}
			}
		}

	}
	
	void GetPastTense(string conjugation, string verbToCheck){

		string root = verbToCheck;
		string[] verbsWithPronouns = new string[7];
		verbToCheck = verbToCheck.ToLower ();
		string body = "null";
		string context = "null";
			
			verbToCheck = ConjugatePast (verbToCheck);

			verbsWithPronouns [0] = verbToCheck + " " + "mé";
			verbsWithPronouns [1] = verbToCheck + " " + "tú";
			verbsWithPronouns [2] = verbToCheck + " " + "sé";
			verbsWithPronouns [3] = verbToCheck + " " + "sí";
			verbsWithPronouns [4] = verbToCheck + " " + "siad";
			verbsWithPronouns [5] = verbToCheck + " " + "sibh";

		if (conjugation == "1") {
			char lastVowel = GetLastVowel (verbToCheck);
			if (root.Contains("igh")){
				verbToCheck = verbToCheck + " muid";
			}
			else if (IsBroad (lastVowel)) {
				verbToCheck = verbToCheck + "amar";
			} else if (IsSlender (lastVowel)) {
				verbToCheck = verbToCheck + "eamar";
			} 


		} else if (conjugation == "2") {

			if (verbToCheck.Contains ("aigh")) {
				verbToCheck = verbToCheck.Replace ("aigh", "");
			}
			else if(verbToCheck.Contains ("igh")) {
				verbToCheck = verbToCheck.Replace ("igh", "");
			}
			else if (verbToCheck.Substring (verbToCheck.Length - 3, 2) == "ai") {
				verbToCheck = verbToCheck.Remove (verbToCheck.Length - 3, 2);
			} else if (verbToCheck.Substring (verbToCheck.Length - 2, 1) == "i") {
				verbToCheck = verbToCheck.Remove (verbToCheck.Length - 2, 1);
			}

			char lastVowel = GetLastVowel (verbToCheck);

			if (IsBroad (lastVowel)) {
				verbToCheck = verbToCheck + "aíomar";
			} else if (IsSlender (lastVowel)) {
				verbToCheck = verbToCheck + "íomar";
			}
		}

		verbsWithPronouns [6] = verbToCheck;
			
		string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/LanguageData.db";
		IDbConnection dbconn;
		dbconn = (IDbConnection)new SqliteConnection (conn);
		using (dbconn = (IDbConnection)new SqliteConnection (conn)) {
			dbconn.Open ();

			using (IDbCommand dbcmd = dbconn.CreateCommand ()) {

				string sqlQuery = "SELECT Body.b_text,Context.c_text FROM Body,Context,RegularVerb WHERE Body.v_id = RegularVerb.b_id AND Context.c_tense = '2' AND Body.c_id = Context.b_id AND RegularVerb.rv_lemma like '%" + root + "%' LIMIT 1";
				dbcmd.CommandText = sqlQuery;
				using (IDataReader reader = dbcmd.ExecuteReader())
				{
					while (reader.Read())
					{
						body = reader.GetString (0);
						context = reader.GetString (1);
					}

				}
			}

			for (int i = 0; i < verbsWithPronouns.Length; i++) {
				verbsWithPronouns [i] = FirstLetterToUpper (verbsWithPronouns [i]);
				using (IDbCommand dbcmd = dbconn.CreateCommand ()) {
					if (verbsWithPronouns [i].Contains ("'")) {
						verbsWithPronouns[i] = verbsWithPronouns[i].Insert(1,"'");
					}
					if (verbsWithPronouns [i].Contains (" ")) {
						string[] split = verbsWithPronouns [i].Split (' ');
						string sqlQuery = "INSERT INTO QuizItem (q_id,q_lemma,q_tense,q_verb,q_person,q_body,q_context) VALUES (0,'" + FirstLetterToUpper(root) + "','past','" + split [0] + "','" + split [1] + "','" + body + "','" + context + "')";
						dbcmd.CommandText = sqlQuery;
						dbcmd.ExecuteReader ();
					} else {
						string sqlQuery = "INSERT INTO QuizItem (q_id,q_lemma,q_tense,q_verb,q_person,q_body,q_context) VALUES (0,'" + FirstLetterToUpper(root) + "','past','" + verbsWithPronouns[i] + "','" + "" + "','" + body + "','" + context + "')";
						dbcmd.CommandText = sqlQuery;
						dbcmd.ExecuteReader ();
					}
				}
			}
		}


	}

	string ConjugateFuture(string verbToCheck){
		
		if (verbToCheck.Contains ("aigh")) {
			verbToCheck = verbToCheck.Replace ("aigh", "");
		} else if (verbToCheck.Contains ("igh")) {
			verbToCheck = verbToCheck.Replace ("igh", "");
		} else if (verbToCheck.Substring (verbToCheck.Length - 3, 2) == "ai") {
			verbToCheck = verbToCheck.Remove (verbToCheck.Length - 3, 2);
		} else if (verbToCheck.Substring (verbToCheck.Length - 2, 1) == "i") {
			verbToCheck = verbToCheck.Remove (verbToCheck.Length - 2, 1);
		}

		return verbToCheck;
	}

	string ConjugatePast(string verbToCheck){
		char firstLetter = verbToCheck [0];
		if (firstLetter == 'f') {
			verbToCheck = verbToCheck.Insert (0, "D'");	
			verbToCheck = verbToCheck.Insert (3, "h");
			return verbToCheck;
		}
		else if (IsVowel (firstLetter)) {
			verbToCheck = verbToCheck.Insert (0, "D'");
			return verbToCheck;
		} 
		else if ((IsVowel (firstLetter) == false && firstLetter != 'f') && (firstLetter == 'b' ||
			firstLetter == 'c' ||  firstLetter == 'd' ||  firstLetter == 'g' ||  firstLetter == 'm' ||
			firstLetter == 'p' ||  firstLetter == 't')) {
			verbToCheck = verbToCheck.Insert (1, "h");
			return verbToCheck;
		}
		else return verbToCheck;
	}

	char GetLastVowel(string word){
		char letterToReturn = '?';
		for(int i = word.Length - 1; i >= 0; i--){
			if (IsVowel (word [i])) {
				return word [i];
			}
		}
		return letterToReturn;
	}

	bool IsVowel(char c){
		return (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u' || c == 'é' || c == 'í' || c == 'ú' || c == 'á' || c == 'ó');
	}

	bool IsFadaVowel(char c){
		return (c == 'é' || c == 'í' || c == 'ú' || c == 'á' || c == 'ó');
	}

	bool IsBroad(char c){
		return (c == 'a' || c == 'o' || c == 'u' || c == 'ú' || c == 'á' || c == 'ó');
	}

	bool IsSlender(char c){
		return (c == 'e' || c == 'i' || c == 'é' || c == 'í');
	}

	public string FirstLetterToUpper(string str)
	{
		if (str == null)
			return null;

		if (str.Length > 1)
			return char.ToUpper(str[0]) + str.Substring(1);

		return str.ToUpper();
	}

	public void GetFromDatabase(){

		string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/LanguageData.db";

		IDbConnection dbconn;
		dbconn = (IDbConnection)new SqliteConnection (conn);
		using (dbconn = (IDbConnection)new SqliteConnection (conn)) {
			dbconn.Open ();

			using (IDbCommand dbcmd = dbconn.CreateCommand ()) {
				string sqlQuery = "SELECT rv_lemma,con_num FROM RegularVerb";
				dbcmd.CommandText = sqlQuery;
				using (IDataReader reader = dbcmd.ExecuteReader ()) {
					while (reader.Read ()) {	
						verb = reader.GetString (0);
						conNum = reader.GetInt32(1).ToString();
						hashVerbs.Add (verb, conNum);
					}

				}
			}
		}
	}
}
