using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public Player owner;
    private SpriteRenderer spriteRenderer; 
    public bool Obstacle;
    public Player ocupant;


	// Use this for initialization
	void Awake () {
        spriteRenderer = this.GetComponent<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setOwner (Player player) {
        owner = player;
        spriteRenderer.color = player.color;
    }

    public bool checkIfCanMove (Player player)
    {
        if(owner == null || owner == player )
        {
            return true;
        }
        if (owner != player && player.drillCounter > 0) {
            player.drillCounter--;
            return true;
        }
        return false;
    }
}
