﻿using System.Collections.Generic;
using Android.Widget;

namespace Chess.ChessBoard;

public class Bishop : Piece
{
    public Bishop(ImageView piece, int id, ImageView space, bool isWhite, int spaceId) : base(piece, id, space, isWhite, spaceId) { }

    public override List<Move> Moves(Dictionary<(char, int), Space> board, Dictionary<(string, int), Piece> pieces)
    {
        List<Move> moves = new List<Move>();
        this.Diagonals(board, pieces, ref moves);
        return moves;
    }
}
