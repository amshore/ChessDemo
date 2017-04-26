using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleMenu : MonoBehaviour {

	public bool isPuzzle1;
	public bool isPuzzle2;

	void OnMouseUp(){
		if (isPuzzle1) {
			SceneManager.LoadScene ("PuzzleGame1", LoadSceneMode.Single);
			GetComponent<Renderer> ().material.color = Color.cyan;
		}
		if (isPuzzle2) {
			SceneManager.LoadScene ("PuzzleGame2", LoadSceneMode.Single);
			GetComponent<Renderer> ().material.color = Color.cyan;
		}
	}
}
