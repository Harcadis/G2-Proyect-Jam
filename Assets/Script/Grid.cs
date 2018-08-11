using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public Player player1;

    public Tile[,] tiles;

    public int gridWidth;
    public int gridHeight;

    public Tile tile;

    public float tileWidth;
    public float tileHeight;

    private float gridOriginX; 
    private float gridOriginY;

    // Use this for initialization
    void Start () {
        gridOriginX = (gridWidth * tileWidth * 0.5f);
        gridOriginY = (gridHeight * tileWidth * 0.5f);

        tiles = new Tile[gridWidth, gridHeight];

        for (int i = 0; i < gridWidth; i++)
        {
            for (int k = 0; k < gridHeight; k++)
            {
                tiles[i,k] = Instantiate(tile, gridToWorldCoord(i, k), Quaternion.identity, this.transform);
                //newTile.setOwner(player1);
            }
        }
	}

    public Vector2 gridToWorldCoord(int gridX, int gridY)
    {
        return new Vector2(tileWidth * gridX - gridOriginX, gridOriginY - tileHeight * gridY);
    }

    public bool tryMove(Player player, int gridX, int gridY)
    {
        bool condition1 = true; //if tile is inside grid boundaries;

        if(condition1 && tiles[gridX, gridY].checkIfCanMove(player))
        {
            tiles[gridX, gridY].setOwner(player);
            return true;
        }

        return false;

    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
