using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void KeyGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MotionGame()
    {
        SceneManager.LoadScene(2);
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene(3);
    }

    public void Menu()
    {
        CameraMotion.backCam.Stop();
        SceneManager.LoadScene(0);
    }

    public void Menu2()
    {
        SceneManager.LoadScene(0);
    }

    public void ClickExit()
    {
        Application.Quit();
    }
}
