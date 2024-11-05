using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    public bool clicking;
    public int width;
    public int height;
    public Tile[,] board;



    private void Start()
    {
        board = TileController.instance.GetBoard();
        width =  TileController.instance.GetWidth();
        height = TileController.instance.GetHeight();
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !clicking && !GameController.instance.isGameOver)
        {

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)mousePosition.x;
            int y = (int)mousePosition.y;


            if (0 <= x && x < width && 0 <= y && y < height)
            {
                Tile clickedObject = board[x, y];
                clickedObject.GetComponent<Tile>().onClick();
            }

            clicking = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            clicking = false;
        }

        if (Input.GetMouseButtonDown(1) && !clicking && !GameController.instance.isGameOver)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)mousePosition.x;
            int y = (int)mousePosition.y;


            if (0 <= x && x < width && 0 <= y && y < height)
            {
                Tile clickedObject = board[x, y];
                clickedObject.GetComponent<Tile>().onRightClick();
            }

            clicking = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            clicking = false;
        }



        if (Input.GetMouseButtonDown(2) && !clicking && !GameController.instance.isGameOver)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = (int)mousePosition.x;
            int y = (int)mousePosition.y;


            if (0 <= x && x < width && 0 <= y && y < height)
            {
                Tile clickedObject = board[x, y];
                clickedObject.GetComponent<Tile>().onMiddleClick();
            }

            clicking = true;
        }

        if (Input.GetMouseButtonUp(2))
        {
            clicking = false;
        }
    }
}
