using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class questionManager : MonoBehaviour {


	public static string questionID, answerValue;
	List<string> questionList = new List<string>();

	public Text questionUI;

	public Toggle answer0, answer1, answer2, answer3, answer4, answer5, answer6;
	private ToggleGroup myToggleGroup;
	public Button nextButton;
	//private Toggle[] everyToggle;

	List<Toggle> everyToggle = new List<Toggle>();

	private int currentItem;
	//private int lastAnswer;

	// Use this for initialization
	void Start () {

		myToggleGroup = FindObjectOfType<ToggleGroup>();
		questionList = csvReader.questionnaireInput;
		//everyToggle = GameObject.FindObjectsOfType<Toggle>(); //optimal way for autonomy and adapts to bigger arrays of toggles, but didn't find a way to order the array
		FillList();

		questionUI.text = questionList[Random.Range(1, questionList.Count)];

		nextButton.interactable = false;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("space")) {
			
			if (currentItem < questionList.Count) {
				currentItem++;
				questionUI.text = questionList [currentItem];
			}
		}

		ActiveToggle ();
			
	}
		
	//While not very beautiful I did not find another way to order the array
	private void FillList(){
		everyToggle.Add(answer0);
		everyToggle.Add(answer1);
		everyToggle.Add(answer2);
		everyToggle.Add(answer3);
		everyToggle.Add(answer4);
		everyToggle.Add(answer5);	
		everyToggle.Add(answer6);
	}

	private void ActiveToggle() {
		/*
		for (int i = 0; i < everyToggle.Count; i++){
			if (everyToggle [i].isOn) 
				Debug.Log ("currently on is " + i);
		}*/

		if (myToggleGroup.AnyTogglesOn ()) 
			nextButton.interactable = true;
		else if (myToggleGroup.AnyTogglesOn () == false)
			nextButton.interactable = false;
		
	}

	public void OnNextButton() {

		for (int i = 0; i < everyToggle.Count; i++) {
			if (everyToggle[i].isOn)
				answerValue = i.ToString();
				everyToggle[i].isOn = false;
		}

		questionUI.text = questionList[Random.Range(1, questionList.Count)];

	}
}
