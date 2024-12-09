﻿using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content.Res;
using Android.Util;
using Android.Widget;

namespace Chess.ChessBoard;

[Serializable]
public class Space
{
    [NonSerialized]
    protected static Resources res;

    public Space(Resources _res) => res = _res;

    [NonSerialized]
    public ImageView space;
    public bool isWhite;
    public int spaceId;

    public Space(ImageView space, bool isWhite, int spaceId, Resources resources = null)
    {
        this.space = space;
        this.isWhite = isWhite;
        this.spaceId = spaceId;
        if (res == null && resources != null)
            res = resources;
    }

    public Space Forward(Dictionary<(char, int), Space> board, bool isWhite)
    {
        if (!isWhite)
            return this.Down(board);
        return this.Up(board);
    }

    public Space Backward(Dictionary<(char, int), Space> board, bool isWhite)
    {
        if (!isWhite)
            return this.Up(board);
        return this.Down(board);
    }

    public Space DiagonalUp(Dictionary<(char, int), Space> board, bool isRight)
    {
        Space up = Up(board);
        if (up == null)
            return null;

        if (isRight)
        {
            Space right = up.Right(board);
            if (right == null)
                return null;

            return right;
        }

        Space left = up.Left(board);
        if (left == null)
            return null;

        return left;
    }

    public Space DiagonalDown(Dictionary<(char, int), Space> board, bool isRight)
    {
        Space down = Down(board);
        if (down == null)
            return null;

        if (isRight)
        {
            Space right = down.Right(board);
            if (right == null)
                return null;

            return right;
        }

        Space left = down.Left(board);
        if (left == null)
            return null;

        return left;
    }

    public Space Up(Dictionary<(char, int), Space> board)
    {
        (char rank, int file) = GetBoardIndex();
        file++;
        (char, int) index = (rank, file);
        if (!board.ContainsKey(index))
            return null;

        return board[index];
    }

    public Space Down(Dictionary<(char, int), Space> board)
    {
        (char rank, int file) = GetBoardIndex();
        file--;
        (char, int) index = (rank, file);
        if (!board.ContainsKey(index))
            return null;

        return board[index];
    }

    public Space Right(Dictionary<(char, int), Space> board)
    {
        (char rank, int file) = GetBoardIndex();
        rank++;
        (char, int) index = (rank, file);
        if (!board.ContainsKey(index))
            return null;

        return board[index];
    }

    public Space Left(Dictionary<(char, int), Space> board)
    {
        (char rank, int file) = GetBoardIndex();
        rank--;
        (char, int) index = (rank, file);
        if (!board.ContainsKey(index))
            return null;

        return board[index];
    }

    public Piece GetPiece(Dictionary<(string, int), Piece> boardPieces)
    {
        var pieces = boardPieces.Values.Where(p => p.spaceId == this.spaceId).ToList();
        if (pieces.Count <= 0)
            return null;

        return boardPieces.Values.FirstOrDefault(p => p.spaceId == this.spaceId);
    }

    public Space BoardSpace(Dictionary<(char, int), Space> board) => board[this.GetBoardIndex()];

    public (char, int) GetBoardIndex()
    {
        string space = res.GetResourceName(this.spaceId).Split("__")[1];
        //s = "A1";  s[0]='A'                   s[^1]='1'
        Log.Error("DebugCatSpace", $"[{space[0]}], [{space[^1]}]");
        return (space[0], int.Parse($"{space[^1]}"));
    }

    public override string ToString()
    {
        string resourceName = res.GetResourceName(spaceId);
        return resourceName.Split("__")[1];
    }
}
