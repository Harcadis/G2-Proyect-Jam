using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControler : MonoBehaviour
{

    public Grid grid;
    public Image[] player1PowerUps = new Image[7];
    public Image[] player2PowerUps = new Image[7];
    public Text markerPlayer1;
    public Text markerPlayer2;
    public Player player1;
    public Player player2;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        markerPlayer1.text = player1.ownedTiles.ToString();
        markerPlayer2.text = player2.ownedTiles.ToString();
        for (int i = 0; i < player1.drillCounter; i++)
        {
            player1PowerUps[i].GetComponent<Image>().enabled = true;
        }
        for (int i = 0; i < player2.drillCounter; i++)
        {
            player2PowerUps[i].GetComponent<Image>().enabled = true;
        }
    }


}
