using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControler : MonoBehaviour
{

    public Grid grid;
    public Sprite defaultImagen;
    public Image[] player1PowerUps;
    public Image[] player2PowerUps;
    public Transform originPlayer1UIPoweruP;
    public Transform originPlayer2UIPoweruP;
    public Text markerPlayer1;
    public Text markerPlayer2;
    public Player player1;
    public Player player2;

    public int separation = 5;


    // Use this for initialization
    void Start()
    {
        player1PowerUps = new Image[grid.gameSettings.maxPowerUps];
        player2PowerUps = new Image[grid.gameSettings.maxPowerUps];
        for (int i = 0; i < player1PowerUps.Length; i++)
        {
            player1PowerUps[i] = createGameObject(originPlayer1UIPoweruP, i, true).GetComponent<Image>();
            player1PowerUps[i].sprite = defaultImagen;
            player1PowerUps[i].enabled = false;
        }
        for (int i = 0; i < player1PowerUps.Length; i++)
        {
            player2PowerUps[i] = createGameObject(originPlayer2UIPoweruP, i, false).GetComponent<Image>();
            player2PowerUps[i].sprite = defaultImagen;
            player2PowerUps[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        markerPlayer1.text = player1.ownedTiles.ToString();
        markerPlayer2.text = player2.ownedTiles.ToString();
        for (int i = 0; i < player1.drillCounter; i++)
        {
            player1PowerUps[i].enabled = true;
        }
        for (int i = 0; i < player2.drillCounter; i++)
        {
            player2PowerUps[i].enabled = true;
        }
    }

    private GameObject createGameObject(Transform origin, int index, bool direction)
    {
        GameObject go = new GameObject();
        go.transform.parent = origin;
        go.AddComponent<CanvasRenderer>();
        go.AddComponent<Image>();
        if (direction == true)
        {
            go.GetComponent<RectTransform>().position = origin.GetComponent<RectTransform>().position + new Vector3(index * separation, 0);
        }
        else
        {
            go.GetComponent<RectTransform>().position = origin.GetComponent<RectTransform>().position + new Vector3(-(index * separation), 0);
        }

        go.GetComponent<RectTransform>().sizeDelta = new Vector2(25, 25);
        return go;
    }

}
