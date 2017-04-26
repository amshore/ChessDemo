using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Instance of a rook object.
/// Can generate a list of valid moves following the
/// rook movement rules.
/// </summary>
public class Rook : Piece {

    /// <summary>
    /// Default constructor.  Should never be used.
    /// </summary>
    public Rook()
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
    public Rook(int all, Point p, Board b, PieceTypeE t) : base(all, p, b, t)
    {
		
    }

    public override List<Vector3> canMoveEval()
    {
        List<Vector3> scores = new List<Vector3>();
        List<Point> pts = canMoveList();

        foreach (Point point in pts)
        {
            int basenum = pts.Count * (int)ScoreWeightsE.MOBILITY + (int)PieceWeightsE.ROOKWEIGHT;
            basenum = basenum + (point.getX()) * (7 - point.getX()) * (int)ScoreWeightsE.ROOKX + (point.getY()) * (7 - point.getY()) * (int)ScoreWeightsE.ROOKY;
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
            Debug.Log("Rook at (" + loc.getX() + ", " + loc.getY() + ") can move to (" + point.getX() + ", " + point.getY() + ")  with weight " + basenum);
            scores.Add(new Vector3(point.getX(), point.getY(), basenum));
        }
        return scores;
    }

    /// <summary>
    /// Calculate points can move to moving along files and ranks until finding an illegal position
    /// </summary>
    /// <returns>List of points that the piece can move to</returns>
    override public List<Point> canMoveList()
    {
        List<Point> retMoveList = new List<Point>();
        bool[] flagArray = { true, true, true, true };
        for(int i = 1; flagArray[0] && i < 8; i++)
        {
            Point p = new Point(loc.getX() + i, loc.getY());
            if (canMove(p) == MoveTypesE.ILLEGAL)
                flagArray[0] = false;
            else
                retMoveList.Add(p);
        }
        for (int i = 1; flagArray[1] && i < 8; i++)
        {
            Point p = new Point(loc.getX(), loc.getY() + i);
            if (canMove(p) == MoveTypesE.ILLEGAL)
                flagArray[1] = false;
            else
                retMoveList.Add(p);
        }
        for (int i = 1; flagArray[2] && i < 8; i++)
        {
            Point p = new Point(loc.getX() - i, loc.getY());
            if (canMove(p) == MoveTypesE.ILLEGAL)
                flagArray[2] = false;
            else
                retMoveList.Add(p);
        }
        for (int i = 1; flagArray[3] && i < 8; i++)
        {
            Point p = new Point(loc.getX(), loc.getY() - i);
            if (canMove(p) == MoveTypesE.ILLEGAL)
                flagArray[3] = false;
            else
                retMoveList.Add(p);
        }

        /*Debug.Log("Rook at (" + loc.getX() + ", " + loc.getY() + ") can move to: ");
        foreach (Point p in retMoveList)
        {
            Debug.Log("(" + p.getX() + ", " + p.getY() + ")");
        }
        if (retMoveList.Count == 0)
            Debug.Log("No Possible Moves");*/

        return retMoveList;
    }

    /// <summary>
    /// The rook moves to any square (except as limited by Article 4.2) on the file or rank on which it stands.
    /// </summary>
    /// <param name="p">The point the piece is trying to move to</param>
    /// <returns>The move type for the piece trying to make that move</returns>
    override public MoveTypesE canMove(Point p)
    {
        MoveTypesE mt = base.canMove(p);
        if(mt == MoveTypesE.ILLEGAL)
            return MoveTypesE.ILLEGAL;
        int dy = p.getY() - loc.getY();
        int dx = p.getX() - loc.getX();

        if(dy == 0)
        {
            int signFactor = (dx * System.Math.Abs(dx) > 0) ? 1 : -1;
            for(int i = 1; i < System.Math.Abs(dx); i++)
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
        return mt;
    }

	public override void moveObjectLoc(Point pt)
	{
		loc = pt;
		gameObject.transform.position = new Vector3(pt.turnToWorld()[0], .75f, pt.turnToWorld()[1]);
	}
}
