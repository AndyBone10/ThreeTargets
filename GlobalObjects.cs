using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class GlobalObjects : MonoBehaviour {

	public string playGameLevel;
	public bool easy;
	public bool mixed,present,past,future;
	public bool regular,irregular, bothVerbTypes;
	public bool conjugatorPressed, translatorPressed;
	public bool char1,char2,char3,char4,char5,char6;
	public bool selectChar1,selectChar2,selectChar3,selectChar4,selectChar5,selectChar6;
	
	// Use this for initialization
	void Start(){
		DontDestroyOnLoad (transform.gameObject);
	}

}
