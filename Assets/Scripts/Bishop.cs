using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece {

    public Bishop()
    {

    }

    public Bishop(int all, Point p, Board b, PieceTypeE t) : base(all, p, b, t)
    {

    }

    public override List<Vector3> canMoveEval()
    {
        List<Vector3> scores = new List<Vector3>();
        List<Point> pts = canMoveList();
        
        foreach (Point point in pts)
        {
            int basenum = pts.Count * (int)ScoreWeightsE.MOBILITY + (int)PieceWeightsE.BISHOPWEIGHT;
            basenum = basenum + (point.getX()) * (7 - point.getX()) * (int)ScoreWeightsE.BISHOPX + (point.getX()) * (7 - point.getX()) * (int)ScoreWeightsE.BISHOPY;
            if (gameBoard.pieceAt(point) != null)
            {
                switch ((((Piece)gameBoard.pieceAt(point).GetComponent("Piece")).getType()))
                {
                    case (PieceTypeE.PAWN):
                        basenum = basenum + (int)PieceWeightsE.PAWNCAPUTURE;
                        break;
                    case (PieceTypeE.BISHOP):
                        basenum = basenum + (int)PieceWeightsE.BISHOPCAPUTURE;
                        break;
                    case (PieceTypeE.KNIGHT):
                        basenum = basenum + (int)PieceWeightsE.KNIGHTCAPUTURE;
                        break;
                    case (PieceTypeE.ROOK):
                        basenum = basenum + (int)PieceWeightsE.ROOKCAPUTURE;
                        break;
                    case (PieceTypeE.QUEEN):
                        basenum = basenum + (int)PieceWeightsE.QUEENCAPUTURE;
                        break;
                    case (PieceTypeE.KING):
                        basenum = basenum + (int)PieceWeightsE.KINGCAPUTURE;
                        break;
                    default:
                        break;
                }
            }
            Debug.Log("Bishop at (" + loc.getX() + ", " + loc.getY() + ") can move to (" + point.getX() + ", " + point.getY() + ")  with weight " + basenum);
            scores.Add(new Vector3(point.getX(), point.getY(), basenum));
        }
        return scores;
    }

    //Create list of valid moves moving along diagonal until finding illegal move
    override public List<Point> canMoveList()
    {
        List<Point> retMoveList = new List<Point>();
        bool[] flagArray = { true, true, true, true };
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

        /*Debug.Log("Bishop at (" + loc.getX() + ", " + loc.getY() + ") can move to: ");
        foreach (Point p in retMoveList)
        {
            Debug.Log("(" + p.getX() + ", " + p.getY() + ")");
        }
        if (retMoveList.Count == 0)
            Debug.Log("No Possible Moves");*/

        return retMoveList;
    }

    //The bishop moves to any square (except as limited by Article 4.2) on the diagonals on which it stands.
    override public MoveTypesE canMove(Point p)
    {
        MoveTypesE mt = base.canMove(p);
        if (mt == MoveTypesE.ILLEGAL)
            return MoveTypesE.ILLEGAL;
        int dy = p.getY() - loc.getY();
        int dx = p.getX() - loc.getX();

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

	public override void moveObjectLoc(Point pt)
	{
		loc = pt;
		gameObject.transform.position = new Vector3(pt.turnToWorld()[0], .6f, pt.turnToWorld()[1]);
	}
}
