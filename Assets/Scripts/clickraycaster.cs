using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickraycaster : MonoBehaviour {

	List<Point> list;
	Board gameboard;

	void Start()
	{
		gameboard = Board.Instance;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast (ray,out hit, 100.0f))
            {
				if(hit.collider.gameObject.tag == "Piece")
				{
					Debug.Log ("Clicked a Piece!");
					if(gameboard.currentPiece != null)
					{
						if(((Piece)hit.collider.gameObject.GetComponent("Piece")).getAllegiance() == gameboard.getAllegiance())
						{
							if(hit.collider.gameObject == gameboard.currentPiece)
							{
								// do nothing because pointless
							}
							else
							{
								//select new piece
								gameboard.currentPiece = (Piece)hit.collider.gameObject.GetComponent("Piece");
								list = ((Piece)hit.collider.gameObject.GetComponent("Piece")).canMoveList();
								gameboard.makeTileField (list);
							}
						}

						else if(((Piece)hit.collider.gameObject.GetComponent("Piece")).getAllegiance() != gameboard.getAllegiance())
						{
							Point enemy = (Point)((Piece)hit.collider.gameObject.GetComponent("Piece")).getLoc();
							foreach(Point p in list)
							{
								if (p == enemy) 
								{
									((Piece)gameboard.currentPiece).tryToMove (enemy);
								}
							}
							//else enemy is out of reach, so do nothing
						}

					}
					else if(gameboard.currentPiece == null)
					{
						if(hit.collider.gameObject != gameboard.currentPiece)
						{
							//you've seleced a piece
							gameboard.currentPiece = (Piece)hit.collider.gameObject.GetComponent("Piece");
							list = ((Piece)hit.collider.gameObject.GetComponent("Piece")).canMoveList();
							gameboard.makeTileField (list);
						}
						//else do nothing
					}
				}
				else if(hit.collider.gameObject.tag == "Tile")
				{
					((Piece)gameboard.currentPiece).tryToMove (((tilescript)hit.collider.gameObject.GetComponent("tilescript")).getLoc());
				}
				else if(hit.collider.gameObject.tag == "Board")
				{
					//do nothing because pointless
				}
				else
				{
					//player clicked on empty space, so do nothing because pointless
				}
                Debug.Log("You selected the " + hit.collider.gameObject.name);
            }
        }
	}
}
