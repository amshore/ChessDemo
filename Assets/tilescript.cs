using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilescript : MonoBehaviour {

	private Point location;

	public void setLoc(Point loc)
	{
		location = loc;
	}

	public Point getLoc()
	{
		return location;
	}
}
