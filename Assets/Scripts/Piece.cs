using System.Collections.Generic;
using UnityEngine;

abstract public class Piece : MonoBehaviour {

    public enum PieceColor
    {
        WHITE,

    }

    public enum MoveTypesE
    {
        ILLEGAL,
        NORMAL,
        CASTLE,
        DOUBLESTEP,
        ENPASSANT,
        CAPTURE,
        PROMOTE
    };

    public enum PieceTypeE
    {
        PAWN,
        ROOK,
        KNIGHT,
        BISHOP,
        KING,
        QUEEN
    };

    public enum PieceWeightsE: int
    {
        PAWNWEIGHT = 1,
        ROOKWEIGHT = 1,
        KNIGHTWEIGHT = 1,
        BISHOPWEIGHT = 1,
        KINGWEIGHT = 1,
        QUEENWEIGHT = 1
    };

    protected bool hasMoved = false;
    bool notClicked = true;
    protected Board gameBoard;
    int allegiance;
    protected Point loc;
    protected PieceTypeE type;
	public GameObject Whiteprefab;
	public GameObject Blackprefab;

    //Default constructor, should never be used
    public Piece()
    {
        allegiance = (int)Board.PlayerE.White;
    }

    public virtual void initialize(int all, Point p, Board b, PieceTypeE t)
    {
        allegiance = all;
        loc = p;
        gameBoard = b;
        type = t;
        Debug.Log("piece Created at (x,y): (" + p.getX() + ", " + p.getY() + ")");
    }

    //Constructor with color, location, and a reference to the board
    public Piece(int all, Point p, Board b, PieceTypeE t)
    {
        allegiance = all;
        loc = p;
        gameBoard = b;
        type = t;
		Debug.Log ("piece Created " + p.getX() + ", " + p.getY());
    }

	// Use this for initialization
	void Start () {
        gameBoard = Board.Instance;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Piece existing is being Updated!");
        if (Input.GetMouseButtonDown(0))
        {
            notClicked = !notClicked;
            Debug.Log("User clicked piece");
        }
	}

    //returns hasMoved, which returns if the piece has moved from its initial location this game
    public bool getHasMoved()
    {
        return hasMoved;
    }

    public virtual void moveObjectLoc(Point pt)
    {
        loc = pt;
		gameObject.transform.position = new Vector3(pt.turnToWorld()[0], 1f, pt.turnToWorld()[1]);
    }

    //Calculates an array of points piece can legally move to
    //Have to override
    public virtual List<Point> canMoveList()
    {
        return null;
    }

    //The default canMove function, should always be overwritten
    //This is called for each subpiece and determines if:
    // (a) The piece is moved into the board (ILLEGAL if outside the board)
    // (b) The move would leave the king in check (ILLEGAL if so)
    // (c) There is a piece at the spot moving to (NORMAL if no piece)
    // (d) The piece at the spot moving to is of the same color (CAPTURE if opposite)
    public virtual MoveTypesE canMove(Point p)
    {
        if (p.getX() == loc.getX() && p.getY() == loc.getY())
            return MoveTypesE.ILLEGAL;
        if((p.getX() >= 0) && (p.getX() <= 7) && (p.getY() >= 0) && (p.getY() <= 7))
        {
            /*if (gameBoard.inCheck(loc, p))
            {
                return MoveTypesE.ILLEGAL;
            }*/
            if (gameBoard.pieceAt(p) == null)
            {
                return MoveTypesE.NORMAL;
            }
            else if (((Piece)gameBoard.pieceAt(p).GetComponent("Piece")).getAllegiance() != allegiance)
                return MoveTypesE.CAPTURE;
        }
        return MoveTypesE.ILLEGAL;
    }

    //The default tryToMove function, and only should be overwritten if piece has special rules
    // Pawn: ENPASSANT, DOUBLESTEP, PROMOTE
    // King: CASTLE
    // Uses canMove to deterine if move is ILLEGAL, and if not, makes the move
    public virtual void tryToMove(Point p)
    {
        MoveTypesE mt = canMove(p);
        if(mt != MoveTypesE.ILLEGAL)
        {
            gameBoard.Move(loc, p);
            hasMoved = true;
        }
    }

    //Returns what type of piece this is
    public PieceTypeE getType()
    {
        return type;
    }

    //Returns the allegiance (0 for WHITE and 1 for BLACK)
    public int getAllegiance()
    {
        return allegiance;
    }

    //Sets the location of the piece to (xn, yn)
    public void setLoc(int xn, int yn)
    {
        loc.setPoint(xn, yn);
    }

    //Sets the location of the piece to p
    public void setLoc(Point p)
    {
        loc = p;
    }

    //Returns the location of the piece
    public Point getLoc()
    {
        return loc;
    }

    public int getPieceScore()
    {
        switch (type)
        {
            case PieceTypeE.PAWN:
                return (int)PieceWeightsE.PAWNWEIGHT;
            case PieceTypeE.ROOK:
                return (int)PieceWeightsE.ROOKWEIGHT;
            case PieceTypeE.KNIGHT:
                return (int)PieceWeightsE.KINGWEIGHT;
            case PieceTypeE.BISHOP:
                return (int)PieceWeightsE.BISHOPWEIGHT;
            case PieceTypeE.QUEEN:
                return (int)PieceWeightsE.QUEENWEIGHT;
            case PieceTypeE.KING:
                return (int)PieceWeightsE.KINGWEIGHT;
            default:
                return 0;

        }
    }

    //Used for highlighting the piece
    /*void OnMouseEnter()
    {
        GetComponent<Renderer>().material.shader = Shader.Find("Outlined/Silhouetted Diffuse");
    }

    //Used for highlighting the piece
    void OnMouseExit()
    {
        if (notClicked)
        {
            GetComponent<Renderer>().material.shader = Shader.Find("Standard");
            gameBoard.unhighlight();
        }
    }*/
}
