using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

	int score = 0; 
	public GameObject rocks; 
	// Use this for initialization
	void Start () {
		InvokeRepeating ("CreateRocks", 1.0f, 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUI.color = Color.black;
		GUILayout.Label ("Score " + score.ToString ());
	}
	void CreateRocks(){

		Instantiate (rocks);
		score++; // score = score+1;
	}
}
