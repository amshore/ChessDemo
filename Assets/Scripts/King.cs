using System.Collections.Generic;
using UnityEngine;

public class King : Piece {

    public King()
    {

    }

    public King(int all, Point p, Board b, PieceTypeE t) : base(all, p, b, t)
    {

    }

    //Calculates all spaces in box around piece to see if legal
    public override List<Point> canMoveList()
    {
        List<Point> retMoveList = new List<Point>();
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
            {
                Point pt = new Point(i, j);
                if (canMove(pt) != MoveTypesE.ILLEGAL)
                    retMoveList.Add(pt);
            }
        if (!hasMoved)
        {
            Point pt = new Point(loc.getX(), loc.getY() + 2);
            if (canMove(pt) != MoveTypesE.ILLEGAL)
                retMoveList.Add(pt);
            Point pt1 = new Point(loc.getX(), loc.getY() - 2);
            if (canMove(pt1) != MoveTypesE.ILLEGAL)
                retMoveList.Add(pt1);
        }

        Debug.Log("King at (" + loc.getX() + ", " + loc.getY() + ") can move to: ");
        foreach (Point p in retMoveList)
        {
            Debug.Log("(" + p.getX() + ", " + p.getY() + ")");
        }
        if (retMoveList.Count == 0)
            Debug.Log("No Possible Moves");

        return retMoveList;
    }

    /// <summary>
    /// (a) Except when castling, the king moves to any adjoining square that is not attacked by an opponent's piece.
    /// (b) Castling is a move of the king and either rook, counting as a single move of the king and executed as follows: the king is transferred from 
    /// its original square two squares toward either rook on the same rank; then that rook is transferred over the king to the square the king has just crossed.
    ///  (e) Castling is [permanently] illegal:
    ///    (i) if the king has already been moved; or
    ///    (ii) with a rook that has already been moved.
    ///  (f) Castling is prevented for the time being:
    ///    (i) if the king's original square, or the square which the king must pass over, or that which it is to occupy, is attacked by an opponent's piece; or
    ///    (ii) if there is any piece between the king and the rook with which castling is to be effected[i.e.castling may still be legal even if the rook is attacked or, 
    ///          when castling queenside, passes over an attacked square] .
    /// </summary>
    /// <param name="p">The point the pawn is trying to move to</param>
    /// <returns>The move type for the piece trying to make that move</returns>
    override public MoveTypesE canMove(Point p)
    {
        MoveTypesE mt = base.canMove(p);
        if (mt == MoveTypesE.ILLEGAL)
            return MoveTypesE.ILLEGAL;
        int dy = p.getY() - loc.getY();
        int dx = p.getX() - loc.getX();

        if ((System.Math.Abs(dx) <= 1 && System.Math.Abs(dy) <= 1))
            return mt;

        if(!hasMoved && System.Math.Abs(dy) == 2 && dx == 0)
        {
            if (gameBoard.inCheck(loc))
                return MoveTypesE.ILLEGAL;
            if(dy > 0)
            {
                for(int i = loc.getY(); i < 7; i++)
                {
                    if (gameBoard.pieceAt(i, loc.getY()) != null)
                        return MoveTypesE.ILLEGAL;
                }
                GameObject rookMaybe = gameBoard.pieceAt(loc.getX(), 7);
                if (rookMaybe == null || ((Piece)rookMaybe.GetComponent("Piece")).getHasMoved())
                    return MoveTypesE.ILLEGAL;
                if (!gameBoard.inCheck(gameObject, rookMaybe, loc.getX(), loc.getY() + 1) && !gameBoard.inCheck(gameObject, rookMaybe, loc.getX(), loc.getY() + 2))
                    return MoveTypesE.ILLEGAL;
            }
            else
            {
                for (int i = loc.getY(); i > 0; i--)
                {
                    if (gameBoard.pieceAt(i, loc.getY()) != null)
                        return MoveTypesE.ILLEGAL;
                }
                GameObject rookMaybe = gameBoard.pieceAt(loc.getX(), 0);
                if (rookMaybe == null || ((Piece)rookMaybe.GetComponent("Piece")).getHasMoved())
                    return MoveTypesE.ILLEGAL;
                if (!gameBoard.inCheck(gameObject, rookMaybe, loc.getX(), loc.getY() - 1) && !gameBoard.inCheck(gameObject, rookMaybe, loc.getX(), loc.getY() - 2))
                    return MoveTypesE.ILLEGAL;
            }
            return MoveTypesE.CASTLE;
        }

        return MoveTypesE.ILLEGAL;
    }

    /// <summary>
    /// Similar tryToMove as the default, but if castling, moves the correct king to the correct position as well.
    /// </summary>
    /// <param name="p"></param>
    public override void tryToMove(Point p)
    {
        MoveTypesE mt = canMove(p);
        if (mt != MoveTypesE.ILLEGAL)
        {
            if (mt == MoveTypesE.CASTLE)
            {
                Point tmploc = loc;
                gameBoard.Move(loc, p);
                if (loc.getX() > p.getX())
                {
                    gameBoard.Move(new Point(p.getX(), 0), new Point(p.getX() + 1, p.getY()));
                }
                else
                {
                    gameBoard.Move(new Point(p.getX(), 7), new Point(p.getX() - 1, p.getY()));
                }
            }
            else
            {
                gameBoard.Move(loc, p);
            }
            hasMoved = true;
        }
    }

}
