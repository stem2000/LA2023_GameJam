using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{


    [SerializeField] private List<GameObject> menusList;
    [SerializeField] private KeyCode pauseButton;


    [SerializeField] private Image timerHP;
    private int timerMaxValue;
    public event Func<int> GetTimerMaxValue;


    private void Start()
    {
        timerMaxValue = (int)(GetTimerMaxValue?.Invoke());
        //UIButtons.pauseEvent += PauseMenuView; // then add entri point
    }
    public void PauseMenuView(bool pause)
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
    public void DefeatPlayer()
    {
        Time.timeScale = 0;
        menusList[1].SetActive(true);
    }
    public void WinPlayer()
    {
        Time.timeScale = 0;
        menusList[2].SetActive(true);
    }
    public void PlayerHealthUI(int timerValue)
    {
        timerHP.fillAmount = (float)timerValue / timerMaxValue;
    }
    private void Update()
    {
        if (Input.GetKeyDown(pauseButton))
            PauseMenuView(true);
    }

}
