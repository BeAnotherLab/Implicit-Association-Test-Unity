using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

/// <summary>
/// /BUG, SPACE GOES TO THE FOLLOWING STIMULUS BUT SHOULDN'T
/// </summary>
public class TextObject : MonoBehaviour {
		
	public TextMesh centralText, topRightText, topLeftText;
	public Renderer square;
	public string attributeConcept1Name, attributeConcept2Name; 	
	public string targetConcept1, targetConcept2;//image folders

	public string subjectID;
	public int stimuliPerTrial;

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

	private Texture2D conceptTexture;

	private IEnumerator TrialRoutine;


	void Start () {
		
		Load (attributeConcept1Name, attributeConcept1);
		Load (attributeConcept2Name, attributeConcept2);

	//	Debug.Log(targetConcept1[3]);

		currentTrial = 1;

		Debug.Log ("starting category is " + currentTrial);

		WriteToFile ("subject ID", "current section", "current category", "current stimulus", "time", "correct");	

		square.enabled = false;
	}
		

	void Update(){


		if (currentTrial > 7)
			GetComponent<TextMesh> ().text = "You're done";


		if (!correct && !onStandby) {
			if(Input.GetKeyDown ("space")) {
				StartCoroutine(TrialRoutine);
			}
		}

		if (onStandby == true) {
			centralText.fontSize = 24;
			centralText.offsetZ = 65;
			centralText.color = Color.white;
			square.enabled = false;


			if (currentTrial == 1) 
				InstructionsText (targetConcept1, null, targetConcept2, null, false);

			if (currentTrial == 2) 
				InstructionsText (attributeConcept1Name, null, attributeConcept2Name, null, false);

			if (currentTrial == 3) 
				InstructionsText (targetConcept1, attributeConcept1Name, targetConcept2, attributeConcept2Name, true);
			
			if (currentTrial == 4) 
				InstructionsText (targetConcept1, attributeConcept1Name, targetConcept2, attributeConcept2Name, true);
	
			if (currentTrial == 5)
				InstructionsText (attributeConcept2Name, null, attributeConcept1Name, null, false);

			if (currentTrial == 6)
				InstructionsText (targetConcept1, attributeConcept2Name, targetConcept2, attributeConcept1Name, true);
			
			if (currentTrial == 7)
				InstructionsText (targetConcept1, attributeConcept2Name, targetConcept2, attributeConcept1Name, true);
			
			
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
			DoneWithSet();
			TrialRoutine = Trial (null, null, null, null);
		}

		if (currentTrial == 2){
			DoneWithSet ();
			TrialRoutine = Trial (attributeConcept1, attributeConcept2, attributeConcept1Name, attributeConcept2Name);
		}

		if (currentTrial == 3){
			DoneWithSet ();
			TrialRoutine = Trial (attributeConcept1, attributeConcept2, attributeConcept1Name, attributeConcept2Name);
		}

		if (currentTrial == 4){
			DoneWithSet (); 
			TrialRoutine = Trial (attributeConcept1, attributeConcept2, attributeConcept1Name, attributeConcept2Name);
		}

		if (currentTrial == 5){ 
			DoneWithSet ();
			TrialRoutine = Trial (attributeConcept2, attributeConcept1, attributeConcept2Name, attributeConcept1Name);
		}

		if (currentTrial == 6){
			DoneWithSet ();
			TrialRoutine = Trial (attributeConcept2, attributeConcept1, attributeConcept2Name, attributeConcept1Name);
		}

		if (currentTrial == 7){
			DoneWithSet ();
			TrialRoutine = Trial (attributeConcept2, attributeConcept1, attributeConcept2Name, attributeConcept1Name);
		}
	}


	/// <summary>
	/// WHENEVER A FULL SET IS FINISHED
	/// </summary>

