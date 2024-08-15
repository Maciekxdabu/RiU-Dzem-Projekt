using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string mainSceneName;

    // ---------- public methods (for Buttons)

    //Display Best Score ???

    //Options ???

    public void OnStartBtn()
    {
        SceneManager.LoadScene(mainSceneName);
    }

    public void OnExitBtn()
    {
        Application.Quit();
    }
}
