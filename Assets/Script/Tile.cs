using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public Player owner;
    private SpriteRenderer spriteRenderer; 
    public bool Obstacle;
    public Player ocupant;

    private SpriteMask mask;

	// Use this for initialization
	void Awake () {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        mask = this.GetComponent<SpriteMask>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setOwner (Player player) {

        if (owner != null)
            owner.ownedTiles--;

        owner = player;
        
        if(player != null) {
            player.ownedTiles++;

            spriteRenderer.color = new Color(0,0,0,0);

            mask.frontSortingOrder = player.frontLayerMask;
            mask.backSortingOrder = player.backLayerMask;
        }
        else {
            mask.frontSortingOrder = 1;
            mask.backSortingOrder = 0;
            spriteRenderer.color = new Color(0, 0, 0, 0.25f);
        }
    }

    public bool checkIfCanMove (Player player)
    {
        if(owner == null || owner == player )
        {
            return true;
        }
        if (owner != player && player.drillCounter > 0) {
            player.drillCounter--;
            player.MovementDrill();
            return true;
        }
        //else
        //{
        //    player.MovementBlocked();
        //}
        return false;
    }
}
