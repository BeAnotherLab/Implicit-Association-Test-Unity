using UnityEngine;
using System.Collections;

public class images : MonoBehaviour {

	public Renderer square;
	//public Texture aTexture;
	private Texture2D myTexture = new Texture2D (2, 2);

	// Use this for initialization
	void Start () {
		//myTexture	= 	Resources.Load(	"Images/firma") as Texture;
		myTexture = Resources.Load(	"Images/firma") as Texture2D;
		square.material.color = Color.black;
	}

	void OnGUI() {
		//GUI.DrawTexture(new Rect(60, 60, 200, 200), myTexture, ScaleMode.ScaleToFit, true, 1.0F);
	}


	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("1")) {

		}

		square.material.mainTexture = myTexture;

	}
}
