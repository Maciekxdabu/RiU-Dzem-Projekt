using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string mainSceneName;
    [SerializeField] private CanvasGroup mainMenuGroup;
    [SerializeField] private CanvasGroup fadeStoryGroup;
    [SerializeField] private float fadeSpeed = 0.5f;

    private bool fading;

    // ---------- Unity messages

    private void Start()
    {
        fadeStoryGroup.alpha = 0f;
        fadeStoryGroup.interactable = false;
        fadeStoryGroup.blocksRaycasts = false;
    }

    private void Update()
    {
        if (fading)
        {
            if (fadeStoryGroup.alpha < 1)//still fading
            {
                fadeStoryGroup.alpha += fadeSpeed * Time.deltaTime;
            }
            else//faded
            {
                fadeStoryGroup.interactable = true;
                fadeStoryGroup.blocksRaycasts = true;

                fading = false;
            }
        }
    }

    // ---------- public methods (for Buttons)

    //Display Best Score ???

    //Options ???

    public void OnStartBtn()
    {
        mainMenuGroup.interactable = false;
        mainMenuGroup.blocksRaycasts = false;
        fading = true;
    }

    public void OnFadeClickedBtn()
    {
        SceneManager.LoadScene(mainSceneName);
    }    

    public void OnExitBtn()
    {
        Application.Quit();
    }
}
