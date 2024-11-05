using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField] private int width = 20;
    [SerializeField] private int height = 20;
    
    private Tile[,] board;
    public GameObject tilePrefab;
    private List<Tile> tiles = new List<Tile>();

    [SerializeField] private int bombAmount = 10;
    private int bombsToFind;
    private int flagAmount;

    [SerializeField] private Camera mainCamera;

    public static TileController instance;

    public Canvas gameOverScreen;


    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        flagAmount = bombAmount;
        bombsToFind = bombAmount;
        board = new Tile[width, height];
        GenerateTiles(width, height);
    }

   

    public void GenerateTiles(int x, int y)
    {
        for (int i = 0; i< x; i++)
        {
            for(int j = 0; j< y; j++)
            {
                GameObject currentTile = Instantiate(tilePrefab, new Vector3(i + 0.5f , j + 0.5f, 0), Quaternion.identity);
                board[i,j] = currentTile.GetComponent<Tile>();

                board[i,j].X = i;
                board[i,j].Y = j;
            }
        }

        while (bombAmount > 0)
        {
            int currentX = UnityEngine.Random.Range(0, width);
            int currentY = UnityEngine.Random.Range(0, height);
            Tile currentTile = board[currentX, currentY];
                    if (currentTile.isMine == false)
            {
                currentTile.isMine = true;
                bombAmount--;
            }
        }

        for(int i = 0; i< width; i++)
        {
            for( int j = 0; j< height; j++)
            {
                 
                Tile tile = board[i, j];
                tile.minesAround();
                tiles.Add(tile);
            }
        }

        mainCamera.transform.position = new Vector3((x / 2) - 0.5f, (y / 2) + 0.5f, -8f);

        mainCamera.orthographicSize = mainCamera.orthographicSize * (height / 10);
    }

   

    



    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public int GetBombAmount()
    {
        return bombAmount;
    }

    public int GetBombsToFind()
    {
        return bombsToFind;
    }
    public int GetFlagAmount()
    {
        return flagAmount;
    }

    public void IncreaseFlagAmount()
    {
        flagAmount++;
    }

    public void DecreaseFlagAmount()
    {
        flagAmount--;
    }

    public Tile[,] GetBoard()
    {
        return board;
    }
}
