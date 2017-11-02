using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class EntryScreenManager : MonoBehaviour {

	public InputField nameField;
	public InputField ageField;

	public Text genderField;
	public Text handednessField;

	public Button nextButton;

	private string name;
	private string age;

	public static string nameToWrite;
	public static string ageToWrite;
	public static string genderToWrite;
	public static string handednessToWrite;

	// Use this for initialization
	void Start () {
		nextButton.interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (name != null && age != null)
			nextButton.interactable = true;
	}

	public void userName() {
		name = nameField.text;
		Debug.Log (nameField.text);
	}

	public void userAge() {
		age = ageField.text;
		Debug.Log (ageField.text);
	}

	public void OnNextButton () {
		
		nameToWrite = name;
		ageToWrite = age;

		genderToWrite = genderField.text;
		handednessToWrite = handednessField.text;

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	
	}

}
