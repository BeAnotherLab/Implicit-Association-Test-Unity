using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextObject : MonoBehaviour {


	public TextMesh central;

	List<string> targetConcept1 = new List<string>();
	List<string> targetConcept2 = new List<string>();

	List<string> attributeConcept1 = new List<string>();
	List<string> attributeConcept2 = new List<string>();

	private bool correct = false;
	private bool onStandby = true;

	private int currentCategory; //1 = left, 2 = right
	private int currentTrial;
	private int count = 0;

	private float elapsedTime;

	//private TextMesh instrucciones;

	private IEnumerator TrialRoutine;


	void Start () {
		targetConcept1.Add ("Carlos");
		targetConcept1.Add ("Joaquin");
		targetConcept1.Add ("Ignacio");
		targetConcept1.Add ("Pedro");

		targetConcept2.Add ("Norma");
		targetConcept2.Add ("Hilda");
		targetConcept2.Add ("Natasha");
		targetConcept2.Add ("Carolina");

		attributeConcept1.Add ("Bien");
		attributeConcept1.Add ("Agradable");
		attributeConcept1.Add ("Entrañable");
		attributeConcept1.Add ("Amable");

		attributeConcept2.Add ("Mal");
		attributeConcept2.Add ("Desargradable");
		attributeConcept2.Add ("Odioso");
		attributeConcept2.Add ("Molesto");

		currentTrial = 1;
		Debug.Log ("starting category is " + currentTrial);

		//instrucciones.
		//TextMesh instrucciones = gameObject.AddComponent("TextMesh") as TextMesh.text("Put a left finger on the S key for items that belong to the category Women \n Put a right finger on the L key for items that belong to the category Men");
	}
		

	void Update(){

		if (currentTrial > 7)
			GetComponent<TextMesh> ().text = "You're done";
		
		if (onStandby == true){
		//	GetComponent<TextMesh> ().text = "Press the space key to continue";
			central.text = "Press the space key to continue";
			if(Input.GetKeyDown ("space")) {
				StartCoroutine(TrialRoutine);
				onStandby = false;
				}
			}

		if (correct == true) {
			count = count + 1;
			StartCoroutine(TrialRoutine);
		}

		if (currentTrial == 1){
			DoneWithSet (targetConcept1, targetConcept2, targetConcept1, targetConcept2);
			TrialRoutine = Trial (targetConcept1, targetConcept2, targetConcept1, targetConcept2);
		}

		if (currentTrial == 2){
			DoneWithSet (attributeConcept1, attributeConcept2, attributeConcept1, attributeConcept2);
			TrialRoutine = Trial (attributeConcept1, attributeConcept2, attributeConcept1, attributeConcept2);
		}

		if (currentTrial == 3){
			DoneWithSet (targetConcept1, targetConcept2, attributeConcept1, attributeConcept2);
			TrialRoutine = Trial (targetConcept1, targetConcept2, attributeConcept1, attributeConcept2);
		}

		if (currentTrial == 4){
			DoneWithSet (targetConcept1, targetConcept2, attributeConcept1, attributeConcept2);
			TrialRoutine = Trial (targetConcept1, targetConcept2, attributeConcept1, attributeConcept2);
		}

		if (currentTrial == 5){
			DoneWithSet (attributeConcept2, attributeConcept1, attributeConcept2, attributeConcept1);
			TrialRoutine = Trial (attributeConcept2, attributeConcept1, attributeConcept2, attributeConcept1);
		}

		if (currentTrial == 6){
			DoneWithSet (targetConcept1, targetConcept2, attributeConcept2, attributeConcept1);
			TrialRoutine = Trial (targetConcept1, targetConcept2, attributeConcept2, attributeConcept1);
		}

		if (currentTrial == 7){
			DoneWithSet (targetConcept1, targetConcept2, attributeConcept2, attributeConcept1);
			TrialRoutine = Trial (targetConcept1, targetConcept2, attributeConcept2, attributeConcept1);
		}


	}


	/// <summary>
	/// /
	/// </summary>

	void DoneWithSet(List<string> primeraLista, List<string> segundaLista, List<string> terceraLista, List<string> cuartaLista){

		if (currentTrial != 4 && currentTrial != 7) {
			if (count == 3) {
				currentTrial = currentTrial + 1;
				count = 0;
				onStandby = true;
				Debug.Log ("changed order to " + currentTrial);
			}
		}

		else {
			if (count == 6) {
				currentTrial = currentTrial + 1;
				count = 0;
				onStandby = true;
				Debug.Log ("changed order to " + currentTrial);
			}
		}


	}


	/// <summary>
	/// /
	/// </summary>
	IEnumerator Trial(List<string> primeraLista, List<string> segundaLista, List<string> terceraLista, List<string> cuartaLista){
		
		correct = false;

		currentCategory = Random.Range (1, 2 + 1);
		int valueRandomizer = Random.Range (0, 3+1);
		int randomListAmongCategory = Random.Range (1, 2+1);

		if (currentCategory == 1) {
			if (randomListAmongCategory == 1)
				GetComponent<TextMesh> ().text = primeraLista [valueRandomizer];
			else if (randomListAmongCategory == 2)
				GetComponent<TextMesh> ().text = terceraLista [valueRandomizer];
		} 

		else if (currentCategory == 2) {
			if (randomListAmongCategory == 1)
				GetComponent<TextMesh> ().text = segundaLista [valueRandomizer];
			else if (randomListAmongCategory == 2)
				GetComponent<TextMesh> ().text = cuartaLista [valueRandomizer];
		}
		float currentTime = Time.fixedTime;
		

		while (!Input.GetKeyDown ("l") && !Input.GetKeyDown ("s")) {
			yield return null;
		}
			
			
			if (currentCategory == 1) {
				
				if (Input.GetKeyDown ("l")) {
					elapsedTime = Time.fixedTime - currentTime;
					correct = true;  
				} else if (Input.GetKeyDown ("s"))
					correct = false;
			}


			if (currentCategory == 2) {
				
				if (Input.GetKeyDown ("s")) {
					correct = true;
					elapsedTime = Time.fixedTime - currentTime;
				} else if (Input.GetKeyDown ("l"))
					correct = false;
			}

		if (correct)
			Debug.Log ("Elapsed time for last stimulus " + elapsedTime);
		else
			Debug.Log ("Wrong answer");
		

		onStandby = !correct;
	}
		


}
