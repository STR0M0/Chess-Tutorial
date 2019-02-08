using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardRenderer : MonoBehaviour
{
  public float boardSize = 8;
  public Grid grid;

  [Header("White")]
  public GameObject whitePawn;
  public GameObject whiteRook;
  public GameObject whiteKnight;
  public GameObject whiteBishop;
  public GameObject whiteQueen;
  public GameObject whiteKing;

  [Header("Black")]
  public GameObject blackPawn;
  public GameObject blackRook;
  public GameObject blackKnight;
  public GameObject blackBishop;
  public GameObject blackQueen;
  public GameObject blackKing;

    [Header("Gameplay Elements")]
    public GameObject selectionQuad;

    private Camera cam;

    private Int2 selectedSpace = new Int2(-1, -1);

  private void InitializeBoard()
  {
    grid = new Grid(8);

        //Create collider for finding which space the mouse is over
        var col = gameObject.AddComponent<BoxCollider>();
        col.center = new Vector3(boardSize / 2, -0.5f, boardSize / 2);

        //The origin of the board is not exactly in the corner so we need to offset it back and to the left by half of a spaces width to fix this
        col.center -= new Vector3(1, 0, 1) * (boardSize / grid.gridSize) * 0.5f;
        col.size = new Vector3(boardSize, 1, boardSize);
  }

  [ContextMenu("Generate")]
  private void GeneratePieces()
  {
    InitializeBoard();

    foreach (var s in grid.gridSpaces)
    {
      //White Pawn
      if (s.piece is WhitePawn)
      {
        SpawnPiece(whitePawn, s.location);
      }

      //Black Pawn
      if (s.piece is BlackPawn)
      {
        SpawnPiece(blackPawn, s.location);
      }

      //White Rook
      if (s.piece is WhiteRook)
      {
        SpawnPiece(whiteRook, s.location);
      }

      //Black Rook
      if (s.piece is BlackRook)
      {
        SpawnPiece(blackRook, s.location);
      }

      //White Knight
      if (s.piece is WhiteKnight)
      {
        SpawnPiece(whiteKnight, s.location);
      }

      //Black Knight
      if (s.piece is BlackKnight)
      {
        SpawnPiece(blackKnight, s.location);
      }

      //White Bishop
      if (s.piece is WhiteBishop)
      {
        SpawnPiece(whiteBishop, s.location);
      }

      //Black Bishop
      if (s.piece is BlackBishop)
      {
        SpawnPiece(blackBishop, s.location);
      }

      //White King 
      if (s.piece is WhiteKing)
      {
        SpawnPiece(whiteKing, s.location);
      }

      //Black King
      if (s.piece is BlackKing)
      {
        SpawnPiece(blackKing, s.location);
      }

      //White Queen
      if(s.piece is WhiteQueen)
      {
        SpawnPiece(whiteQueen, s.location);
      }

      //Black Queen
      if(s.piece is BlackQueen)
      {
        SpawnPiece(blackQueen, s.location);
      }
    }
  }

  private void SpawnPiece(GameObject piece, Int2 location)
  {
    var p = Instantiate(piece);
    p.transform.parent = transform;
    p.transform.localPosition = new Vector3(location.x, 0, location.y) * (boardSize / grid.gridSize);

    grid.gridSpaces[location.x, location.y].piece.renderer = p;
  }

  [ContextMenu("Clear Board")]
  private void ClearBoard()
  {
    //Destroy Children
    var children = new List<Transform>();
    for (int i = 0; i < transform.childCount; i++)
    {
      children.Add(transform.GetChild(i));
    }
    foreach (var c in children)
    {
      DestroyImmediate(c.gameObject);
    }

    //Clear the spaces
    if(grid != null && grid.gridSpaces != null)
        grid.ClearSpaces();

        //Remove collider
        if (gameObject.GetComponent<BoxCollider>())
            DestroyImmediate(gameObject.GetComponent<BoxCollider>());
  }

    private Int2 FindSpaceFromMousePos()
  {
        //Draw a ray to hit the board
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //Calculate the delta from the boards origin to the rays hit position
            var delta = hit.point - transform.position;

            //This delta will be relative to the origin that is offset up and to the right of the true origin, so we need to fix that
            delta += new Vector3(1, 0, 1) * (boardSize / grid.gridSize) * 0.5f;

            //Divide that delta by the spacing between cells, flooring that value = int grid position
            var x = Mathf.FloorToInt(delta.x / (boardSize / grid.gridSize)); // Use X for horizontal position
            var y = Mathf.FloorToInt(delta.z / (boardSize / grid.gridSize)); // Use Z for vertical position

            //If the found space falls out of bounds, return -1
            if (x > grid.gridSize - 1 || y > grid.gridSize - 1 || x < 0 || y < 0)
                return new Int2(-1, -1);

            return new Int2(x, y);
        }
        else
        {
            //If we hit nothing, return -1
            return new Int2(-1, -1);
        }
  }

    private void Awake()
    {
        cam = Camera.main;
        ClearBoard();
        GeneratePieces();
    }

    private void Update()
    {
        //No piece selected
        if (selectedSpace == new Int2(-1, -1))
        {
            var space = FindSpaceFromMousePos();

            //This will pass if the FindSpaceFromRenderer method finds nothing as we set it to return [-1,-1] if that is the case
            if (space == new Int2(-1, -1))
            {
                selectionQuad.SetActive(false);
                return;
            }

            //Else, we have successfully found a piece and therefor we can highlight the space
            selectionQuad.SetActive(true);

            selectionQuad.transform.position = new Vector3(space.x * 1.25f, selectionQuad.transform.position.y, space.y * 1.25f);

            //This is where we will select the piece
            if (Input.GetMouseButtonDown(0))
            {
                //This is where we should highlight the possible moves
                selectedSpace = space;
            }
        }

        //Piece selected
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Temporary deselect method
                selectedSpace = new Int2(-1, -1);
            }
        }
    }
}
