using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    public bool isPowerUp;
    public Color color;

    public int gridX;
    public int gridY;

    public Grid grid;

    public float moveCooldown = 0.4f;

    private float lastMovementTime;

    // Use this for initialization
    void Start()
    {
        this.transform.position = grid.gridToWorldCoord(gridX, gridY);
    }

    // Update is called once per frame
    void Update()
    {
        //check key press
        //check try move
        //if tryMove retuns true, move the player
        if (Time.time - lastMovementTime > moveCooldown)
        {
            if (Input.GetKey(KeyCode.W))
            {
                move(gridX, gridY - 1);
            }
            if (Input.GetKey(KeyCode.S))
            {
                move(gridX, gridY + 1);

            }
            if (Input.GetKey(KeyCode.A))
            {
                move(gridX - 1, gridY);

            }
            if (Input.GetKey(KeyCode.D))
            {
                move(gridX + 1, gridY);
            }
        }
    }

    private void move(int newGridX, int newGridY)
    {
        if(grid.tryMove(this, newGridX, newGridY))
        {
            lastMovementTime = Time.time;
            gridX = newGridX;
            gridY = newGridY;

            this.transform.position = grid.gridToWorldCoord(gridX, gridY);
        }

        //lastMovementTime = 4.1
    }
}
