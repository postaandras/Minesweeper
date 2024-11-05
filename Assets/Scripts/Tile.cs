using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public enum tileState
{
    revealed,
    hidden,
    flagged
}
public class Tile : MonoBehaviour
{
    public tileState state = tileState.hidden;
    public bool isMine = false;
    public int X;
    public int Y;
    
    private int width, height;
    private int minesAroundTile;

    private Tile[,] tiles;

    public List<Sprite> numberSprites;
    public Sprite hidden;
    public Sprite empty;
    public Sprite mine;
    public Sprite wrongFlag;
    public Sprite flag;
    public Sprite exploded;

    

    //public GameController gameController ;

    // Start is called before the first frame update
    void Awake()
    {
        height = TileController.instance.GetHeight();
        width = TileController.instance.GetWidth();
        tiles = TileController.instance.GetBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {

        if(state != tileState.hidden)
        {
            return;
        }
        reveal();
        if (GameController.instance.CheckForVictory())
        {
            GameController.instance.Victory();
        }
    }

    public void reveal()
    {
        state = tileState.revealed;
        if(state != tileState.flagged && this.isMine) {
            GetComponent<SpriteRenderer>().sprite = exploded;
            state = tileState.revealed;
            if (!GameController.instance.isGameOver)
            {
                GameController.instance.Loss();
            }
            
        }

        else
        {
            
            if(this.minesAroundTile == 0)
            {
                GetComponent<SpriteRenderer>().sprite = empty;
                revealAround(X, Y);
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = numberSprites[this.minesAroundTile-1];
                //this.checkMinesAround();
            }
            //Debug.Log(mines);


        }
       
    }

    public void minesAround()
    {
        int minesAround = 0;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if(isValid(X+i, Y + j))
                {
                    if (tiles[X+i, Y + j].GetComponent<Tile>().isMine)
                    {
                        minesAround++;
                    }
                }
            }
        }
        
       this.minesAroundTile = minesAround;
    }

    

    private int flagsAround()
    {
        int flagsAround = 0;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                
                if (isValid(X + i, Y + j))
                {
                    
                    if (tiles[X + i, Y + j].GetComponent<SpriteRenderer>().sprite == flag)
                    {
                        flagsAround++;
                        
                    }
                }
            }
        }
        return flagsAround;
    }

    private void revealAround(int X, int Y)
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (!(i == 0 && j == 0) && isValid(X + i, Y + j))
                {
                    tiles[X + i, Y + j].GetComponent<Tile>().onClick();
                }
            }
        }
    }

    private bool isValid(int x, int y)
    {
        if(x >=0 && x< width && y>=0 && y < height)
        {
            return true;
        }
        return false;
    }

    public void onRightClick()
    {
        Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
        if(currentSprite == flag)
        {
            this.state = tileState.hidden;
            GetComponent<SpriteRenderer>().sprite = hidden;
            TileController.instance.IncreaseFlagAmount();
        }
        else if(currentSprite == hidden && TileController.instance.GetFlagAmount() > 0)
        {
            this.state = tileState.flagged;
            GetComponent<SpriteRenderer>().sprite = flag;
            TileController.instance.DecreaseFlagAmount();
        }

        
    }

    public void onMiddleClick()
    {
        Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
        if (numberSprites.Contains(currentSprite))
        {
            //Debug.Log(flagsAround());
            if (flagsAround() > 0)
            {
                if (numberSprites[this.flagsAround()-1].Equals(currentSprite))
                {
                    revealAround(X, Y);
                }
            }
            
        }
    }
}
