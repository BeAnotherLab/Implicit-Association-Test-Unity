using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csvWrite : MonoBehaviour {
	public string subjectID;

	private string stringLine;
	private string instructionsMessage;


	// Use this for initialization
	void Start () {
		
		WriteToFile ("subject ID", "condition", "question ID", "value");	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void WriteToFile(string a, string b, string c, string d){

		stringLine = a + "," + b + "," + c + "," + d + ",";

		System.IO.StreamWriter file = new System.IO.StreamWriter("./Logs/" + subjectID + "_log.csv", true);
		file.WriteLine(stringLine);
		file.Close();	
	}
}
