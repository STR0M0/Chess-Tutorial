using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridHelper
{
    public static bool IsDiagonal(Int2 p1, Int2 p2)
    {
        var delta = (p1.Abs() - p2.Abs()).Abs();
        return delta.x == delta.y;
    }

    public static Piece IsDiagonalIntersect(Grid grid, Int2 p1, Int2 p2)
    {
        if (!IsDiagonal(p1, p2))
            return null;
        var delta = (p1 - p2).NormalizePerAxis();
        var curPos = p1;
        while (curPos != p2) // this is real sketchy, careful ;-;
        {
            curPos += delta;
            if (grid.gridSpaces[curPos.x, curPos.y].piece != null)
                return grid.gridSpaces[curPos.x, curPos.y].piece;
            continue;
        }
        return null;
    }

    public static bool IsHorizontal(Int2 p1, Int2 p2)
    {
        return p1.x == p2.x;
    }

    public static bool IsVertical(Int2 p1, Int2 p2)
    {
        return p1.y == p2.y;
    }

    public static bool IsCross(Int2 p1, Int2 p2)
    {
        return IsHorizontal(p1, p2) || IsVertical(p1, p2);
    }

    public static bool IsLShape(Int2 p1, Int2 p2, int l1 = 2, int l2 = 1)
    {
        var delta = (p1.Abs() - p2.Abs()).Abs();
        if (delta.x == l1)
            if (delta.y == l2)
                return true;
        if (delta.x == l2)
            if (delta.y == l1)
                return true;
        return false;
    }
}
