using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Spawner spawner;
    public Laser laser;
    public Player baseplayer;
    public float speedRate = 0.2f;
    public GameObject homeMenu;
    public GameObject pauseMenu;
    public GameObject gameUI;
    public GameObject restartMenu;
    public GameObject howtoplaymenu;
    public Canvas menuContainer;
    private GameObject currentMenu;
    private float nextAction = 0;
    private Player player;
    private bool isPaused;

	// Use this for initialization
	void Start () {
        isPaused = true;
        returnHome();
	}

    public void changeMenu(GameObject menu)
    {
        if (currentMenu != null)
            currentMenu.SetActive(false);
        currentMenu = menu;
        currentMenu.SetActive(true);
    }

    public void howToPlay()
    {
        changeMenu(howtoplaymenu);
    }

    public void returnHome()
    {
        isPaused = true;
        changeMenu(homeMenu);
    }

    public void startGame()
    {
        player = Instantiate(baseplayer);
        spawner.focus = player.gameObject.transform;
        laser.setFocus(player.gameObject.transform);
        gameUI.GetComponent<UpdateUI>().player = player;
        resume();
    }

    public void resume()
    {
        isPaused = false;
        changeMenu(gameUI);
        spawner.Resume();
    }

    public void pause()
    {
        isPaused = true;
        changeMenu(pauseMenu);
        spawner.Pause();
    }

    public void exit()
    {
        Application.Quit();
    }

    public void restart()
    {
        isPaused = true;
        changeMenu(restartMenu);
    }
	
	// Update is called once per frame
	void Update () {
        if (player != null && !isPaused)
        {
            if (Time.time >= nextAction)
            {
                if ((int)(Random.value * 100) % 2 == 0)
                    spawner.Spawn();
                else
                    laser.Shoot();
                nextAction = Time.time + speedRate;
            }
            if (player.lifes == 0)
            {
                player.Die();
                player = null;
                restart();
            }
            if (Input.GetKey(KeyCode.Escape))
                pause();
        }
	}
}
