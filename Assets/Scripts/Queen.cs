using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Instance of a queen object.
/// Can generate a list of valid moves following the
/// queen movement rules.
/// </summary>
public class Queen : Piece {

    /// <summary>
    /// Default constructor.  Should never be used.
    /// </summary>
    public Queen()
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
    public Queen(int all, Point p, Board b, PieceTypeE t) : base(all, p, b, t)
    {
    }

    /// <summary>
    /// Create list of valid moves moving along diagonal, file, rank until finding illegal move
    /// </summary>
    /// <returns>List of valid points the queen can move to</returns>
    override public List<Point> canMoveList()
    {
        List<Point> retMoveList = new List<Point>();
        bool[] flagArray = { true, true, true, true, true, true, true, true};
        for (int i = 1; flagArray[0] && i < 8; i++)
        {
            Point p = new Point(loc.getX() + i, loc.getY() + i);
            if (canMove(p) == MoveTypesE.ILLEGAL)
                flagArray[0] = false;
            else
                retMoveList.Add(p);
        }
        for (int i = 1; flagArray[1] && i < 8; i++)
        {
            Point p = new Point(loc.getX() - i, loc.getY() + i);
            if (canMove(p) == MoveTypesE.ILLEGAL)
                flagArray[1] = false;
            else
                retMoveList.Add(p);
        }
        for (int i = 1; flagArray[2] && i < 8; i++)
        {
            Point p = new Point(loc.getX() + i, loc.getY() - i);
            if (canMove(p) == MoveTypesE.ILLEGAL)
                flagArray[2] = false;
            else
                retMoveList.Add(p);
        }
        for (int i = 1; flagArray[3] && i < 8; i++)
        {
            Point p = new Point(loc.getX() - i, loc.getY() - i);
            if (canMove(p) == MoveTypesE.ILLEGAL)
                flagArray[3] = false;
            else
                retMoveList.Add(p);
        }
        for (int i = 1; flagArray[4] && i < 8; i++)
        {
            Point p = new Point(loc.getX() + i, loc.getY());
            if (canMove(p) == MoveTypesE.ILLEGAL)
                flagArray[0] = false;
            else
                retMoveList.Add(p);
        }
        for (int i = 1; flagArray[5] && i < 8; i++)
        {
            Point p = new Point(loc.getX(), loc.getY() + i);
            if (canMove(p) == MoveTypesE.ILLEGAL)
                flagArray[1] = false;
            else
                retMoveList.Add(p);
        }
        for (int i = 1; flagArray[6] && i < 8; i++)
        {
            Point p = new Point(loc.getX() - i, loc.getY());
            if (canMove(p) == MoveTypesE.ILLEGAL)
                flagArray[2] = false;
            else
                retMoveList.Add(p);
        }
        for (int i = 1; flagArray[7] && i < 8; i++)
        {
            Point p = new Point(loc.getX(), loc.getY() - i);
            if (canMove(p) == MoveTypesE.ILLEGAL)
                flagArray[3] = false;
            else
                retMoveList.Add(p);
        }

        Debug.Log("Queen at (" + loc.getX() + ", " + loc.getY() + ") can move to: ");
        foreach (Point p in retMoveList)
        {
            Debug.Log("(" + p.getX() + ", " + p.getY() + ")");
        }
        if (retMoveList.Count == 0)
            Debug.Log("No Possible Moves");

        return retMoveList;
    }

    /// <summary>
    /// The queen moves to any square (except as limited by Article 4.2) [No leapfrogging] on the file, rank, or diagonals on which it stands.
    /// </summary>
    /// <param name="p">The point the queen is trying to move to</param>
    /// <returns>The move type for the piece trying to make that move</returns>
    override public MoveTypesE canMove(Point p)
    {
        MoveTypesE mt = base.canMove(p);
        if (mt == MoveTypesE.ILLEGAL)
            return MoveTypesE.ILLEGAL;
        int dy = p.getY() - loc.getY();
        int dx = p.getX() - loc.getX();

        if (dy == 0)
        {
            int signFactor = (dx * System.Math.Abs(dx) > 0) ? 1 : -1;
            for (int i = 1; i < System.Math.Abs(dx); i++)
            {
                if (gameBoard.pieceAt(loc.getX() + signFactor * i, loc.getY()) != null)
                    return MoveTypesE.ILLEGAL;
            }
        }
        else if (dx == 0)
        {
            int signFactor = (dy * System.Math.Abs(dy) > 0) ? 1 : -1;
            for (int i = 1; i < System.Math.Abs(dy); i++)
            {
                if (gameBoard.pieceAt(loc.getX(), loc.getY() + signFactor * i) != null)
                    return MoveTypesE.ILLEGAL;
            }
        }
        if (System.Math.Abs(dy) == System.Math.Abs(dx))
        {
            int signFactorX = (dx * System.Math.Abs(dx) > 0) ? 1 : -1;
            int signFactorY = (dy * System.Math.Abs(dy) > 0) ? 1 : -1;
            for (int i = 1; i < System.Math.Abs(dx); i++)
            {
                if (gameBoard.pieceAt(loc.getX() + signFactorX * i, loc.getY() + signFactorY * i) != null)
                    return MoveTypesE.ILLEGAL;
            }
        }
        return mt;
    }
}
