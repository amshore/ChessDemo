using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.color = Color.white;
	}

	void OnMouseEnter(){
		GetComponent<Renderer>().material.color = Color.yellow;
	}

	void OnMouseExit(){
		GetComponent<Renderer>().material.color = Color.white;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
