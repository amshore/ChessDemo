using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTeleporter : MonoBehaviour {

	public GameObject RollingBall;
	public GameObject BallStart;
	
	// Update is called once per frame
	void Update () {
		if (RollingBall.transform.position.y < -5) {
			RollingBall.transform.position = BallStart.transform.position;
		}
	}
}
