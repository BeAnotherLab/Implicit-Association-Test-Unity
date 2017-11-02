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

	private string participantName;
	private string age;

	// Use this for initialization
	void Start () {
		nextButton.interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (participantName != null && age != null)
			nextButton.interactable = true;
	}

	public void userName() {
		participantName = nameField.text;
	}

	public void userAge() {
		age = ageField.text;
	}

	public void OnNextButton () {
		
		csvWrite.subjectID = participantName;
		csvWrite.age = age;

		csvWrite.gender = genderField.text;
		csvWrite.handedness = handednessField.text;

		while (Time.fixedDeltaTime >= 3) {
		}

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	
	}

}
