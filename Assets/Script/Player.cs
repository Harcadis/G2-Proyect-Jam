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

    private float moveCooldown = 0.4f;

    private float lastMovementTime;

    public int ownedTiles;

    public int frontLayerMask;
    public int backLayerMask;

    private Transform body;

    private Vector3 rightScale;
    private Vector3 leftScale;

    // Use this for initialization
    void Start()
    {
        moveCooldown = grid.gameSettings.moveCooldown;

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

        body = this.transform.GetChild(0);
        rightScale = body.localScale;
        leftScale = body.localScale;
        leftScale.x = -body.localScale.x;
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
                body.localEulerAngles = new Vector3(0, 0, -90);
                body.localScale = rightScale;
            }
            else if (Input.GetKey(keyDown))
            {
                move(gridX, gridY + 1);
                body.localEulerAngles = new Vector3(0, 0, 90);
                body.localScale = rightScale;
            }
            else if (Input.GetKey(keyRigth))
            {
                move(gridX + 1, gridY);
                body.localEulerAngles = new Vector3(0, 0, 0);
                body.localScale = leftScale;
            }
            else if (Input.GetKey(keyLeft))
            {
                move(gridX - 1, gridY);
                body.localEulerAngles = new Vector3(0, 0, 0);
                body.localScale = rightScale;
            }
        }
    }

    private void move(int newGridX, int newGridY)
    {
        if(grid.tryMove(playerNumber, newGridX, newGridY, gridX, gridY))
        {
            lastMovementTime = Time.time;
            gridX = newGridX;
            gridY = newGridY;

            Vector2 newCoords = grid.gridToWorldCoord(gridX, gridY);
            iTween.MoveTo(this.gameObject, iTween.Hash("x", newCoords.x, "y", newCoords.y, "easetype", iTween.EaseType.easeInOutSine, "time", moveCooldown));
        }
    }

    public void getPowerup()
    {
        drillCounter += grid.gameSettings.superDrillsPerPowerUp;
        if (drillCounter > grid.gameSettings.maxPowerUps)
        {
            drillCounter = grid.gameSettings.maxPowerUps;
        }
    }
}