	void DoneWithSet(){

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
	IEnumerator Trial(List<string> primeraLista, List<string> segundaLista, string primeraListaName, string segundaListaName){

		centralText.text = null;
		centralText.offsetZ = 40;
		centralText.fontSize = 24;
		centralText.color = Color.white;

		string currentCategoryName = null;

		correct = false;

		currentSide = Random.Range (1, 2 + 1);

		int randomCategoryAmongGroup = 1;

		int stimuliRandomizer = 0;


		if (currentTrial == 1) {

			if (currentSide == 1) {
				stimuliRandomizer = Random.Range (1, Resources.LoadAll ("Images/" + targetConcept1 + "/").Length);
				conceptTexture = Resources.Load ("Images/" + targetConcept1 + "/image" + stimuliRandomizer) as Texture2D;
				currentCategoryName = targetConcept1;
			} 
			else {
				stimuliRandomizer = Random.Range(1, Resources.LoadAll ("Images/" + targetConcept2 + "/").Length);
				conceptTexture = Resources.Load ("Images/" + targetConcept2 + "/image" + stimuliRandomizer) as Texture2D;
				currentCategoryName = targetConcept2;
			}

			square.enabled = true;
			square.material.mainTexture = conceptTexture;
		}

		if (currentTrial == 2) {

			if (currentSide == 1) {
				stimuliRandomizer = Random.Range (0, primeraLista.Count);
				centralText.text = primeraLista [stimuliRandomizer];
				currentCategoryName = primeraListaName;

			} else {
				stimuliRandomizer = Random.Range (0, segundaLista.Count);
				centralText.text = segundaLista [stimuliRandomizer];
				currentCategoryName = segundaListaName;
			}
		}

		if (currentTrial == 3 | currentTrial == 4) {
			if (currentSide == 1) {
				if (randomCategoryAmongGroup == 1) {
					stimuliRandomizer = Random.Range (1, Resources.LoadAll ("Images/" + targetConcept1 + "/").Length);
					conceptTexture = Resources.Load ("Images/" + targetConcept1 + "/image" + stimuliRandomizer) as Texture2D;
					square.enabled = true;
					square.material.mainTexture = conceptTexture;
					currentCategoryName = targetConcept1;
				}
				if (randomCategoryAmongGroup == 2) {
					stimuliRandomizer = Random.Range (0, primeraLista.Count);
					centralText.text = primeraLista [stimuliRandomizer];
					currentCategoryName = primeraListaName;
				}

			} 

			else if (currentSide == 2) {
				
				if (randomCategoryAmongGroup == 1) {
					stimuliRandomizer = Random.Range(1, Resources.LoadAll ("Images/" + targetConcept2 + "/").Length);
					conceptTexture = Resources.Load ("Images/" + targetConcept2 + "/image" + stimuliRandomizer) as Texture2D;
					square.enabled = true;
					square.material.mainTexture = conceptTexture;
					currentCategoryName = targetConcept2;
				}
				if (randomCategoryAmongGroup == 2) {
					stimuliRandomizer = Random.Range (0, segundaLista.Count);
					centralText.text = segundaLista [stimuliRandomizer];
					currentCategoryName = segundaListaName;
				}
			}
		}

		if (currentTrial == 5) {
			if (currentSide == 1) {
				stimuliRandomizer = Random.Range (0, segundaLista.Count);
				centralText.text = segundaLista [stimuliRandomizer];
				currentCategoryName = segundaListaName;
			} 

			else {
				stimuliRandomizer = Random.Range (0, primeraLista.Count);
				centralText.text = primeraLista [stimuliRandomizer];
				currentCategoryName = primeraListaName;
			}
		}


		if (currentTrial == 6 | currentTrial == 8) {
			if (currentSide == 1) {
				if (randomCategoryAmongGroup == 1) {
					stimuliRandomizer = Random.Range (1, Resources.LoadAll ("Images/" + targetConcept1 + "/").Length);
					conceptTexture = Resources.Load ("Images/" + targetConcept1 + "/image" + stimuliRandomizer) as Texture2D;
					square.enabled = true;
					square.material.mainTexture = conceptTexture;
					currentCategoryName = targetConcept1;
				}
				if (randomCategoryAmongGroup == 2) {
					stimuliRandomizer = Random.Range (0, segundaLista.Count);
					centralText.text = segundaLista [stimuliRandomizer];
					currentCategoryName = segundaListaName;
				}

			} 

			else if (currentSide == 2) {

				if (randomCategoryAmongGroup == 1) {
					stimuliRandomizer = Random.Range(1, Resources.LoadAll ("Images/" + targetConcept2 + "/").Length);
					conceptTexture = Resources.Load ("Images/" + targetConcept2 + "/image" + stimuliRandomizer) as Texture2D;
					square.enabled = true;
					square.material.mainTexture = conceptTexture;
					currentCategoryName = targetConcept2;
				}
				if (randomCategoryAmongGroup == 2) {
					stimuliRandomizer = Random.Range (0, primeraLista.Count);
					centralText.text = primeraLista [stimuliRandomizer];
					currentCategoryName = primeraListaName;
				}
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
			
		WriteToFile (subjectID, currentTrial.ToString(), currentCategoryName, stimuliRandomizer.ToString(), elapsedTime.ToString(), correct.ToString()); 

		square.enabled = false;

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

			StreamReader csvReader = new StreamReader("./Assets/Resources/Lists/" + fileName + ".csv", Encoding.Default);

			using (csvReader)
			{
				line = csvReader.ReadLine();
				if(line != null){
					
					// While there's lines left in the text file, do this:
					do {
						string[] entries = line.Split(',');

						if (entries.Length > 0){
							arrayToTransferTo.Add (entries[0]);
						}

						//DoStuff(entries);
						line = csvReader.ReadLine();
					}

					while (line != null);
				} 
				// Done reading, close the reader and return true to broadcast success    
				csvReader.Close();
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