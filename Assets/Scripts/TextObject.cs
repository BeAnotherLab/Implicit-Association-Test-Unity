using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class TextObject : MonoBehaviour {
		
	public TextMesh centralText, topRightText, topLeftText;

	public string subjectID;

	public int stimuliPerTrial;

	public string targetConcept1Name, targetConcept2Name, attributeConcept1Name, attributeConcept2Name;

	List<string> targetConcept1 = new List<string>();
	List<string> targetConcept2 = new List<string>();

	List<string> attributeConcept1 = new List<string>();
	List<string> attributeConcept2 = new List<string>();

	private bool correct = false;
	private bool onStandby = true;

	private int currentSide; //1 = left, 2 = right
	private int currentTrial;
	private int count = 0;

	private float elapsedTime;

	private string stringLine;

	private string instructionsMessage;

	private IEnumerator TrialRoutine;


	void Start () {

		Load (targetConcept1Name, targetConcept1);
		Load (targetConcept2Name, targetConcept2);
		Load (attributeConcept1Name, attributeConcept1);
		Load (attributeConcept2Name, attributeConcept2);

	//	Debug.Log(targetConcept1[3]);


		currentTrial = 1;

		Debug.Log ("starting category is " + currentTrial);

		WriteToFile ("subject ID", "current section", "current category", "current stimulus", "time", "correct");	

	}
		

	void Update(){


		if (currentTrial > 7)
			GetComponent<TextMesh> ().text = "You're done";


		if (!correct && !onStandby) {
			if(Input.GetKeyDown ("space")) {
				StartCoroutine(TrialRoutine);
			}
		}

		if (onStandby == true){
			centralText.fontSize = 24;
			centralText.offsetZ = 65;
			centralText.color = Color.white;


			if (currentTrial == 1) 
				InstructionsText (targetConcept1Name, null, targetConcept2Name, null, false);

			if (currentTrial == 2) 
				InstructionsText (attributeConcept1Name, null, attributeConcept2Name, null, false);

			if (currentTrial == 3) 
				InstructionsText (targetConcept1Name, attributeConcept1Name, targetConcept2Name, attributeConcept2Name, true);
			
			if (currentTrial == 4) 
				InstructionsText (targetConcept1Name, attributeConcept1Name, targetConcept2Name, attributeConcept2Name, true);
	
			if (currentTrial == 5)
				InstructionsText (attributeConcept2Name, null, attributeConcept1Name, null, false);

			if (currentTrial == 6)
				InstructionsText (targetConcept1Name, attributeConcept2Name, targetConcept2Name, attributeConcept1Name, true);
			
			if (currentTrial == 7)
				InstructionsText (targetConcept1Name, attributeConcept2Name, targetConcept2Name, attributeConcept1Name, true);
			
			
			if(Input.GetKeyDown ("space")) {
				StartCoroutine(TrialRoutine);
				onStandby = false;
				}
			}

		if (correct == true) {
			count = count + 1;
			StartCoroutine(TrialRoutine);
		}

		if (currentTrial == 1) {
			DoneWithSet (targetConcept1, targetConcept2, targetConcept1, targetConcept2);
			TrialRoutine = Trial (targetConcept1, targetConcept2, targetConcept1, targetConcept2, targetConcept1Name, targetConcept2Name, targetConcept1Name, targetConcept2Name);
		}

		if (currentTrial == 2){
			DoneWithSet (attributeConcept1, attributeConcept2, attributeConcept1, attributeConcept2);
			TrialRoutine = Trial (attributeConcept1, attributeConcept2, attributeConcept1, attributeConcept2, attributeConcept1Name, attributeConcept2Name, attributeConcept1Name, attributeConcept2Name);
		}

		if (currentTrial == 3){
			DoneWithSet (targetConcept1, targetConcept2, attributeConcept1, attributeConcept2);
			TrialRoutine = Trial (targetConcept1, targetConcept2, attributeConcept1, attributeConcept2, targetConcept1Name, targetConcept2Name, attributeConcept1Name, attributeConcept2Name);
		}

		if (currentTrial == 4){
			DoneWithSet (targetConcept1, targetConcept2, attributeConcept1, attributeConcept2); 
			TrialRoutine = Trial (targetConcept1, targetConcept2, attributeConcept1, attributeConcept2, targetConcept1Name, targetConcept2Name, attributeConcept1Name, attributeConcept2Name);
		}

		if (currentTrial == 5){ 
			DoneWithSet (attributeConcept2, attributeConcept1, attributeConcept2, attributeConcept1);
			TrialRoutine = Trial (attributeConcept2, attributeConcept1, attributeConcept2, attributeConcept1, attributeConcept2Name, attributeConcept1Name, attributeConcept2Name, attributeConcept1Name);
		}

		if (currentTrial == 6){
			DoneWithSet (targetConcept1, targetConcept2, attributeConcept2, attributeConcept1);
			TrialRoutine = Trial (targetConcept1, targetConcept2, attributeConcept2, attributeConcept1, targetConcept1Name, targetConcept2Name, attributeConcept2Name, attributeConcept1Name);
		}

		if (currentTrial == 7){
			DoneWithSet (targetConcept1, targetConcept2, attributeConcept2, attributeConcept1);
			TrialRoutine = Trial (targetConcept1, targetConcept2, attributeConcept2, attributeConcept1, targetConcept1Name, targetConcept2Name, attributeConcept2Name, attributeConcept1Name);
		}
	}


	/// <summary>
	/// WHENEVER A FULL SET IS FINISHED
	/// </summary>

	void DoneWithSet(List<string> primeraLista, List<string> segundaLista, List<string> terceraLista, List<string> cuartaLista){

		if (currentTrial != 4 && currentTrial != 7) {
			if (count == stimuliPerTrial) { 
				currentTrial = currentTrial + 1;
				count = 0;
				onStandby = true;
			}
		}

		else {
			if (count == stimuliPerTrial * 2) {
				currentTrial = currentTrial + 1;
				count = 0;
				onStandby = true;
			}
		}
	}


	/// <summary>
	/// EVERY TEST TRIAL RUNS HERE
	/// </summary>
	IEnumerator Trial(List<string> primeraLista, List<string> segundaLista, List<string> terceraLista, List<string> cuartaLista, 
		string primeraListaName, string segundaListaName, string terceraListaName, string cuartaListaName){

		centralText.offsetZ = 40;
		centralText.fontSize = 24;
		centralText.color = Color.white;

		string currentCategoryName = null;

		correct = false;

		currentSide = Random.Range (1, 2 + 1);

		int randomCategoryAmongGroup = Random.Range (1, 2+1);

		if (currentSide == 1) {
			if (randomCategoryAmongGroup == 1) {
				centralText.text = primeraLista [Random.Range (0, primeraLista.Count)];
				currentCategoryName = primeraListaName;
			}
			else
				centralText.text = terceraLista [Random.Range (0, terceraLista.Count)];
				currentCategoryName = terceraListaName;
		} 

		else if (currentSide == 2) {
			if (randomCategoryAmongGroup == 1) {
				centralText.text = segundaLista [Random.Range (0, segundaLista.Count)];
				currentCategoryName = segundaListaName;
			} else {
				centralText.text = cuartaLista [Random.Range (0, cuartaLista.Count)];
				currentCategoryName = cuartaListaName;
			}
		}

		float currentTime = Time.fixedTime;

		//STOPS THE COROUTINE HERE UNTIL EITHER L OR R ARE PRESSED
		while (!Input.GetKeyDown ("l") && !Input.GetKeyDown ("s")) { 
			yield return null;
		}
			
			if (currentSide == 1) {
				
				if (Input.GetKeyDown ("s")) 
					correct = true;  
				else if (Input.GetKeyDown ("l"))
					correct = false;

				elapsedTime = Time.fixedTime - currentTime;
			}


			if (currentSide == 2) {
				
				if (Input.GetKeyDown ("l")) 
					correct = true;
					
				else if (Input.GetKeyDown ("s"))
					correct = false;
			
				elapsedTime = Time.fixedTime - currentTime;
			}
			
		WriteToFile (subjectID, currentTrial.ToString(), currentCategoryName, centralText.text, elapsedTime.ToString(), correct.ToString()); 

		if (!correct) {
			centralText.fontSize = 96;
			centralText.offsetZ = 20;
			centralText.color = Color.red;
			centralText.text = "X";
		}

	}


	public void InstructionsText(string leftInput, string leftInput2, string rightInput, string rightInput2, bool isDoubleInput){

		if (!isDoubleInput) { 
			instructionsMessage =
			"PART " + currentTrial + " OF 7 \n \n __ \n \n \n \n Put a left finger on the S key for items that belong to the category " + leftInput +
			". \n \n Put a right finger on the L key for items that belong to the category " + rightInput +
			" \n \n If you make a mistake a red X will appear. Press the space key to continue." +
			" \n \n GO AS FAST AS YOU CAN WHILE BEING ACCURATE." +
			" \n \n \n \n Press the space bar when you are ready to start.";
			
			topLeftText.text = "press S for " + leftInput;
			topRightText.text = "press L for " + rightInput;
		}
		if (isDoubleInput) {
			instructionsMessage =
				"PART " + currentTrial + " OF 7 \n \n __ \n \n \n \n Put a left finger on the S key for items that belong to the category " + leftInput + leftInput2 +
			". \n \n Put a right finger on the L key for items that belong to the category " + rightInput + rightInput2 +
			" \n \n If you make a mistake a red X will appear. Press the space key to continue." +
			" \n \n GO AS FAST AS YOU CAN WHILE BEING ACCURATE." +
			" \n \n \n \n Press the space bar when you are ready to start.";

			topLeftText.text = "press S for " + leftInput + " or " + leftInput2;
			topRightText.text = "press L for " + rightInput + " or " + rightInput2;
		}

		centralText.text = instructionsMessage;

	}

	private bool Load(string fileName, List<string> arrayToTransferTo) {
		// Handle any problems that might arise when reading the text
		try
		{
			string line;
			// Create a new StreamReader, tell it which file to read and what encoding the file
			// was saved as
			StreamReader theReader = new StreamReader("./Assets/Lists/" + fileName + ".csv", Encoding.Default);
			// Immediately clean up the reader after this block of code is done.
			// You generally use the "using" statement for potentially memory-intensive objects
			// instead of relying on garbage collection.
			// (Do not confuse this with the using directive for namespace at the 
			// beginning of a class!)

			using (theReader)
			{
				line = theReader.ReadLine();
				if(line != null){
					// While there's lines left in the text file, do this:
					do {
						// Do whatever you need to do with the text line, it's a string now
						// In this example, I split it into arguments based on comma
						// deliniators, then send that array to DoStuff()
						string[] entries = line.Split(',');
						if (entries.Length > 0){
							//Debug.Log(entries[0]);
							arrayToTransferTo.Add (entries[0]);
						}

						//DoStuff(entries);
						line = theReader.ReadLine();

					}
					while (line != null);
				} 
				// Done reading, close the reader and return true to broadcast success    
				theReader.Close();
				return true;
			}
		}


		// If anything broke in the try block, we throw an exception with information
		// on what didn't work
		catch (System.Exception e) {
			Debug.Log("{0}\n" + e.Message);
			return false;
		}
	}


	/// <summary>
	/// Writes to file.
	/// </summary>

	void WriteToFile(string a, string b, string c, string d, string e, string f){

		stringLine = a + "," + b + "," + c + "," + d + "," + e + "," + f;

		System.IO.StreamWriter file = new System.IO.StreamWriter("./Logs/" + subjectID + "_log.csv", true);
		file.WriteLine(stringLine);
		file.Close();	
	}

}

		