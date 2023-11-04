using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public event EventHandler OnAllUFOSDefeated;// for testing, deleted 


    [SerializeField] private List<GameObject> menusList;
    [SerializeField] private KeyCode pauseButton;


    [SerializeField] private Image timerHP;
    [SerializeField] private float timerMaxValue = 60f;
    private float timerValue;


    private void Start()
    {
        OnAllUFOSDefeated += WinPlayer;
        UIButtons.pauseEvent += PauseMenuView;
    }
    private void PauseMenuView(bool pause)
    {
        if (pause == true)
        {
            if (menusList[0].activeSelf == false)
            {
                Time.timeScale = 0;
                menusList[0].SetActive(true);
            }
            else if (menusList[0].activeSelf == true)
            {
                Time.timeScale = 1;
                menusList[0].SetActive(false);
            }
        }
    }
    private void DefeatPlayer(object sender, EventArgs e)
    {
        Time.timeScale = 0;
        menusList[1].SetActive(true);
    }
    private void WinPlayer(object sender, EventArgs e)
    {
        Time.timeScale = 0;
        menusList[2].SetActive(true);
    }
    private void PlayerHealthUI()
    {
        timerHP.fillAmount = timerValue / timerMaxValue;
    }
    private void Update()
    {
        if (Input.GetKeyDown(pauseButton))
            PauseMenuView(true);
        PlayerHealthUI();

    }

}
