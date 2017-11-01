using UnityEngine;
using System.Collections;

public class ToggleExample : MonoBehaviour {
	public Texture aTexture;
	private bool toggleTxt = false;
	private bool toggleImg = false;


	void OnGUI() {
		if (!aTexture) {
			Debug.LogError("Please assign a texture in the inspector.");
			return;
		}

		toggleTxt = GUI.Toggle(new Rect(10, 10, 100, 30), toggleTxt, "A Toggle text");
		toggleImg = GUI.Toggle(new Rect(10, 50, 50, 50), toggleImg, aTexture);
	}
}