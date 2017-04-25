using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Record of all moves made in game
//Modeled as doubly linked list
public class History {

    private Board board;
    Point from, to;
    Piece killedPiece;

    History next, prev;

    public History(Point f, Point t, Board b, History p)
    {
        from = f;
        to = t;
        board = b;
        prev = p;
        next = null;
        killedPiece = board.pieceAt(to);
    }

    public void setNext(History n)
    {
        next = n;
    }

    public void undoLastMove()
    {

    }

    public void redoLastMove()
    {

    }

}
