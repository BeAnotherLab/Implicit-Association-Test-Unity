using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class csvWrite : MonoBehaviour {

	private string subjectID;
	private string age;
	private string gender;
	private string handedness;

	private string instructionsMessage;

	// Use this for initialization
	void Start () {
		
		WriteToFile ("subject ID", "age", "gender", "handedness", "condition", "question ID", "value");	

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onNextButtonPressed(){
		
		WriteToFile (subjectID, age, gender, handedness, null, null, questionManager.answerValue);

	}

	public void onParticipantDataEntered(){

		subjectID = EntryScreenManager.nameToWrite;
		age = EntryScreenManager.ageToWrite;
		gender = EntryScreenManager.genderToWrite;
		handedness = EntryScreenManager.handednessToWrite;

	}

	void WriteToFile(string a, string b, string c, string d, string f, string g, string h){

		string stringLine =  a + "," + b + "," + c + "," + d + ",";

		System.IO.StreamWriter file = new System.IO.StreamWriter("./Logs/" + subjectID + "_log.csv", true);
		file.WriteLine(stringLine);
		file.Close();	
	}
}
