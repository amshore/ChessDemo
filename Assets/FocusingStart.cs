using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class FocusingStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		bool focusModeSet = CameraDevice.Instance.SetFocusMode( 
			CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);

		if (!focusModeSet) {
			Debug.Log ("Failed to set focus mode (unsupported mode).");
		}
	}

}
