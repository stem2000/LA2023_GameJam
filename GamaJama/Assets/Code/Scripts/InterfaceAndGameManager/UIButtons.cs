using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIButtons : MonoBehaviour
{
    public static event Action<bool> pauseEvent;


    public void Levels(int idScene)
    {
        SceneManager.LoadScene(idScene);
        Debug.Log(idScene);
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
