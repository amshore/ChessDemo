using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public bool isSinglePlayer;
	public bool isMultiPlayer;
	public bool isEasy;
	public bool isMedium;
	public bool isHard;

	void OnMouseUp(){
		if (isSinglePlayer) {
			Application.LoadLevel (1);
			GetComponent<Renderer> ().material.color = Color.cyan;
		}
		if (isMultiPlayer) {
			Application.Quit ();
			GetComponent<Renderer> ().material.color = Color.cyan;
		}
		if (isEasy) {
			Application.LoadLevel (2);
			GetComponent<Renderer> ().material.color = Color.cyan;
		}
		if (isMedium) {
			Application.LoadLevel (3);
			GetComponent<Renderer> ().material.color = Color.cyan;
		}
		if (isHard) {
			Application.LoadLevel (4);
			GetComponent<Renderer> ().material.color = Color.cyan;
		}
	}
}
