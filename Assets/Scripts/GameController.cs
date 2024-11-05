using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Tile[,] tiles;

    public static GameController instance;

    public bool isGameOver = false;

    // Start is called before the first frame update
    void Awake()
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

    private void Start()
    {
        tiles = TileController.instance.GetBoard();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Victory()
    {
        isGameOver = true;
        Debug.Log("You win!");
        CanvasController.instance.ShowCanvas();
    }

    public void Loss()
    {
        Debug.Log("Loss!");
        isGameOver = true;
        RevealAllMines();
        CanvasController.instance.ShowCanvas();
    }

    public void RevealAllMines()
    {
        foreach (Tile tile in tiles)
        {
           
            if (tile.isMine && tile.state == tileState.hidden)
            {
                
                tile.reveal();
            }
            if (!tile.isMine && tile.state == tileState.flagged)
            {
                tile.GetComponent<SpriteRenderer>().sprite = tile.GetComponent<Tile>().wrongFlag;
            }
        }
    }

    public bool CheckForVictory()
    {
        foreach (Tile tile in tiles)
        {
            if (tile.isMine)
            {
                continue;
            }
            if (tile.state != tileState.revealed)
            {
                return false;
            }
        }
        return true;
    }
}
