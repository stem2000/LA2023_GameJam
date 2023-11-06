using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIButtons : MonoBehaviour
{
    public event Action<bool> pauseEvent;


    public void Levels(int idScene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(idScene + 1);
        Debug.Log(idScene + 1);
    }
    public void MainMenu(int idScene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(idScene);
    }
    public void PauseButton(bool pauseClick)
    {
        pauseClick = !pauseClick;
        pauseEvent?.Invoke(pauseClick);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextLevel()
    {
        if ((SceneManager.GetActiveScene().buildIndex + 1) < SceneManager.sceneCountInBuildSettings)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
            Debug.Log("Error scene null");
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("ExitConfirm");
    }


}
