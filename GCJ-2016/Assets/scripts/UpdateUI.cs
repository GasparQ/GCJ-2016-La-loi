using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateUI : MonoBehaviour {
    public Text score;
    public Text lives;
    public Text nextLife;
    public Player player;
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            score.text = player.getScore().ToString();
            lives.text = player.lifes.ToString();
            nextLife.text = player.getNextLife().ToString();
        }
	}
}
