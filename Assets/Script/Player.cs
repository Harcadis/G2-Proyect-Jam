using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYERS { player1, player2 };

public class Player : MonoBehaviour
{

    KeyCode keyUp, keyDown, keyLeft, keyRigth;
    
    public PLAYERS playerNumber;

    public int drillCounter = 0;
    public Color color;

    public int gridX;
    public int gridY;

    public Grid grid;

    public float moveCooldown = 0.4f;

    private float lastMovementTime;

    public int ownedTiles;

    // Use this for initialization
    void Start()
    {
        if (playerNumber == PLAYERS.player1)
        {
            keyUp = KeyCode.W;
            keyDown = KeyCode.S;
            keyLeft = KeyCode.A;
            keyRigth = KeyCode.D;
        }
        if (playerNumber == PLAYERS.player2)
        {
            keyUp = KeyCode.UpArrow;
            keyDown = KeyCode.DownArrow;
            keyLeft = KeyCode.LeftArrow;
            keyRigth = KeyCode.RightArrow;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //check key press
        //check try move
        //if tryMove retuns true, move the player
        if (Time.time - lastMovementTime > moveCooldown)
        {
            if (Input.GetKey(keyUp))
            {
                move(gridX, gridY - 1);
            }
            else if (Input.GetKey(keyDown))
            {
                move(gridX, gridY + 1);
            }
            else if (Input.GetKey(keyRigth))
            {
                move(gridX + 1, gridY);
            }
            else if (Input.GetKey(keyLeft))
            {
                move(gridX - 1, gridY);
            }
        }
    }

    private void move(int newGridX, int newGridY)
    {
        if(grid.tryMove(playerNumber, newGridX, newGridY))
        {
            lastMovementTime = Time.time;
            gridX = newGridX;
            gridY = newGridY;

            this.transform.position = grid.gridToWorldCoord(gridX, gridY);
        }

        //lastMovementTime = 4.1
    }

    public void getPowerup()
    {
        drillCounter += 3;
    }
}
