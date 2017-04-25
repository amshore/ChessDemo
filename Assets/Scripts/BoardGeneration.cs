using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGeneration : MonoBehaviour {

    private Board board;

    public BoardGeneration(Board b)
    {
        board = b;
    }

    public void defaultSetup()
    {
        board.generatePiece(Board.PlayerE.White, new Point(0, 0), Piece.PieceTypeE.ROOK, board.whiteRook, "Rook");
        board.generatePiece(Board.PlayerE.White, new Point(0, 1), Piece.PieceTypeE.KNIGHT, board.whiteKnight, "Knight");
        board.generatePiece(Board.PlayerE.White, new Point(0, 2), Piece.PieceTypeE.BISHOP, board.whiteBishop, "Bishop");
        board.generatePiece(Board.PlayerE.White, new Point(0, 3), Piece.PieceTypeE.QUEEN, board.whiteQueen, "Queen");
        board.generatePiece(Board.PlayerE.White, new Point(0, 4), Piece.PieceTypeE.KING, board.whiteKing, "King");
        board.generatePiece(Board.PlayerE.White, new Point(0, 5), Piece.PieceTypeE.BISHOP, board.whiteBishop, "Bishop");
        board.generatePiece(Board.PlayerE.White, new Point(0, 6), Piece.PieceTypeE.KNIGHT, board.whiteKnight, "Knight");
        board.generatePiece(Board.PlayerE.White, new Point(0, 7), Piece.PieceTypeE.ROOK, board.whiteRook, "Rook");
        board.generatePiece(Board.PlayerE.White, new Point(1, 0), Piece.PieceTypeE.PAWN, board.whitePawn, "Pawn");
        board.generatePiece(Board.PlayerE.White, new Point(1, 1), Piece.PieceTypeE.PAWN, board.whitePawn, "Pawn");
        board.generatePiece(Board.PlayerE.White, new Point(1, 2), Piece.PieceTypeE.PAWN, board.whitePawn, "Pawn");
        board.generatePiece(Board.PlayerE.White, new Point(1, 3), Piece.PieceTypeE.PAWN, board.whitePawn, "Pawn");
        board.generatePiece(Board.PlayerE.White, new Point(1, 4), Piece.PieceTypeE.PAWN, board.whitePawn, "Pawn");
        board.generatePiece(Board.PlayerE.White, new Point(1, 5), Piece.PieceTypeE.PAWN, board.whitePawn, "Pawn");
        board.generatePiece(Board.PlayerE.White, new Point(1, 6), Piece.PieceTypeE.PAWN, board.whitePawn, "Pawn");
        board.generatePiece(Board.PlayerE.White, new Point(1, 7), Piece.PieceTypeE.PAWN, board.whitePawn, "Pawn");

        board.generatePiece(Board.PlayerE.Black, new Point(6, 0), Piece.PieceTypeE.PAWN, board.blackPawn, "Pawn");
        board.generatePiece(Board.PlayerE.Black, new Point(6, 1), Piece.PieceTypeE.PAWN, board.blackPawn, "Pawn");
        board.generatePiece(Board.PlayerE.Black, new Point(6, 2), Piece.PieceTypeE.PAWN, board.blackPawn, "Pawn");
        board.generatePiece(Board.PlayerE.Black, new Point(6, 3), Piece.PieceTypeE.PAWN, board.blackPawn, "Pawn");
        board.generatePiece(Board.PlayerE.Black, new Point(6, 4), Piece.PieceTypeE.PAWN, board.blackPawn, "Pawn");
        board.generatePiece(Board.PlayerE.Black, new Point(6, 5), Piece.PieceTypeE.PAWN, board.blackPawn, "Pawn");
        board.generatePiece(Board.PlayerE.Black, new Point(6, 6), Piece.PieceTypeE.PAWN, board.blackPawn, "Pawn");
        board.generatePiece(Board.PlayerE.Black, new Point(6, 7), Piece.PieceTypeE.PAWN, board.blackPawn, "Pawn");
        board.generatePiece(Board.PlayerE.Black, new Point(7, 0), Piece.PieceTypeE.ROOK, board.blackRook, "Rook");
        board.generatePiece(Board.PlayerE.Black, new Point(7, 1), Piece.PieceTypeE.KNIGHT, board.blackKnight, "Knight");
        board.generatePiece(Board.PlayerE.Black, new Point(7, 2), Piece.PieceTypeE.BISHOP, board.blackBishop, "Bishop");
        board.generatePiece(Board.PlayerE.Black, new Point(7, 3), Piece.PieceTypeE.QUEEN, board.blackQueen, "Queen");
        board.generatePiece(Board.PlayerE.Black, new Point(7, 4), Piece.PieceTypeE.KING, board.blackKing, "King");
        board.generatePiece(Board.PlayerE.Black, new Point(7, 5), Piece.PieceTypeE.BISHOP, board.blackBishop, "Bishop");
        board.generatePiece(Board.PlayerE.Black, new Point(7, 6), Piece.PieceTypeE.KNIGHT, board.blackKnight, "Knight");
        board.generatePiece(Board.PlayerE.Black, new Point(7, 7), Piece.PieceTypeE.ROOK, board.blackRook, "Rook");
    }
}
