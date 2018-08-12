using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UiControllerSScene : MonoBehaviour {

    public Image player1Win;
    public Image player2Win;
    public Image draw;
    public GameObject moleBlue;
    public GameObject moleRed;
    public int winner;

    // Use this for initialization
    void Start () {
        winner = PlayerPrefs.GetInt("ganador");
        Debug.Log(winner);
        if (winner == 2)
        {
            player1Win.enabled = true;
            iTween.ScaleAdd(moleBlue.gameObject, iTween.Hash("x", 0.3, "y", 0.3, "easetype", iTween.EaseType.easeOutQuad, "looptype", iTween.LoopType.pingPong, "time", 0.5f));
        }
        else if (winner == 1)
        {
            player2Win.enabled = true;
            iTween.ScaleAdd(moleRed.gameObject, iTween.Hash("x", 0.3, "y", 0.3, "easetype", iTween.EaseType.easeOutQuad, "looptype", iTween.LoopType.pingPong, "time", 0.5f));
        }else
        {
            draw.enabled = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Space))
            Application.LoadLevel(0);
	}
}
