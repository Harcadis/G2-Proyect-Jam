﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grid : MonoBehaviour
{
    public GameSettings gameSettings;

    public Player player1;
    public Player player2;

    public GridContent superDrill;
    public GridContent obstacle;

    public Tile[,] tiles;
    public GridContent[,] tileContent;

    public int gridWidth;
    public int gridHeight;

    public Tile tile;

    public float tileWidth;
    public float tileHeight;

    private float gridOriginX;
    private float gridOriginY;

    private bool playerSpawned = false;
    private bool obstaclesSpawned = false;

    public float startTime;
    // Use this for initialization
    void Start()
    {
        startTime = Time.time;

        gridOriginX = (gridWidth * tileWidth * 0.5f);
        gridOriginY = (gridHeight * tileWidth * 0.5f);

        tiles = new Tile[gridWidth, gridHeight];
        tileContent = new GridContent[gridWidth, gridHeight];

        for (int i = 0; i < gridWidth; i++)
        {
            for (int k = 0; k < gridHeight; k++)
            {
                Vector2 tilePos = gridToWorldCoord(i, k);
                Vector2 InitialPos = tilePos + new Vector2(0, 10);

                tiles[i, k] = Instantiate(tile, InitialPos, Quaternion.identity, this.transform);

                iTween.MoveTo(tiles[i, k].gameObject, iTween.Hash("x", tilePos.x, "y", tilePos.y, "easetype", iTween.EaseType.easeInOutQuad, "time", 1.5f, "delay", 0.005f * Mathf.Pow((i + 1) * (k + 1), 1.1f)));

                tileContent[i, k] = null;
                //newTile.setOwner(player1);
            }
        }
       
        player2.gridX = 0;
        player2.gridY = 0;
        player1.gridX = gridWidth - 1;
        player1.gridY = gridHeight - 1;


        player1.transform.position = gridToWorldCoord(player1.gridX, player1.gridY);
        player2.transform.position = gridToWorldCoord(player2.gridX, player2.gridY);
    }

    public Vector2 gridToWorldCoord(int gridX, int gridY)
    {
        return new Vector2(tileWidth * (gridX + 0.5f) - gridOriginX, gridOriginY - tileHeight * (gridY - 0.5f));
    }

    public bool tryMove(PLAYERS player, int gridX, int gridY, int previousGridX, int previousGridY)
    {
        Player movingPlayer = player == PLAYERS.player1 ? player1 : player2;
        Player otherPlayer = player == PLAYERS.player1 ? player2 : player1;

        bool checkLimits = gridX < 0 || gridX >= gridWidth || gridY >= gridHeight || gridY < 0;
        bool checkOtherPlayer = otherPlayer.gridX == gridX && otherPlayer.gridY == gridY;

        if (!checkLimits && !checkOtherPlayer && tiles[gridX, gridY].checkIfCanMove(movingPlayer))
        {
            if (tileContent[gridX, gridY] != null)
            {
                if (tileContent[gridX, gridY].contentType == TILE_CONTENTS.obstacle)
                {
                    return false;
                }
                if (tileContent[gridX, gridY].contentType == TILE_CONTENTS.powerUp)
                {
                    Destroy(tileContent[gridX, gridY].gameObject);
                    movingPlayer.getPowerup();
                    tileContent[gridX, gridY] = null;
                }
            }

            tiles[previousGridX, previousGridY].setOwner(movingPlayer);
            //empty the current tile
            //tiles[gridX, gridY].setOwner(null);

            if (Random.Range(0f, 1f) < gameSettings.powerUpsSpawnRate)
                spawnPowerUp();

            return true;
        }

        return false;
    }

    private void spawnPowerUp()
    {
        bool spawned = false;
        int maximunTries = 10;

        while (!spawned && maximunTries > 0)
        {
            int powerUpX = Random.Range(1, gridWidth - 2);
            int powerUpY = Random.Range(1, gridHeight - 2);

            if (tileContent[powerUpX, powerUpY] == null)
            {
                GridContent powerUp = Instantiate(superDrill, gridToWorldCoord(powerUpX, powerUpY), Quaternion.identity, this.transform);
                powerUp.transform.Translate(new Vector3(0, 0, -1));
                tileContent[powerUpX, powerUpY] = powerUp;

                iTween.ScaleFrom(powerUp.gameObject, iTween.Hash("x", 0, "y", 0, "easetype", iTween.EaseType.easeOutBounce, "time", 0.5f));

                spawned = true;
            }
            maximunTries--;
        }
    }

    private void spawnObstacle()
    {
        bool spawned = false;
        int maximunTries = 10;

        while (!spawned && maximunTries > 0)
        {
            int obstacleX = Random.Range(1, gridWidth - 2);
            int obstacleY = Random.Range(1, gridHeight - 2);

            if (tileContent[obstacleX, obstacleY] == null)
            {
                GridContent obstacle = Instantiate(this.obstacle, gridToWorldCoord(obstacleX, obstacleY), Quaternion.identity, this.transform);
                obstacle.transform.Translate(new Vector3(0, 0, -1));
                tileContent[obstacleX, obstacleY] = obstacle;
                iTween.ScaleFrom(obstacle.gameObject, iTween.Hash("x", 0, "y", 0, "easetype", iTween.EaseType.easeInOutQuad, "time", 0.5f));
                spawned = true;
            }
            maximunTries--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - startTime) > 2.5 && !obstaclesSpawned)
        {
            for (int i = 0; i < gameSettings.initialPowerUpsOnGrid; i++)
            {
                spawnPowerUp();
            }

            for (int i = 0; i < gameSettings.obstaclesNumber; i++)
            {
                spawnObstacle();
            }

            obstaclesSpawned = true;
        }
        else if ((Time.time - startTime) > 3 && !playerSpawned)
        {
            iTween.ScaleTo(player1.gameObject, iTween.Hash("x", 1, "y", 1, "easetype", iTween.EaseType.easeOutQuad, "time", 1f));
            iTween.ScaleTo(player2.gameObject, iTween.Hash("x", 1, "y", 1, "easetype", iTween.EaseType.easeOutQuad, "time", 1f));
            playerSpawned = true;
        }
        if ((Time.time - startTime) > 4.5f)
        {
            player1.canMove = true;
            player2.canMove = true;
        }

        if((Time.time - startTime) > gameSettings.gameDuration)
        {

            if (player1.ownedTiles > player2.ownedTiles)
            {
                Debug.Log("player 1 wins");
                PlayerPrefs.SetInt("ganador", 1);
            }
            else if (player2.ownedTiles > player1.ownedTiles)
            {
                Debug.Log("player 2 wins");
                PlayerPrefs.SetInt("ganador", 2);
            }
            else
            {
                Debug.Log("draw");
                PlayerPrefs.SetInt("ganador", 0);
            }

            Application.LoadLevel(2);
        }
    }
}
