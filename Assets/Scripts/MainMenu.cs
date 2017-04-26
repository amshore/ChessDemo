using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public bool isSinglePlayer;
	public bool isMultiPlayer;
	public bool isEasy;
	public bool isMedium;
	public bool isHard;
	public bool isBack;
	public bool isPuzzle;

	void OnMouseUp(){
		if (isSinglePlayer) {
			SceneManager.LoadScene("mainMenuAISelection", LoadSceneMode.Single);
			GetComponent<Renderer> ().material.color = Color.cyan;
		}
		if (isMultiPlayer) {
			Application.Quit ();
			GetComponent<Renderer> ().material.color = Color.cyan;
		}
		if (isEasy) {
			SceneManager.LoadScene("mainGameAIEasy", LoadSceneMode.Single);
			GetComponent<Renderer> ().material.color = Color.cyan;
		}
		if (isMedium) {
			SceneManager.LoadScene("mainGameAIMedium", LoadSceneMode.Single);
			GetComponent<Renderer> ().material.color = Color.cyan;
		}
		if (isHard) {
			SceneManager.LoadScene("mainGameAIMedium", LoadSceneMode.Single);
			GetComponent<Renderer> ().material.color = Color.cyan;
		}
		if (isBack) {
			SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
			GetComponent<Renderer> ().material.color = Color.cyan;
		}
		if (isPuzzle) {
			SceneManager.LoadScene ("puzzlesSelectedScene", LoadSceneMode.Single);
			GetComponent<Renderer> ().material.color = Color.cyan;
		}
	}
}
