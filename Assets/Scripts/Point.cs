using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Encapsulates a 2 points into an object, called Point
public class Point{

    private int x;
    private int y;

    public Point(int i, int j)
    {
        x = i;
        y = j;
    }

    public void setPoint(int i, int j)
    {
        x = i;
        y = j;
    }

	public float[] turnToWorld()
	{
		float[] tempLong = { (2.0f * x - 7) / 1.65f, (2.0f * y - 7) / 1.65f };
		return tempLong;
	}

    public static float[] turnToWorld(int x, int y)
    {
        float[] tempLong = { (2.0f * x - 7) / 16, (2.0f * y - 7) / 16 };
        return tempLong;
    }

    public int getX() { return x; }
    public int getY() { return y; }
    public void setX(int i) { x = i; }
    public void setY(int j) { y = j; } 
}
