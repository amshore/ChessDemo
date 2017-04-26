using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Instance of a pawn object.
/// Can generate a list of valid moves following the
/// pawn movement rules.
/// </summary>
public class Pawn : Piece {

    /// <param name="direction">Used for determing which direction pawn moves (1 for WHITE, -1 for BLACK)</param>
    int direction;

    /// <summary>
    /// Default constructor.  Should never be used.
    /// </summary>
    public Pawn()
    {
        direction = 1;
    }

    /// <summary>
    /// Constructor that should be used.
    /// </summary>
    /// <param name="all">Bit representing the color of the Piece.
    /// White = 0, Black = 1;</param>
    /// <param name="p">The location of the piece on the board</param>
    /// <param name="b">A reference to the game board</param>
    /// <param name="t">The type of piece being created.</param>
    public Pawn(int all, Point p, Board b, PieceTypeE t) : base(all, p, b,t)
    {

    }

    /// <summary>
    /// Initializes pawn as the piece.initialize code is called and then direction is set
    /// </summary>
    /// <param name="all">Bit representing the color of the Piece.
    /// White = 0, Black = 1;</param>
    /// <param name="p">The location of the piece on the board</param>
    /// <param name="b">A reference to the game board</param>
    /// <param name="t">The type of piece being created.</param>
    override public void initialize(int all, Point p, Board b, PieceTypeE t)
    {
        base.initialize(all, p, b, t);
        if (all == 0)
            direction = 1;
        else
            direction = -1;
    }

    public override List<Vector3> canMoveEval()
    {
        List<Vector3> scores = new List<Vector3>();
        List<Point> pts = canMoveList();
        foreach(Point point in pts)
        {
            int basenum = pts.Count * (int)ScoreWeightsE.MOBILITY + (int)PieceWeightsE.PAWNWEIGHT;
            basenum = basenum + ((getAllegiance() == 0) ? (point.getX()) : (7 - point.getX())) * (int)ScoreWeightsE.PAWNX + (point.getY()) * (7 - point.getY()) * (int)ScoreWeightsE.PAWNY;
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
            else if (System.Math.Abs(loc.getX() - point.getX()) == 1 && System.Math.Abs(loc.getY() - point.getY()) == 1)
                basenum = basenum + (int)ScoreWeightsE.ENPASSANT;
            if (System.Math.Abs(point.getX() - loc.getX()) == 2)
                basenum = basenum + (int)ScoreWeightsE.DOUBLESTEP;
            if(point.getX() == ((getAllegiance() == 0)?7:0))
            {
                basenum = basenum + (int)ScoreWeightsE.PROMOTE;
            }
            Debug.Log("Pawn at (" + loc.getX() + ", " + loc.getY() + ") can move to (" + point.getX() + ", " + point.getY() + ")  with weight " + basenum);
            scores.Add(new Vector3(point.getX(), point.getY(), basenum));
        }
        return scores;
    }

    /// <summary>
    /// Create list of valid moves 
    /// </summary>
    /// <returns>List of valid points the knight can move to</returns>
    public override List<Point> canMoveList()
    {
        List<Point> retMoveList = new List<Point>();
        for(int i = -1; i <= 1; i++)
            for(int j = -1; j <= 1; j++)
            {
                Point pt = new Point(loc.getX() + i, loc.getY() + j);
                if (canMove(pt) != MoveTypesE.ILLEGAL)
                    retMoveList.Add(pt);
            }
        if (!hasMoved && canMove(new Point(loc.getX() + direction, loc.getY())) != MoveTypesE.ILLEGAL)
        {
            Point pt = new Point(loc.getX() + 2 * direction, loc.getY());
            if (canMove(pt) != MoveTypesE.ILLEGAL)
                retMoveList.Add(pt);
        }

        /*Debug.Log("Pawn at (" + loc.getX() + ", " + loc.getY() + ") can move to: ");
        foreach (Point p in retMoveList)
        {
            Debug.Log("(" + p.getX() + ", " + p.getY() + ")");
        }
        if (retMoveList.Count == 0)
            Debug.Log("No Possible Moves");*/

        return retMoveList;
    }

    //(a) The pawn may move only forward[except as limited by Article 4.2].
    //(b) Except when making a capture, it advances from its original square either one or two vacant squares along the file on which it is placed, 
    //     and on subsequent moves it advances one vacant square along the file.When capturing, it advances one square along either of the diagonals 
    //     on which it stands.
    //(c) A pawn, attacking a square crossed by an opponent's pawn which has [just] been advanced two squares in one move from its original square, 
    //     may capture this opponent's pawn as though the latter had been moved only one square.This capture may only be made in [immediate]
    //     reply to such an advance, and is called an "en passant" capture.
    override public MoveTypesE canMove(Point p)
    {
        if (base.canMove(p) == MoveTypesE.ILLEGAL)
            return MoveTypesE.ILLEGAL;
        int dy = p.getY() - loc.getY();
        int dx = p.getX() - loc.getX();
        if (dx == direction)
        {
            Piece pAt;
            if (gameBoard.pieceAt(p) == null)
                pAt = null;
            else
                pAt = (Piece)gameBoard.pieceAt(p).GetComponent("Piece");
            if ((System.Math.Abs(dy) == 1) && pAt == null)
            {
                if (gameBoard.getEnPassant() != null && gameBoard.getEnPassant().getX() == p.getX() && gameBoard.getEnPassant().getY() == p.getY())
                    return MoveTypesE.ENPASSANT;
            }
            else if ((System.Math.Abs(dy) == 1) && (getAllegiance() != pAt.getAllegiance()))
            {
                if ((direction == 1 && p.getX() == 7) || (direction == -1 && p.getX() == 0))
                    return MoveTypesE.PROMOTE;
                else
                    return MoveTypesE.CAPTURE;
            }
            if ((dy == 0) && (System.Math.Abs(dx) == 1) && pAt == null)
            {
                if ((direction == 1 && p.getX() == 7) || (direction == -1 && p.getX() == 0))
                    return MoveTypesE.PROMOTE;
                else
                    return MoveTypesE.NORMAL;
            }
        }
        if (!hasMoved && (System.Math.Abs(dx) == 2) && (dy == 0) && (gameBoard.pieceAt(p) == null) && (gameBoard.pieceAt(p.getX() - direction, p.getY()) == null))
            return MoveTypesE.DOUBLESTEP;
        return MoveTypesE.ILLEGAL;
    }

    //Same basic functionality as normal move but keeps track of setting enpassant if the pawn doublesteps and promotes the pawn if it reaches the end
    public override void tryToMove(Point p)
    {
        MoveTypesE mt = canMove(p);
        if (mt != MoveTypesE.ILLEGAL)
        {
            if (mt == MoveTypesE.DOUBLESTEP)
                gameBoard.Move(loc, p, new Point((loc.getX() + p.getX()) / 2, (loc.getY() + p.getY()) / 2));
            else
            {
                if (mt == MoveTypesE.ENPASSANT)
                    gameBoard.killEnPassant(new Point(loc.getX(), p.getY()));
                if (mt == MoveTypesE.PROMOTE)
                    gameBoard.promotePawn(loc);
                gameBoard.Move(loc, p);
            }
            hasMoved = true;
        }
    }

	public override void moveObjectLoc(Point pt)
	{
		loc = pt;
		gameObject.transform.position = new Vector3(pt.turnToWorld()[0], .5f, pt.turnToWorld()[1]);
	}
}
