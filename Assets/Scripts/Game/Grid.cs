using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Grid
{
  public int gridSize;
  public GridSpace[,] gridSpaces;

  public Grid(int gridSize)
  {
    this.gridSize = gridSize;
    ResetSpaces();
  }

  //Using the default board layout for now.
  //Later on we can make this reference a script with different formations, 
  //or even save a grid into binary data and make an editor for it.
  public void ResetSpaces()
  {
    gridSpaces = new GridSpace[gridSize, gridSize];

    for (int x = 0; x < gridSize; x++)
    {
      for (int y = 0; y < gridSize; y++)
      {
        gridSpaces[x, y] = new GridSpace(this, new Int2(x, y));
        //White Pawns
        if (y == 1)
        {
          gridSpaces[x, y].piece = new WhitePawn();
        }

        //Black Pawns
        else if (y == gridSize - 2)
        {
          gridSpaces[x, y].piece = new BlackPawn();
        }

        //First Row
        else if (y == 0)
        {
          //White Rooks
          if (x == 0 || x == gridSize - 1)
          {
            gridSpaces[x, y].piece = new WhiteRook();
          }

          //White Knights
          if (x == 1 || x == gridSize - 2)
          {
            gridSpaces[x, y].piece = new WhiteKnight();
          }

          //White Bishops
          if (x == 2 || x == gridSize - 3)
          {
            gridSpaces[x, y].piece = new WhiteBishop();
          }

          //White King
          if (x == 3)
          {
            gridSpaces[x, y].piece = new WhiteKing();
          }

          //White Queen
          if (x == 4)
            gridSpaces[x, y].piece = new WhiteQueen();
        }

        //Last Row
        else if (y == gridSize - 1)
        {
          //Black Rooks
          if (x == 0 || x == gridSize - 1)
          {
            gridSpaces[x, y].piece = new BlackRook();
          }

          //Black Knights
          if (x == 1 || x == gridSize - 2)
          {
            gridSpaces[x, y].piece = new BlackKnight();
          }

          //Black Bishop
          if (x == 2 || x == gridSize - 3)
          {
            gridSpaces[x, y].piece = new BlackBishop();
          }

          //Black King 
          if (x == 3)
          {
            gridSpaces[x, y].piece = new BlackKing();
          }

          //Black Quuen
          if (x == 4)
          {
            gridSpaces[x, y].piece = new BlackQueen();
          }
        }
      }
    }
  }

  public void ClearSpaces()
  {
    for (int x = 0; x < gridSize; x++)
    {
      for (int y = 0; y < gridSize; y++)
      {
        gridSpaces[x, y].piece = null;
      }
    }
  }
}

[System.Serializable]
public class GridSpace
{
  public Grid owner;
  public Int2 location;
  public Piece piece;

  public GridSpace(Grid owner, Int2 location)
  {
    this.owner = owner;
    this.location = location;
  }
}