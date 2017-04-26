
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public bool isSinglePlayer;
	public bool isMultiPlayer;
	public bool isEasy;
	public bool isMedium;
	public bool isHard;
	public bool isBack;
	public bool isPuzzles = false;
	public bool isPuzzle1 = false;
	public bool isPuzzle2 = false;

	void OnMouseUp(){
		if (isSinglePlayer) {
			if (isEasy && (!isPuzzle1 || !isPuzzle2) ) {
				Application.LoadLevel (3);
				GetComponent<Renderer> ().material.color = Color.cyan;
			}
			if (isMedium && (!isPuzzle1 || !isPuzzle2) ) {
				Application.LoadLevel (4);
				GetComponent<Renderer> ().material.color = Color.cyan;
			}
			if (isHard && (!isPuzzle1 || !isPuzzle2) ) {
				Application.LoadLevel (5);
				GetComponent<Renderer> ().material.color = Color.cyan;
			}
			if (isBack) {
				Application.LoadLevel (0);
				GetComponent<Renderer> ().material.color = Color.cyan;
			} else { 
				Application.LoadLevel (1);
				GetComponent<Renderer> ().material.color = Color.cyan;
			}
		}
		if (isMultiPlayer) {
			Application.Quit ();
			GetComponent<Renderer> ().material.color = Color.cyan;
		}
		if (isPuzzles) {
			if (isPuzzle1) {
				if (isEasy && (isPuzzle1)){
					Application.LoadLevel (8);
					GetComponent<Renderer> ().material.color = Color.cyan;
				}
				if (isMedium && (isPuzzle1 )) {
					Application.LoadLevel (9);
					GetComponent<Renderer> ().material.color = Color.cyan;
				}
				if (isHard && (isPuzzle1)) {
					Application.LoadLevel (10);
					GetComponent<Renderer> ().material.color = Color.cyan;
				} else { 
					Application.LoadLevel (6);
					GetComponent<Renderer> ().material.color = Color.cyan;
				}
			}
			if (isPuzzle2) {
				if (isEasy && (isPuzzle2)) {
					Application.LoadLevel (11);
					GetComponent<Renderer> ().material.color = Color.cyan;
				}
				if (isMedium && (isPuzzle2)) {
					Application.LoadLevel (12);
					GetComponent<Renderer> ().material.color = Color.cyan;
				}
				if (isHard && (isPuzzle2)) {
					Application.LoadLevel (13);
					GetComponent<Renderer> ().material.color = Color.cyan;
				} else {
					Application.LoadLevel (7);
					GetComponent<Renderer> ().material.color = Color.cyan;
				}
			} else {
				Application.LoadLevel (2);
				GetComponent<Renderer> ().material.color = Color.cyan;
			}
		}
	}
}