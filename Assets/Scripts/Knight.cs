using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Instance of a knight object.
/// Can generate a list of valid moves following the
/// knight movement rules.
/// </summary>
public class Knight : Piece {

    /// <summary>
    /// Default constructor.  Should never be used.
    /// </summary>
    public Knight()
    {

    }

    /// <summary>
    /// Constructor that should be used.
    /// </summary>
    /// <param name="all">Bit representing the color of the Piece.
    /// White = 0, Black = 1;</param>
    /// <param name="p">The location of the piece on the board</param>
    /// <param name="b">A reference to the game board</param>
    /// <param name="t">The type of piece being created.</param>
    public Knight(int all, Point p, Board b, PieceTypeE t) : base(all, p, b, t)
    {

    }

    /// <summary>
    /// Create list of valid moves 
    /// </summary>
    /// <returns>List of valid points the knight can move to</returns>
    override public List<Point> canMoveList()
    {
        List<Point> retMoveList = new List<Point>();
        Point p1 = new Point(loc.getX() + 1, loc.getY() + 2);
        Point p2 = new Point(loc.getX() + 2, loc.getY() + 1);
        Point p3 = new Point(loc.getX() + 2, loc.getY() - 1);
        Point p4 = new Point(loc.getX() + 1, loc.getY() - 2);
        Point p5 = new Point(loc.getX() - 1, loc.getY() - 2);
        Point p6 = new Point(loc.getX() - 2, loc.getY() - 1);
        Point p7 = new Point(loc.getX() - 2, loc.getY() + 1);
        Point p8 = new Point(loc.getX() - 1, loc.getY() + 2);
        if (canMove(p1) != MoveTypesE.ILLEGAL)
            retMoveList.Add(p1);
        if (canMove(p2) != MoveTypesE.ILLEGAL)
            retMoveList.Add(p2);
        if (canMove(p3) != MoveTypesE.ILLEGAL)
            retMoveList.Add(p3);
        if (canMove(p4) != MoveTypesE.ILLEGAL)
            retMoveList.Add(p4);
        if (canMove(p5) != MoveTypesE.ILLEGAL)
            retMoveList.Add(p5);
        if (canMove(p6) != MoveTypesE.ILLEGAL)
            retMoveList.Add(p6);
        if (canMove(p7) != MoveTypesE.ILLEGAL)
            retMoveList.Add(p7);
        if (canMove(p8) != MoveTypesE.ILLEGAL)
            retMoveList.Add(p8);

        Debug.Log("Knight at (" + loc.getX() + ", " + loc.getY() + ") can move to: ");
        foreach (Point p in retMoveList)
        {
            Debug.Log("(" + p.getX() + ", " + p.getY() + ")");
        }
        if (retMoveList.Count == 0)
            Debug.Log("No Possible Moves");

        return retMoveList;
    }

    /// <summary>
    /// The knight's move is composed of two different steps; first, it makes one step of one single square along its rank or file, and then, 
    /// still moving away from the square of departure, one step of one single square on a diagonal. It does not matter if the square of the 
    /// first step is occupied.
    /// </summary>
    /// <param name="p">The point the knight is trying to move to</param>
    /// <returns>The move type for the piece trying to make that move</returns>
    override public MoveTypesE canMove(Point p)
    {
        MoveTypesE mt = base.canMove(p);
        if (mt == MoveTypesE.ILLEGAL)
            return MoveTypesE.ILLEGAL;
        int dy = p.getY() - loc.getY();
        int dx = p.getX() - loc.getX();

        if ((System.Math.Abs(dy) == 2 && System.Math.Abs(dx) == 1) || (System.Math.Abs(dy) == 1 && System.Math.Abs(dx) == 2))
            return mt;
        return MoveTypesE.ILLEGAL;
    }
}
