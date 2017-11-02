using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class questionManager : MonoBehaviour {

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

		questionUI.text = questionList[currentItem];

		nextButton.interactable = false;
	}



	// Update is called once per frame
	void Update () {
		
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

		if (myToggleGroup.AnyTogglesOn ()) 
			nextButton.interactable = true;
		
		else if (myToggleGroup.AnyTogglesOn () == false)
			nextButton.interactable = false;
	}



	public void OnNextButton() {

		for (int i = 0; i < everyToggle.Count; i++) {
			if (everyToggle[i].isOn)
				csvWrite.answerValue = i.ToString();
				everyToggle[i].isOn = false;
		}

		csvWrite.questionID = currentItem.ToString ();

		currentItem ++;

		if (currentItem < questionList.Count)
			questionUI.text = questionList [currentItem];

		else if (currentItem >= questionList.Count) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}
}
